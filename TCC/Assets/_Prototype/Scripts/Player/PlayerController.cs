using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
{
    [Header("ScriptableObject")]
    public SOPlayer player;
    CharacterController cc;
    Vector3 movementAxis;
    Vector3 rotationAxis;
    Quaternion targetRotation;

    [Header("Arma")]
    public Arma actualArma;
    public Arma[] armaInventory;
    public bool canShoot;

    public playerUIElements playerUI;

    #region Interaçao Ambiente
    Tile ativo;
    public PlayerController playerLastDamage;
    public Vector3 _base;
    #endregion

    #region PowerUPs
    [Header("PowerUp")]
    public bool PowerUp;
    List<PowerUpManager> SOpowerUps;
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

    public Inputs controls;

    Material hp;

    public void ResetarPlayer()
    {
       
        this.transform.position = _base;
        actualArma = null;
        armaInventory = new Arma[2];
        canShoot = true;
        life = player.hp;
        speed = player.speed;
        shield = 0;
        SOpowerUps.Clear();
        


    }
    public PlayerController(SOPlayer jogador)
    {
        player = jogador;
     
        PowerUp = false;
    }

    void Awake()
    {
        controls = new Inputs();
        canDeath = true;
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
        SOpowerUps = new List<PowerUpManager>();
        // rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        armaInventory = new Arma[2];
        canShoot = true;
        PowerUp = false;

        // Iniciação dos status do personagem
        life = player.hp;
        speed = player.speed;

      
    }
    
    public void ReceiveDamage(int damage,PlayerController lastDamage)
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
        GameController.Singleton.gameMode.DeathRule(this);
        if(this.transform.GetChild(2).childCount > 0)
        Destroy(this.transform.GetChild(2).GetChild(0).gameObject);
        
    }


    private void FixedUpdate()
    {
        
        passiva.AtivarPassiva(this);
        if (PowerUp == true)
            VerificarPU();


       
    }

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

    void VerificarPU()
    {
        if (SOpowerUps.Count == 0 || SOpowerUps == null)
        {
            DesativarPowerUP();
        }
        else
        {
            for (int i = 0; i < SOpowerUps.Count; i++)
            {

                if (SOpowerUps[i].AcabouTempo())
                {
                    SOpowerUps.RemoveAt(i);

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
            SOpowerUps.Add(PUP);

        }
        else
        {
            if (!PUActive(powerUP))
            {
                PowerUpManager PUP = new PowerUpManager(Time, powerUP, this);
                SOpowerUps.Add(PUP);
            }
        }
    }

    public bool PUActive(PowerUP pu)
    {

        for (int i = 0; i < SOpowerUps.Count; i++)
        {
            if (SOpowerUps[i].PU.Name == pu.Name)
            {
                SOpowerUps[i].tempoAtual = SOpowerUps[i].time;
                return true;
            }

        }

        return false;
    }

    public void DesativarPowerUP()
    {
        PowerUp = false;
    }
    public void TileInteract()
    {
        float menorDistancia = float.MaxValue;
        for (int k = 0; k < TerrainController.instance.tilesInstanciados.Count; k++)
        {
            if (Vector3.Distance(this.transform.position, TerrainController.instance.tilesInstanciados[k].Pivot.transform.position) < menorDistancia)
            {
                menorDistancia = Vector3.Distance(this.transform.position, TerrainController.instance.tilesInstanciados[k].Pivot.transform.position);
                ativo = TerrainController.instance.tilesInstanciados[k];
            }
        }
        ativo.Interagir(this);
    }


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
        if(actualArma != null)
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

    void Update()
    {

       TileInteract();
        cc.Move(movementAxis);
        Rot();
    }

    public void Rot()
    {
        if (rotationAxis != Vector3.zero)
            targetRotation = Quaternion.LookRotation(rotationAxis);
        transform.rotation = Quaternion.Lerp(targetRotation, Quaternion.identity, Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        movementAxis *= (speed + speedTile) * Time.deltaTime;

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnFire(InputAction.CallbackContext context)
    {

        if (actualArma != null)
        {
            if (canShoot)
            {
                transform.rotation = transform.GetChild(0).rotation * Quaternion.identity;
                Transform transformArma = transform.GetChild(2).GetChild(0).GetChild(0);
                actualArma.Shoot(transformArma.position, this.transform.rotation, transformArma.forward, this);
                StartCoroutine(fireRate(actualArma.fireRate));
                if (actualArma.ammoAmount <= 0)
                {
                    actualArma = null;
                    canShoot = true;
                    Destroy(transform.GetChild(2).GetChild(0).gameObject);
                }
            }
        }
    }

    public void OnStart(InputAction.CallbackContext context)
    {

    }

    IEnumerator fireRate(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void OnInsert(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSwitch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnAdd(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnConfirmed(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnUP(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}


