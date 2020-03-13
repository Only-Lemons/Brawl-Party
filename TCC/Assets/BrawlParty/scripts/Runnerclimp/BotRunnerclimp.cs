using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRunnerclimp : MonoBehaviour
{
    public float speedBot = 3;


    float distance;
    GameObject platformNext;
    Rigidbody rb;

    //limitadores
    float timeJump;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Jump()
    {
        if (transform.position.y < Camera.main.transform.position.y + 3)
        {
            timeJump += Time.deltaTime;
            if (rb.velocity.y == 0 && timeJump > 0.15f)
            {
                Debug.Log("pulo");
                rb.AddForce(Vector3.up * (11f), ForceMode.Impulse);
                timeJump = 0;
            }
        }
    }

    void Walking()
    {
        if (transform.position.x < platformNext.transform.position.x)
        {
            Debug.Log("direita");
            transform.position = new Vector3(transform.position.x + Time.fixedDeltaTime * speedBot, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > platformNext.transform.position.x)
        {
            Debug.Log("esquerda");
            transform.position = new Vector3(transform.position.x - Time.fixedDeltaTime * speedBot, transform.position.y, transform.position.z);
        }
        else
        {

        }
    }

    void Punch()
    {

    }


    void FixedUpdate()
    {
        SearchPlatform();
        Walking();
    }

    GameObject PlatformNext(GameObject[] plat)
    {
        GameObject platformNext = GameObject.FindGameObjectWithTag("Platform");
        if (platformNext != null)
        {
            distance = Vector3.Distance(platformNext.transform.position, this.transform.position);
            for (int i = 0; i < plat.Length; i++)
            {
                if (Vector3.Distance(plat[i].transform.position, this.transform.position) < distance && plat[i].transform.position.y < transform.position.y + 5)
                {
                    distance = Vector3.Distance(plat[i].transform.position, this.transform.position);
                    platformNext = plat[i];
                }
            }

        }
        return platformNext;
    }

    void SearchPlatform()
    {
        if (platformNext == null && rb.velocity.y == 0)
        {
            platformNext = PlatformNext(GameObject.FindGameObjectsWithTag("Platform"));
        }

        if (platformNext != null)
        {
            if (this.transform.position.y > platformNext.transform.position.y)
            {
                distance = 0;
                platformNext.tag = "Untagged";
                if (rb.velocity.y == 0)
                    platformNext = null;
                //alvoAtual = AlvoMaisProximo(GameObject.FindGameObjectsWithTag("Horda"));
            }
            else
            {
                Jump();
            }
        }
    }
}
