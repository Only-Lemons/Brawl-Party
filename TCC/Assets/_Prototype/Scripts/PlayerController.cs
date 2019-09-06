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

    
    public Player player;
    Rigidbody rb;  
    float turnSpeed = 10f;
    Vector3 movementAxis;
    Vector3 rotationAxis;
    Quaternion targetRotation;
    

 
    #region PowerUPs
    public EstadoPU PowerUp;
    Player statusNormal;
    List<PowerUPManager> powerUPManagers;
    #endregion
    #region Status
    public int life;
    #endregion
    Inputs controls;


    public PlayerController(Player jogador)
    {
        player = jogador;
        statusNormal = jogador;
        PowerUp = EstadoPU.Normal;
    }

    void Awake()
    {
       
        life = player.hp;
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
        powerUPManagers = new List<PowerUPManager>();
        rb = GetComponent<Rigidbody>();
  
        
    }
    private void FixedUpdate()
    {

        
        if (PowerUp == EstadoPU.Ativo)
            VerificarParticulas();
    }
    
    void VerificarParticulas()
    {
        if (powerUPManagers.Count == 0)
        {
            DesativarPowerUP();
        }
        else
        {
            for (int i = 0; i < powerUPManagers.Count; i++)
            {
                if (powerUPManagers[i].AcabouTempo())
                {
                    powerUPManagers.RemoveAt(i);

                }
            }
        }
        
    }
    public void AtivarPowerUP(float Time,GameObject[] particulas,PowerUP powerUP)
    {
        if(PowerUp == EstadoPU.Normal)
        {
            statusNormal = player;
            PowerUp = EstadoPU.Ativo;
            PowerUPManager PUP = new PowerUPManager(Time,powerUP,this);
            PUP.Particulas = particulas;
            powerUPManagers.Add(PUP);
        }
        else
        {
            PowerUPManager PUP = new PowerUPManager(Time, powerUP,this);
            powerUPManagers.Add(PUP);
        }
    }
    public void DesativarPowerUP()
    {
        player = statusNormal;
        PowerUp = EstadoPU.Normal;
    }


    void Update()
    {
        
        //movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        //rotationAxis = new Vector3(Input.GetAxisRaw("rotHorizontal") * -1,0,Input.GetAxisRaw("rotVertical") * -1);
    }

    public void Rot()
    {
       

            targetRotation = Quaternion.LookRotation (rotationAxis);
            //transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, 720f * Time.deltaTime);
            transform.rotation = targetRotation * Quaternion.identity ;
    }


    public void Move()
    {
        //rb.MovePosition(rb.position + movementAxis * player.Speed * Time.deltaTime);
        rb.velocity = movementAxis * player.speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        Move();
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if(context.ReadValue<Vector2>().x != 0  || context.ReadValue<Vector2>().y != 0)
        rotationAxis =  new Vector3(context.ReadValue<Vector2>().x ,0,context.ReadValue<Vector2>().y );
       
        Rot();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnStart(InputAction.CallbackContext context)
    {
      
    }

    public void OnMoveKeyboard(InputAction.CallbackContext context)
    {
        Debug.Log("this is us");
    }

    public void OnRotateWithMouse(InputAction.CallbackContext context)
    {

        Debug.Log(Keyboard.current);
    }
}
