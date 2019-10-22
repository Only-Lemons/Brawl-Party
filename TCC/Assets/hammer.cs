using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer : MonoBehaviour
{
    bool canBrick = false;
    bool canUp = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().ReceiveDamage(10000000, null);
        }
    }
    private void Update()
    {
        if (canBrick)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, -4.31f, transform.position.z), 0.2f * Time.deltaTime);
            if (transform.position == new Vector3(transform.position.x, -4.31f, this.transform.position.z) && canUp)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 5.57f, transform.position.z), 0.5f * Time.deltaTime);
                
            }
        }
    }
    public IEnumerator brickHammer()
    {
        yield return new WaitForSeconds(2f);
        canBrick = true;
        StartCoroutine(backHammer());
        

    }
    IEnumerator backHammer()
    {
        yield return new WaitForSeconds(1f);
        canUp = true;
        yield return new WaitForSeconds(2f);
        canBrick = false;
        // hammers[i].GetComponent<Animator>().SetTrigger("estate");



    }
}
