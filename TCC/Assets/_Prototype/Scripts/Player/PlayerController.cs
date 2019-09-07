using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IMovement , Inputs.IPlayerActions
{
    public enum EstadoPU
    {
        Normal,
        Ativo
    }

    [Header("ScriptableObject")]
    public SOPlayer player;
    Rigidbody rb;  
    float turnSpeed = 10f;
    Vector3 movementAxis;
    Vector3 rotationAxis;
    Quaternion targetRotation;
    

    public Arma actualArma;

 
    #region PowerUPs
    [Header("PowerUp")]
    public EstadoPU PowerUp;
    SOPlayer statusNormal;
    List<PowerUpManager> SOpowerUps;
    #endregion

    #region Status
    [Header("Status")]
    public int life;
    public float speed;
    #endregion

    Inputs controls;


    public PlayerController(SOPlayer jogador)
    {
        player = jogador;
        statusNormal = jogador;
        PowerUp = EstadoPU.Normal;
    }

    void Awake()
    {
        statusNormal = player;
        PowerUp = EstadoPU.Normal;
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
        Debug.Log(SOpowerUps.Count);
    }
    private void FixedUpdate()
    {

        
        if (PowerUp == EstadoPU.Ativo)
            VerificarPU();
    }
    
    void VerificarPU()
    {
        Debug.Log(SOpowerUps.Count);
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
        if(PowerUp == EstadoPU.Normal)
        {
         
            PowerUp = EstadoPU.Ativo;
            PowerUpManager PUP = new PowerUpManager(Time,powerUP,this);
            PUP.Particulas = particulas;
            SOpowerUps.Add(PUP);
         
        }
        else
        {
            if (PUActive(powerUP)){
                Debug.Log("Não adicionou");
           }
            else
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
                Debug.Log("Encontrou");
                SOpowerUps[i].tempoAtual = SOpowerUps[i].time;
                return true;
            }

        }
        Debug.Log(" Não Encontrou");
        return false;
    }
    public void DesativarPowerUP()
    {
        PowerUp = EstadoPU.Normal;
      
      
    }
   

    void Update()
    {
        
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
        if(Gamepad.current == null)
        {
                rb.MovePosition(movementAxis + transform.position);
        }else
        {
           
            movementAxis *= player.speed;
            rb.velocity  = movementAxis; 
        }
  
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().x != 0  || context.ReadValue<Vector2>().y != 0)
        rotationAxis =  new Vector3(context.ReadValue<Vector2>().x ,0,context.ReadValue<Vector2>().y );
       
        Rot();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
       if(actualArma != null)
       {
            actualArma.Shoot();
            actualArma.ammoAmount-=1;
            if(actualArma.ammoAmount<=0)
                actualArma=null;   
       }
        
    }

    public void OnStart(InputAction.CallbackContext context)
    {
      
    }

}
