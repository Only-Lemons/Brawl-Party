using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IMovement
{
    Player player;
    Vector3 input;
    float inputDeadzone = 0.1f;
    Rigidbody rb;
    
    float turnSpeed = 10f;
    Quaternion targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = new Player(100,8f);
        targetRotation = transform.rotation;
    }

    void Update()
    {
        GetInput();
        Turn();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
    }

    void Turn()
    {
    }

    public void Move()
    {
       
    }
}
