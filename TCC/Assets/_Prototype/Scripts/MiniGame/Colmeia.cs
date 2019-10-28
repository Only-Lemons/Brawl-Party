using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colmeia : MonoBehaviour
{
    Vector3 position;
    private void Start()
    {
        position = this.transform.position;
        Destroy(this.gameObject, 2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            GameController.singleton.gameMode.DeathRule(other.GetComponent<Basket>().player);
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(position.x,0,position.z), 1 * Time.deltaTime);
    }
}
