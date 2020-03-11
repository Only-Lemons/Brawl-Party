using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keanu : MonoBehaviour
{
    public float potenciaDeTiro = 200;
    public GameObject projetil;
    public Transform pos;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject b = Instantiate(projetil, pos.position, Quaternion.identity);
            b.GetComponent<myRB>().AddForce(transform.position, potenciaDeTiro);
        }

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.Rotate();
    }
}
