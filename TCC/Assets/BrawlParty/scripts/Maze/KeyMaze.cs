using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaze : MonoBehaviour
{
    Maze maze;
    bool scalad;
    bool nascendo;
    void Start()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        maze = GameObject.FindObjectOfType<Maze>();
        scalad = true;
        AudioManager.PlayNascendo();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nascendo)
        {
            transform.Rotate(0, 90 * Time.fixedDeltaTime, 0);
            if (scalad)
            {
                transform.localScale = new Vector3(transform.localScale.x + Time.fixedDeltaTime, transform.localScale.x, transform.localScale.x);
                if (transform.localScale.magnitude > 3)
                    scalad = false;
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.fixedDeltaTime, transform.localScale.x, transform.localScale.x);
                if (transform.localScale.magnitude < 2)
                    scalad = true;
            }
        }

        Nascendo();
    }

    void Nascendo()
    {
        if (!nascendo)
        {
            transform.Rotate(0, 90 * Time.fixedDeltaTime * 5, 0);
            if (scalad)
            {
                transform.localScale = new Vector3(transform.localScale.x + Time.fixedDeltaTime * 3, transform.localScale.x, transform.localScale.x);
                if (transform.localScale.magnitude > 10)
                    scalad = false;
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - Time.fixedDeltaTime * 3, transform.localScale.x, transform.localScale.x);
                if (transform.localScale.magnitude < 2)
                    nascendo = true;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.PlayColeta();
            maze.KeyPlayer(other.GetComponent<PlayerController>());
        }
    }
}
