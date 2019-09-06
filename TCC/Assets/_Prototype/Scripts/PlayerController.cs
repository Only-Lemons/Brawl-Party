using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour, IMovement , Inputs.IPlayerActions
{
    Player player;
    Rigidbody rb;  
    float turnSpeed = 10f;
    Vector3 movementAxis;
    Vector3 rotationAxis;
    Quaternion targetRotation;
  


    

    Inputs controls;


    public PlayerController(Player jogador)
    {
        player = jogador;
       
    }

    void Awake()
    {
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
        rb = GetComponent<Rigidbody>();
        Debug.Log(Keyboard.current + " " + this.gameObject.name);
        
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
