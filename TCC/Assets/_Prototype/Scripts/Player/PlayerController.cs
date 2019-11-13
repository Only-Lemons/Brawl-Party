using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
{
    public SOPlayer player;
    public Sprite playerSprite;
    public Color playerColor;

    CharacterController _cc;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public playerUIElements playerUI;
    [HideInInspector]
    public Inputs controls;

    #region Movimentacao
    Vector3 _movementAxis;
    Vector3 _rotationAxis;
    Quaternion _targetRotation;
    #endregion

    #region Arma
    [Header("Arma")]
    public Arma actualArma;
    public List<Arma> armaInventory;
    [HideInInspector]
    public bool canShoot;
    public Transform hand;
    AutoAim _autoAim;
    public Transform sairTiro;
    public AudioSource armaSom;
    #endregion

    #region Interaçao Ambiente
    Tile _tileAtivo;
    [HideInInspector]
    public PlayerController playerLastDamage;

    public Vector3 _base;
    #endregion

    #region PowerUPs
    [Header("PowerUp")]
    public bool PowerUp;
    List<PowerUpManager> _SOpowerUps;
    #endregion

    #region Status
    [Header("Status")]
    public int life;
    public float speed;
    public float speedTile;
    public int shield;
    public SOPassive passiva;
    public bool canDeath;
    #endregion

    Material hp;

    int idTrocouArma = 0;

    public int playerSelectIndex=0;

    public PlayerController(SOPlayer jogador)
    {
        player = jogador;
        PowerUp = false;
    }

    void Awake()
    {
        controls = new Inputs();
        UIManager.onChangeValues += uiUpdate;
        UIManager.onStartValues += uiStart;
    }



    void Start()
    {

        life = player.hp;
        speed = player.speed;
        speedTile = 0;
        canDeath = true;
        canShoot = true;
        PowerUp = false;

        _SOpowerUps = new List<PowerUpManager>();
        _cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        _autoAim = GetComponent<AutoAim>();
        sairTiro = transform.GetChild(2);
        armaInventory.Clear();
    }
    private void FixedUpdate()
    {
        if (GameController.singleton.comecou)
        {
            TileInteract();
            AcaoTrocarArma();

            if (canDeath == true)
            {
                if (_movementAxis != Vector3.zero)
                    anim.SetBool("isMove", true);
                else
                    anim.SetBool("isMove", false);

                Rot(); //Usado apenas pra desbugar rotação em autoAIM


                GameController.singleton.gameMode.MovementRule(_movementAxis, this.transform, speed + speedTile);


                GameController.singleton.gameMode.RotationRule(_rotationAxis, this.transform);


                // passiva.AtivarPassiva(this);
                if (PowerUp == true)
                    VerificarPU();
                if (actualArma == null)
                    anim.SetBool("HasGun", false);
                else
                    anim.SetBool("HasGun", true);

                AtirarSemParar();

                NoAr();
            }
        }


        //PARA DE COLOCAR LERP NA MORTE TA ACHANDO QUE O CARA É MICHAEL JACKSON PARA IR DESLISANDO PARA BASE?????



    }
    public void ResetarPlayer()
    {
        this.transform.position = _base;
        canDeath = true;
        actualArma = null;
        armaInventory.Clear();
        canShoot = true;
        life = player.hp;
        speed = player.speed;
        shield = 0;
        _SOpowerUps.Clear();
        for (int i = 0; i < hand.transform.childCount; i++)
        {
            Destroy(hand.transform.GetChild(i).gameObject);
        }
    }
    void Rot()
    {
        if (_rotationAxis != Vector3.zero)
        {

        }
    }
    void TileInteract()
    {
        if (TerrainController.instance != null)
        {
            float menorDistancia = float.MaxValue;
            try
            {
                for (int k = 0; k < TerrainController.instance.tilesInstanciados.Count; k++)
                {
                    if (Vector3.Distance(this.transform.position, TerrainController.instance.tilesInstanciados[k].Pivot.transform.position) < menorDistancia)
                    {
                        menorDistancia = Vector3.Distance(this.transform.position, TerrainController.instance.tilesInstanciados[k].Pivot.transform.position);
                        _tileAtivo = TerrainController.instance.tilesInstanciados[k];
                    }
                }
                _tileAtivo.Interagir(this);
            }
            catch
            {

            }
        }
    }

    #region Condições de Derrota 
    public void ReceiveDamage(int damage, PlayerController lastDamage)
    {
        playerLastDamage = lastDamage;
        if (shield >= damage)
            shield -= damage;
        else if (shield < damage)
        {
            if (shield > 0)
            {
                int danoVida = damage - shield;
                shield = 0;
                life -= danoVida;
            }
            else
            {
                life -= damage;
                if (life <= 0)
                {
                    life = 0;
                    Debug.Log("Morri");
                    Death();
                }

            }
        }

    }
    void Death()
    {
        anim.SetTrigger("Death");
        GameManager.Instance.audioManager.playDeath();
        GameController.singleton.gameMode.HitRule(this);
        if (this.transform.GetChild(2).childCount > 0)
            Destroy(this.transform.GetChild(2).GetChild(0).gameObject);

    }
    #endregion

    #region Shield
    public void AtivarEscudo(int valor)
    {
        shield += valor;

    }
    public void DesativarEscudo(int valor)
    {
        if (shield > 0)
            shield -= valor;
        if (shield < 0)
            shield = 0;
    }
    #endregion

    #region PowerUP
    void VerificarPU()
    {
        if (_SOpowerUps.Count == 0 || _SOpowerUps == null)
        {
            DesativarPowerUP();
        }
        else
        {
            for (int i = 0; i < _SOpowerUps.Count; i++)
            {

                if (_SOpowerUps[i].AcabouTempo())
                {
                    _SOpowerUps.RemoveAt(i);

                }
            }
        }

    }
    public void AtivarPowerUP(float Time, GameObject[] particulas, PowerUP powerUP)
    {
        if (PowerUp == false)
        {
            PowerUp = true;
            PowerUpManager PUP = new PowerUpManager(Time, powerUP, this);
            PUP.Particulas = particulas;
            _SOpowerUps.Add(PUP);

        }
        else
        {
            if (!PUActive(powerUP))
            {
                PowerUpManager PUP = new PowerUpManager(Time, powerUP, this);
                _SOpowerUps.Add(PUP);
            }
        }
    }
    public bool PUActive(PowerUP pu)
    {

        for (int i = 0; i < _SOpowerUps.Count; i++)
        {
            if (_SOpowerUps[i].PU.Name == pu.Name)
            {
                _SOpowerUps[i].tempoAtual = _SOpowerUps[i].time;
                return true;
            }

        }

        return false;
    }
    public void DesativarPowerUP()
    {
        PowerUp = false;
    }
    #endregion

    #region UI
    void uiStart()
    {
        playerUI.hp.maxValue = player.hp;
        playerUI.character.sprite = player.sprite;
        try
        {
            playerUI.colorBase.color = GetComponent<Renderer>().material.color;
        }
        catch { }

    }
    void uiUpdate()
    {
        playerUI.hp.value = life;
        playerUI.hpText.text = life.ToString();

        //provisorio 
        if (actualArma != null)
        {

            playerUI.ammo.value = actualArma.ammoAmount;
            playerUI.ammoText.text = actualArma.ammoAmount.ToString();
            playerUI.gun.color = new Color(1, 1, 1, 1);


        }
        else
        {
            playerUI.ammo.value = 0;
            playerUI.ammoText.text = "0";
            playerUI.gun.color = new Color(0, 0, 0, 0);
            playerUI.gun.sprite = null;
        }

        // mudar os outros trem aqui 

    }
    #endregion

    #region InputSystemEvents
    public void OnMove(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            _movementAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
            try
            {
                anim.SetFloat("x", context.ReadValue<Vector2>().x);
                anim.SetFloat("y", context.ReadValue<Vector2>().y);
            }
            catch
            {
                Debug.Log("its not time yet folk calm down");
            }
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            _rotationAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }

    bool podeAtirarSemParar = false;
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (actualArma != null)
                podeAtirarSemParar = !podeAtirarSemParar;
            //AtirarSemParar();
            else podeAtirarSemParar = false;
        }

    }

    void AtirarSemParar()
    {

        if (actualArma != null)
        {
            if (podeAtirarSemParar)
            {
                if (canShoot)
                {
                   

                    actualArma.Shoot(sairTiro.position, this.transform.rotation, sairTiro.forward, this);
                    StartCoroutine(fireRate(actualArma.fireRate));
                    if (actualArma.ammoAmount <= 0 && armaInventory.Count > 0)
                    {
                        if (actualArma == armaInventory[0] && actualArma.ammoAmount <= 0)
                        {
                            armaInventory.RemoveAt(0);
                            if (armaInventory.Count > 0 && armaInventory[0] != null)
                            {
                                actualArma = armaInventory[0];
                            }
                        }
                        else if (armaInventory.Count > 1 && actualArma == armaInventory[1] && actualArma.ammoAmount <= 0)
                        {
                            armaInventory.RemoveAt(1);
                            actualArma = armaInventory[0];

                        }

                        if (armaInventory.Count == 0)
                            actualArma = null;

                        canShoot = true;
                        Destroy(hand.GetChild(0).gameObject);
                    }
                }
            }
        }
    }

    public void OnStart(InputAction.CallbackContext context) { }

    public void OnAim(InputAction.CallbackContext context)
    {
        GetComponent<AutoAim>().SetarBool();
    }

    public void OnInsert(InputAction.CallbackContext context) { }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        TrocarArma();
    }

    public void OnAdd(InputAction.CallbackContext context) { }

    public void OnConfirmed(InputAction.CallbackContext context) { }

    public void OnUP(InputAction.CallbackContext context) { }
    #endregion

    IEnumerator fireRate(float fireRate)
    {
        anim.SetBool("Shooting", true);
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        anim.SetBool("Shooting", false);
        canShoot = true;
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameController.singleton.gameMode.Action(this);
        }
    }


    //TESTES ARMAS
    void AcaoTrocarArma()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TrocarArma();
        }
    }

    void TrocarArma()
    {
        if (idTrocouArma == 0)
        {
            idTrocouArma = 1;
            if (armaInventory[0] != null)
            {
                for (int i = 0; i < hand.transform.childCount; i++)
                {
                    Destroy(hand.transform.GetChild(i).gameObject);
                }
                actualArma = armaInventory[0];
                playerUI.gun.sprite = armaInventory[0].gunSprite;

                if (armaInventory[1] != null)
                    playerUI.gun2.sprite = armaInventory[1].gunSprite;

                Instantiate(actualArma.prefab, hand);
            }
        }
        else
        {
            idTrocouArma = 0;
            if (armaInventory[1] != null)
            {
                for (int i = 0; i < hand.transform.childCount; i++)
                {
                    Destroy(hand.transform.GetChild(i).gameObject);
                }
                actualArma = armaInventory[1];
                playerUI.gun.sprite = armaInventory[1].gunSprite;

                if (armaInventory[0] != null)
                    playerUI.gun2.sprite = armaInventory[0].gunSprite;

                Instantiate(actualArma.prefab, hand.position, hand.rotation, hand);
            }
        }
    }

    #region ControleGetItRock
    void NoAr()
    {
        if (pulou == true)
        {
            if (transform.position.y <= 0.4f)
            {
                pulou = false;
            }
            else if(transform.position.y > 3f)
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 10f, ForceMode.VelocityChange);
        }
    }

    public bool pulou = false;
    public int direc = 1;
    #endregion

    public void OnR(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnL(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    //FIM TESTES
}


