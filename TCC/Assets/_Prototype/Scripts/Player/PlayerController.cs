﻿using System.Collections;
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

    #region 
    [Header("Status")]
    public int life;
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
        SOpowerUps = new List<PowerUpManager>();
        rb = GetComponent<Rigidbody>();     
    }
    private void FixedUpdate()
    {

        
        if (PowerUp == EstadoPU.Ativo)
            VerificarParticulas();
    }
    
    void VerificarParticulas()
    {
        if (SOpowerUps.Count == 0)
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
            statusNormal = player;
            PowerUp = EstadoPU.Ativo;
            PowerUpManager PUP = new PowerUpManager(Time,powerUP,this);
            PUP.Particulas = particulas;
            SOpowerUps.Add(PUP);
        }
        else
        {
            PowerUpManager PUP = new PowerUpManager(Time, powerUP,this);
            SOpowerUps.Add(PUP);
        }
    }
    public void DesativarPowerUP()
    {
        player = statusNormal;
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