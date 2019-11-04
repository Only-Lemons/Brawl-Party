using UnityEngine;
using System.Collections.Generic;


public class JhonBeen : IGameMode
{
    class Stun
    {
        public bool canMove;
        public float timeInStun;
    }
    public class PositionsLR
    {
        public Vector3 left;
        public Vector3 right;
    }
    GameController aux;
    float timeOfGame;
    float timeToSpawn = 0;
    GameObject _bird = Resources.Load("Mecanicas/Bird") as GameObject;
    List<PlayerController> winners = new List<PlayerController>();
    GameObject[] cameras;
    Dictionary<PlayerController, Stun> canMove = new Dictionary<PlayerController, Stun>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, PositionsLR> playerPosition = new Dictionary<PlayerController, PositionsLR>();
    int numwinner = 0;
    bool adicionolPoint = false;
    public JhonBeen(GameController gameController, float time)
    {
        timeOfGame = time;
        aux = gameController;
    }
    public void HitRule(PlayerController player)
    {
        canMove[player].canMove = false;
        canMove[player].timeInStun = 1;
    }
    void removePlayersInStun()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            if (!canMove[player].canMove)
            {
                canMove[player].timeInStun -= Time.deltaTime;
                if (canMove[player].timeInStun <= 0)
                    canMove[player].canMove = true;
            }
        }
    }
   
    public void Update()
    {
        if (!adicionolPoint)
        {
            UpdatePositionCamera();
            timeOfGame -= Time.deltaTime;
            ShowTime();
            removePlayersInStun();
            if (timeOfGame <= 0)
            {
                InsertWinners();
                WinRule();
            }

        }
    }

    private void UpdatePositionCamera()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if(aux.playerManager.playersControllers[i].transform.position.y > 0)
                cameras[i].transform.position = new Vector3(aux.playerManager.playersControllers[i].transform.position.x, aux.playerManager.playersControllers[i].transform.position.y, aux.playerManager.playersControllers[i].transform.position.z - 14);
            else
                cameras[i].transform.position = new Vector3(aux.playerManager.playersControllers[i].transform.position.x, cameras[i].transform.position.y, aux.playerManager.playersControllers[i].transform.position.z - 14);
        }
    }
    void InsertWinners()
    {
        int a = 0;
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (playerMortos[aux.playerManager.playersControllers[i]])
            {
                winners[a] = aux.playerManager.playersControllers[i];
                a++;
            }
        }
    }

    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        aux.time.text = minute + ":" + seconds;
    }
    void goDownPlayers()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3 * Time.deltaTime, player.transform.position.z);
        }
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove)
        {
            if (dir.x > 0f)
            {

                //player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].right.x, player.transform.position.y, player.transform.position.z);
                player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].right.x, player.transform.position.y, player.transform.position.z);
            }
            if (dir.x < 0f)
            {
                player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].left.x, player.transform.position.y, player.transform.position.z);
            }
        }
    }

    public void PointRule(PlayerController player)
    {
        winners.Add(player);
        numwinner++;
        WinRule();
    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        InsertPlayerInDates();
        //spawnBirds();

    }
    void spawnBirds()
    {
        int QuantidadeBomb = Random.Range(10, 18);
        for (int i = 0; i < GameController.singleton.playerManager.playersControllers.Count; i++)
        {
            List<Vector3> posicoesInstance = new List<Vector3>();
            for (int a = 0; a < QuantidadeBomb; a++)
            {
                int side = Random.Range(0, 2);
                Vector3 position;
                if (side == 0)
                {
                    position = new Vector3(aux.tileManager.bases[i].x, Random.Range(-15, 17.8f), aux.tileManager.bases[i].z);
                    //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                }
                else
                {
                    position = new Vector3(aux.tileManager.bases[i].x + 2, Random.Range(-15, 17.8f), aux.tileManager.bases[i].z);
                    //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x + 2, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                }
                while (posicoesInstance.Contains(position) && CanInstance(posicoesInstance))
                {
                    side = Random.Range(0, 2);
                    if (side == 0)
                    {
                        position = new Vector3(aux.tileManager.bases[i].x, Random.Range(-15, 17.8f), aux.tileManager.bases[i].z);
                        //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                    }
                    else
                    {
                        position = new Vector3(aux.tileManager.bases[i].x + 2, Random.Range(-15, 17.8f), aux.tileManager.bases[i].z);
                        //GameObject.Instantiate(_bird, new Vector3(aux.tileManager.bases[i].x + 2, aux.tileManager.bases[i].y + Random.Range(-19, 17.8f), aux.tileManager.bases[i].z), Quaternion.identity);
                    }
                }
                GameObject.Instantiate(_bird, position, Quaternion.identity);
            }
        }

    }
    bool CanInstance(List<Vector3> posicoes)
    {
        foreach (Vector3 pos in posicoes)
        {
            for (int i = 0; i < posicoes.Count; i++)
            {
                if (Vector3.Distance(pos, posicoes[i]) < 2f)
                {
                    return false;
                }
            }

        }
        return true;
    }
    void InsertPlayerInDates()
    {
        cameras = GameObject.FindGameObjectsWithTag("Point");
        for (int i = 0; i < GameController.singleton.playerManager.playersControllers.Count; i++)
        {
          
           
            playerMortos.Add(GameController.singleton.playerManager.playersControllers[i], false);
            PositionsLR auxLR = new PositionsLR();
            playerPosition.Add(GameController.singleton.playerManager.playersControllers[i], auxLR);
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].left = aux.tileManager.bases[i];
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].right = new Vector3(aux.tileManager.bases[i].x + 2, aux.tileManager.bases[i].y, aux.tileManager.bases[i].z);
            Stun auxStun = new Stun();
            auxStun.canMove = true;
            auxStun.timeInStun = 0;
            canMove.Add(GameController.singleton.playerManager.playersControllers[i], auxStun);
        }
    }

    public void WinRule()
    {
        if (!adicionolPoint)
        {
       
            for (int i = 0; i < winners.Count; i++)
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(winners[i])] += 1;

            }
            aux.FinishGame();
            adicionolPoint = true;
        }
    }

    public void Action(PlayerController player)
    {
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove)
        {
            player.transform.position += new Vector3(0f, 1f, 0f);
        }
    }
}
