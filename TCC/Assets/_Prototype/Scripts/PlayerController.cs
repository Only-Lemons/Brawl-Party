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
    Quaternion targetRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = new Player(100,8f);

    }

    void Update()
    {
        movementAxis = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        rotationAxis = new Vector3(Input.GetAxisRaw("rotHorizontal") * -1,0,Input.GetAxisRaw("rotVertical") * -1);

    }

    void FixedUpdate()
    {
       
        Move();


    }

    public void Move()
    {
        rb.MovePosition(rb.position + movementAxis * player.Speed * Time.fixedDeltaTime);
        if(rotationAxis != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation (rotationAxis);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle (transform.eulerAngles.y, targetRotation.eulerAngles.y, 720f * Time.deltaTime);
        }
    }
}
