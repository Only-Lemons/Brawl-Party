using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour, Inputs.IPlayerActions
{
    public SOPlayer player;
    public Sprite playerSprite;
    public Color playerColor;

    CharacterController _cc;
    [HideInInspector]
    public Animator anim;   
    [HideInInspector]
    public playerUIElements playerUI;
    [HideInInspector]
    public Inputs controls;

    #region Movimentacao
    public Vector3 _movementAxis;
    Vector3 _rotationAxis;
    Quaternion _targetRotation;
    public Rigidbody rb;
    #endregion

    #region Interaçao Ambiente

    public Vector3 _base;
    #endregion

    #region PowerUPs
    #endregion

    #region Status
    [Header("Status")]
    public int life;
    public float speed;
    public bool canDeath;
    #endregion
                                            
    Material hp;


    public int playerSelectIndex=0;

    //controle jhon bean
    public int morreuAgora = 0;

    public bool travar = false;

    //controle pontos genericos Snack e Falling
    public int pontosGenericos = 0;

    public GameObject[] vencedor;

    public MiniGame actualGameMode;


    public PlayerController(SOPlayer jogador)
    {
        player = jogador;
    }

    void Awake()
    {
        controls = new Inputs();
        UIManager.onChangeValues += uiUpdate;
        UIManager.onStartValues += uiStart;
    }





    void Start()
    {

        life = player.hp;
        speed = player.speed;
        _cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        travar = false;
        
    }

    private void FixedUpdate()
    {

          //  if (canDeath == true)
            //{
                if (_movementAxis != Vector3.zero)
                    anim.SetBool("isMove", true);
                else
                    anim.SetBool("isMove", false);

                actualGameMode.MovementRule(this);
                actualGameMode.RotationRule(this);

                NoAr();
                ContarExplosao();
           // }
    }


    public void ResetarPlayer()
    {
        this.transform.position = _base;
        canDeath = true;

        life = player.hp;
        speed = player.speed;
    }
    void Rot()
    {
        if (_rotationAxis != Vector3.zero)
        {

        }
    }

    #region Condições de Derrota 
    void Death()
    {
        anim.SetTrigger("Death");
        GameManager.Instance.audioManager.playDeath();
        GameController.singleton.gameMode.HitRule(this);
        if (this.transform.GetChild(2).childCount > 0)
            Destroy(this.transform.GetChild(2).GetChild(0).gameObject);

    }
    #endregion

    void uiStart()
    {
  
        playerUI.character.sprite = player.sprite;
        try
        {
            playerUI.colorBase.color = GetComponent<Renderer>().material.color;
        }
        catch { }

    }
    void uiUpdate()
    {

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            _movementAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
            try
            {
                anim.SetFloat("x", context.ReadValue<Vector2>().x);
                anim.SetFloat("y", context.ReadValue<Vector2>().y);
            }
            catch
            {
                Debug.Log("its not time yet folk calm down");
            }
        }

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            _rotationAxis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }

    }

    public void OnStart(InputAction.CallbackContext context) { }

    public void OnAim(InputAction.CallbackContext context)
    {
        GetComponent<AutoAim>().SetarBool();
    }

    public void OnInsert(InputAction.CallbackContext context) { }

    public void OnSwitch(InputAction.CallbackContext context)
    {
     
    }

    public void OnAdd(InputAction.CallbackContext context) { }

    public void OnConfirmed(InputAction.CallbackContext context) { }

    public void OnUP(InputAction.CallbackContext context) { }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            actualGameMode.Action(this);
        }
    }

    #region ControleGetItRock
    void NoAr()
    {
        if (pulou == true)
        {
            if (transform.position.y <= 0.4f)
            {
                pulou = false;
            }
            else if(transform.position.y > 3f)
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 10f, ForceMode.VelocityChange);
        }
    }

    public bool pulou = false;
    public int direc = 1;
    #endregion

    public void OnR(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnL(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    float contExplosaoPower;
    public bool explode = false;
    public void ExplosaoPower()
    {
        if(explode == true)
        {
            Debug.Log("EXPLODIU");
            contExplosaoPower = 0;
            explode = false;
        }
    }
    void ContarExplosao()
    {
        if (explode == false)
        {
            contExplosaoPower += Time.deltaTime;
            if(contExplosaoPower >= 5)
            {
                explode = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oxygen"))
        {
            actualGameMode.HitRule(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            rb.drag = 1f;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Planet"))
        {
            rb.drag = .2f;
        }
    }

}
