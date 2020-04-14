using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRunnerclimp : MonoBehaviour
{
    public float speedBot = 3;

    float distance;
    GameObject platformNext;
    GameObject platformPast;
    Rigidbody rb;

    //limitadores
    float timeJump;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        platformPast = GameObject.FindGameObjectWithTag("Base");
    }

    void Jump()
    {
        if (transform.position.y <= Camera.main.transform.position.y + 4)
        {
            timeJump += Time.deltaTime;
            if (timeJump > 0.15f && rb.velocity.magnitude < 0.2f)
            {
                Debug.Log("pulo");
                rb.AddForce(Vector3.up * (11f), ForceMode.Impulse);
                timeJump = 0;
                //platformPast = null;
            }
        }
    }

    void Walking()
    {
        if (transform.position.x > platformPast.transform.position.x - 3 && transform.position.x < platformPast.transform.position.x + 3 || Mathf.Abs(rb.velocity.y) > 0.1f)
        {
            if (transform.position.x < platformNext.transform.position.x)
            {
                rb.velocity = new Vector3(rb.velocity.x + Time.deltaTime * speedBot, rb.velocity.y, rb.velocity.z);
            }
            if (transform.position.x > platformNext.transform.position.x)
            {
                rb.velocity = new Vector3(rb.velocity.x - Time.deltaTime * speedBot, rb.velocity.y, rb.velocity.z);
            }
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
                for (int j = 0; j < plat.Length; j++)
                {
                    if(plat[i].transform.position != plat[j].transform.position)
                    {
                        if(Vector3.Distance(plat[i].transform.position, transform.position) < Vector3.Distance(plat[j].transform.position, transform.position))
                        {
                            if (Vector3.Distance(plat[i].transform.position, transform.position) < distance)
                                if (plat[i].transform.position.y < this.transform.position.y +5 && plat[i].transform.position.y > this.transform.position.y)
                            {
                                distance = Vector3.Distance(plat[i].transform.position, this.transform.position);
                                platformNext = plat[i];
                            }
                        }
                    }
                }
            }

        }
        return platformNext;
    }

    void SearchPlatform()
    {
        if (platformNext == null)
        {
            platformNext = PlatformNext(GameObject.FindGameObjectsWithTag("Platform"));
        }

        if (platformNext != null)
        {
            if (this.transform.position.y >= platformNext.transform.position.y)
            {
                distance = 0;
                //platformNext.tag = "Untagged";
                //Untagg("Platform", gameObject);
                if (Mathf.Abs(rb.velocity.magnitude) < 0.1f)
                {
                    platformPast = platformNext;
                    platformNext = null;
                }
            }
            else
            {
                Jump();
            }
        }

        if(platformPast == null)
            platformPast = GameObject.FindGameObjectWithTag("Base");
    }

    void Untagg(string tag, GameObject bot)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        foreach(GameObject obj in objs)
        {
            if (Vector3.Distance(obj.transform.position, bot.transform.position) < 1 && bot.transform.position.y > platformNext.transform.position.y + 1)
                obj.tag = "Untagged";
            else
                obj.tag = tag;
        }
    }
}
