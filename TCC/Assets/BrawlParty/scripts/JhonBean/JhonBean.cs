using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JhonBean : MiniGame
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

    float timeOfGame;
    float timeToSpawn = 0;
    GameObject _bird = Resources.Load("Mecanicas/Bird") as GameObject;
    List<PlayerController> winners = new List<PlayerController>();
    GameObject[] cameras;
    Dictionary<PlayerController, Stun> canMove = new Dictionary<PlayerController, Stun>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, PositionsLR> playerPosition = new Dictionary<PlayerController, PositionsLR>();
    bool adicionolPoint = false;
    List<PlayerController> players = new List<PlayerController>();
    int vencedor;
    float zoomCam = 14f;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        if (GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;

        }
     
        CancelarCameras();
        vencedor = players.Count - 1;

        for (int i = 0; i <players.Count; i++)
        {
            if (players[i] != null)
            {
                cameras[i].transform.position = new Vector3(players[i].transform.localPosition.x, players[i].transform.localPosition.z - zoomCam);
            }
        }
        InsertPlayerInDates();
    }
    void InsertWinners()
    {
        int a = 0;
        for (int i = 0; i < players.Count; i++)
        {
            if (playerMortos[players[i]])
            {
                winners[a] = players[i];
                a++;
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        
    }
    void goDownPlayers()
    {
        foreach (PlayerController player in players)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3 * Time.deltaTime, player.transform.position.z);
        }
    }
    void CancelarCameras()
    {

        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = false;
        }

        for (int i = 0; i < players.Count; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = true;
            cameras[i].GetComponent<Camera>().fieldOfView = 28.41141f;
        }

        //teste

        int qtdPlayersJogando = players.Count;


        switch (qtdPlayersJogando)
        {
            case 1:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                zoomCam = 30;
                break;
            case 2:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                zoomCam = 25;
                break;
            case 3:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.333f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.333f, 0, 0.334f, 1);
                cameras[2].GetComponent<Camera>().rect = new Rect(0.667f, 0, 0.333f, 1);
                zoomCam = 16;
                break;
            case 4:
                cameras[0].GetComponent<Camera>().rect = new Rect(0, 0, 0.25f, 1);
                cameras[1].GetComponent<Camera>().rect = new Rect(0.25f, 0, 0.25f, 1);
                cameras[2].GetComponent<Camera>().rect = new Rect(0.50f, 0, 0.25f, 1);
                cameras[3].GetComponent<Camera>().rect = new Rect(0.75f, 0, 0.25f, 1);
                zoomCam = 14;
                break;
        }
        UpdatePositionCamera();
        //fimteste
    }
    private void UpdatePositionCamera()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                if (players[i].transform.position.y > 0)
                    cameras[i].transform.position = Vector3.Lerp(cameras[i].transform.position, new Vector3(players[i].transform.localPosition.x, players[i].transform.localPosition.y + 5, players[i].transform.localPosition.z - zoomCam), Time.deltaTime * 2);
                else
                    cameras[i].transform.position = Vector3.Lerp(cameras[i].transform.position, new Vector3(players[i].transform.localPosition.x, players[i].transform.localPosition.y + 5, players[i].transform.localPosition.z - zoomCam), Time.deltaTime * 2);
            }
        }
    }
    void InsertPlayerInDates()
    {
        cameras = new GameObject[players.Count];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i] = GameObject.Find("Camera_P" + (i + 1));
            //Debug.Log(cameras[i].gameObject.name);

        }
        for (int i = 0; i < players.Count; i++)
        {


            playerMortos.Add(players[i], false);
            PositionsLR auxLR = new PositionsLR();
            playerPosition.Add(players[i], auxLR);
            playerPosition[players[i]].left = players[i].transform.position;
            playerPosition[players[i]].right = new Vector3(players[i].transform.position.x + 2f, players[i].transform.position.y, players[i].transform.position.z);
            Stun auxStun = new Stun();
            auxStun.canMove = true;
            auxStun.timeInStun = 0;
            canMove.Add(players[i], auxStun);
        }
    }
    void Update()
    {

    }
    public override void Action(PlayerController player)
    {
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove  && !adicionolPoint && !player.travar)
        {
            player.anim.SetTrigger("Climb");
            player.transform.position += new Vector3(0f, 1f, 0f);
        }
    }

    public override void HitRule(PlayerController player)
    {
       
    }

    public override void Jump(PlayerController player)
    {
        
    }

    public override void MovementRule(PlayerController player)
    {
        if (canMove[player].canMove && !player.travar)
        {
            if (player._movementAxis.x > 0f)
            {
                player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.left), Quaternion.identity, Time.deltaTime);
                player.transform.position = new Vector3(playerPosition[player].right.x, player.transform.position.y, player.transform.position.z);
            }
            if (player._movementAxis.x < 0f)
            {
                player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);
                player.transform.position = new Vector3(playerPosition[player].left.x, player.transform.position.y, player.transform.position.z);
            }
            else
                player.transform.position = player.transform.position;

        }
    }

    public override void PointRule(PlayerController player)
    {
        player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);

       
        player.travar = true;
        vencedor--;
        switch (vencedor)
        {
            case 3:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 3;
                break;
            case 2:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 2;
                break;
            case 1:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                break;
            default:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                break;
        }

        if (vencedor <= 0)
            WinRule();
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
    public override void RotationRule(PlayerController player)
    {
        
    }

    public override void WinRule()
    {
        if (!adicionolPoint)
        {

            for (int i = 0; i < winners.Count; i++)
            {
                GameManager.Instance.pontosGeral[players.IndexOf(winners[i])] += 1;

            }
            GameManager.Instance.WinMinigame();
            adicionolPoint = true;
        }
    }

    
}
