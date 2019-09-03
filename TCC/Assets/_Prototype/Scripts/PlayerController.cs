using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IMovement
{
    Player player;
    Rigidbody rb;  
    float turnSpeed = 10f;

    Vector3 movementAxis;
    Vector3 rotationAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = new Player(100,8f);

    }

    void Update()
    {
        movementAxis.x =  Input.GetAxisRaw("Horizontal");
        movementAxis.z =  Input.GetAxisRaw("Vertical"); 
        rotationAxis.x =  Input.GetAxisRaw("rotHorizontal");
        rotationAxis.y =  Input.GetAxisRaw("rotVertical");
         
    }

    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + movementAxis * player.Speed * Time.fixedDeltaTime);

        
        Turn();
    }

    void Turn()
    {
   
        Vector3 newPos = new Vector3(0,Mathf.Atan2(rotationAxis.x,rotationAxis.y)*180/Mathf.PI,0f);
        transform.localEulerAngles =Vector3.Cross(transform.localEulerAngles, newPos);
    }

    public void Move()
    {
       
    }
}
