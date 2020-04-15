using UnityEngine;

public class FinishMazeGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.actualGameMode.PointRule(player);
        }
    }
}
