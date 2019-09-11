using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IMovement , Inputs.IPlayerActions
{
    [Header("ScriptableObject")]
    public SOPlayer player;
    Rigidbody rb;
    CharacterController cc;  
    float turnSpeed = 10f;
    Vector3 movementAxis;
    Vector3 rotationAxis;
    Quaternion targetRotation;
    
    [Header("Arma")]
    public Arma actualArma;
    public bool canShoot;

 
    #region PowerUPs
    [Header("PowerUp")]
    public bool PowerUp;
    SOPlayer statusNormal;
    List<PowerUpManager> SOpowerUps;
    #endregion

    #region Status
    [Header("Status")]
    public int life;
    public float speed;
    public int shield;
    #endregion

    Inputs controls;


    public PlayerController(SOPlayer jogador)
    {
        player = jogador;
        statusNormal = jogador;
        PowerUp = false; 
    }

    void Awake()
    {
        statusNormal = player;
        PowerUp = false ; 
        life = player.hp;
        speed = player.speed;
        controls = new Inputs();      
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
      
        SOpowerUps = new List<PowerUpManager>();
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        canShoot=true;
     
    }
    private void FixedUpdate()
    {
        player.passiva.AtivarPassiva(this);
        if (PowerUp == true) 
            VerificarPU();
    }
    public void AtivarEscudo(int valor){
        shield += valor;

    }
    public void DesativarEscudo(){
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
    public void AtivarPowerUP(float Time,GameObject[] particulas,PowerUP powerUP)
    {
        if(PowerUp == false)
        {
         
            PowerUp = true; 
            PowerUpManager PUP = new PowerUpManager(Time,powerUP,this);
            PUP.Particulas = particulas;
            SOpowerUps.Add(PUP);
         
        }
        else
        {
            if (!PUActive(powerUP)){
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
        PowerUp =false;
    }
   

    void Update()
    {
        rb.MovePosition(movementAxis + transform.position);
        Rot();
    }

    public void Rot()
    {
        targetRotation = Quaternion.LookRotation (rotationAxis);
        transform.rotation = targetRotation * Quaternion.identity ;
    }


    public void Move()
    {
       
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        movementAxis *= speed * Time.deltaTime;
         
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().x != 0  || context.ReadValue<Vector2>().y != 0)
        rotationAxis =  new Vector3(context.ReadValue<Vector2>().x ,0,context.ReadValue<Vector2>().y );
       
    
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        

       if(actualArma != null)
       {

            if(canShoot)
            {
                actualArma.Shoot();
                StartCoroutine(fireRate(actualArma.fireRate));
                if(actualArma.ammoAmount<=0)
                {
                    actualArma=null;
                    canShoot = true;
                    Destroy(transform.GetChild(1).GetChild(0).gameObject); 
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

}
