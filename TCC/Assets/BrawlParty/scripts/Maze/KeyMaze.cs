using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMaze : MonoBehaviour
{
    Maze maze;
    bool scalad;
    void Start()
    {
        maze = GameObject.FindObjectOfType<Maze>();
        scalad = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0,90*Time.fixedDeltaTime,0);

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            maze.KeyPlayer(other.GetComponent<PlayerController>());
        }
    }
}
