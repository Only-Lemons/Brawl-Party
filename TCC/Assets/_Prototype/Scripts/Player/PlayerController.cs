using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
{
    
    public SOPlayer player;
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
    public Arma[] armaInventory;
    [HideInInspector]
    public bool canShoot;
    public Transform hand;
    AutoAim _autoAim;
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

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        passiva = Instantiate(player.passiva);
        life = player.hp;
        speed = player.speed;

        canDeath = true;
        canShoot = true;
        PowerUp = false;

        _SOpowerUps = new List<PowerUpManager>();
        _cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        _autoAim = GetComponent<AutoAim>();
       
        armaInventory = new Arma[2];
    
        this.transform.GetChild(1).GetComponentInChildren<Renderer>().material = player.corProvisorio;
    }
    private void FixedUpdate()
    {
        TileInteract();
        this.transform.position += _movementAxis;
        Rot();
        passiva.AtivarPassiva(this);
        if (PowerUp == true)
            VerificarPU();
        if (actualArma == null)
            anim.SetBool("HasGun", false);
        else
            anim.SetBool("HasGun", true);
    }
    public void ResetarPlayer()
    {
        this.transform.position = _base;
        actualArma = null;
        armaInventory = new Arma[2];
        canShoot = true;
        life = player.hp;
        speed = player.speed;
        shield = 0;
        _SOpowerUps.Clear();
    }
    void Rot()
    {
        if (_rotationAxis != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(_rotationAxis);
            GetComponent<AutoAim>().mirando = false;
        }
        transform.rotation = Quaternion.Lerp(_targetRotation, Quaternion.identity, Time.deltaTime);
    }
    void TileInteract()
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
            Debug.Log("Tentando definir tiles interativos");
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
        GameController.singleton.gameMode.DeathRule(this);
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
            playerUI.gun.sprite = actualArma.gunSprite;
        }
        else
        {
            playerUI.ammo.value = 0;
            playerUI.ammoText.text = "0";
        }


        // mudar os outros trem aqui 

    }
    #endregion

    #region InputSystemEvents
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        _movementAxis *= (speed + speedTile) * Time.deltaTime;
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

    public void OnLook(InputAction.CallbackContext context)
    {
        _rotationAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (actualArma != null)
            {
                if (canShoot)
                {
                    transform.rotation = transform.GetChild(0).rotation * Quaternion.identity;
                    Transform transformArma = hand.GetChild(0);
                    actualArma.Shoot(transformArma.position, this.transform.rotation, transformArma.forward, this);
                    StartCoroutine(fireRate(actualArma.fireRate));
                    if (actualArma.ammoAmount <= 0)
                    {
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

    public void OnSwitch(InputAction.CallbackContext context) { }

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
}


