using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    public Arma actualArma;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponentInParent<PlayerController>();

            for (int i = 0; i < playerController.hand.transform.childCount; i++)
            {
                Destroy(playerController.hand.transform.GetChild(i).gameObject);
            }

            if (playerController.armaInventory.Count < 2)
            {
                playerController.armaInventory.Add(actualArma);
                if (playerController.armaInventory.Count > 1)
                    playerController.playerUI.gun2.sprite = playerController.armaInventory[0].gunSprite;
            }
            else if (playerController.armaInventory.Count == 2)
            {
                if (playerController.actualArma == playerController.armaInventory[0])
                    playerController.armaInventory[0] = actualArma;
                else if (playerController.actualArma == playerController.armaInventory[1])
                    playerController.armaInventory[1] = actualArma;
            }

            playerController.actualArma = null;
            playerController.actualArma = actualArma;

            playerController.playerUI.ammo.maxValue = actualArma.ammoAmount;
            playerController.playerUI.gun.sprite = actualArma.gunSprite;
            playerController.anim.SetBool("HasGun", true);
            Instantiate(actualArma.prefab, playerController.hand.position, playerController.hand.localRotation, playerController.hand.transform);

            Destroy(this.gameObject);
        }

    }
}
