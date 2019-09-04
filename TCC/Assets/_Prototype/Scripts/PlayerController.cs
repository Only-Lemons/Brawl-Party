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

    Gamepad actualGamepad;

    Inputs controls;




    void Awake()
    {
        controls = new Inputs();
        //controls.Enable();
        
        controls.Player.SetCallbacks(this);    
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
        player = new Player(100,8f);

        actualGamepad = Gamepad.current;

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
        rb.velocity = movementAxis * player.Speed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementAxis = new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        Move();
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationAxis =  new Vector3(context.ReadValue<Vector2>().x * -1,0,context.ReadValue<Vector2>().y * -1);
       
        Rot();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
