using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBirds : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public GameObject[] obstaculos;
    public float timeForInstantiate = 2;
    public float velocidade;
    float velocidadeDificult;
    float timeFI;
    public Vector3 posInstantiate;

    Dictionary<PlayerController, int> _positionOfDeath = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, bool> _playerDeath = new Dictionary<PlayerController, bool>();
    [SerializeField]
    int _deathplayer;
    void Start()
    {
        //AudioController.Instance.PlayAudio("BGM");
        //AudioController.Instance.PlayAudio("Platform");

        _deathplayer = -1;
        timeFI = timeForInstantiate;

        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        if (GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;
            _positionOfDeath.Add(player, 0);
            _playerDeath.Add(player, false);
            _deathplayer++;
        }


        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (i < players.Count)
            {
                players[i].setColor(GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor);
                //  players[i].playerIndiq.GetComponent<Renderer>().material.color = GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor * 4;

            }
        }
    }



    public override void Action(PlayerController player)
    {
        AudioController.Instance.PlayAudio("Up");
        player.rb.velocity = Vector3.zero;
        player.rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
    }

    public override void HitRule(PlayerController player)
    {
        AudioController.Instance.PlayAudio("Hit");
        _positionOfDeath[player] = _deathplayer;
        _deathplayer--;
        _playerDeath[player] = true;
        PointRule(player);
        player.gameObject.SetActive(false);
        if (_deathplayer <= 1)
        {
            foreach(PlayerController p in players)
            {
                if(!_playerDeath[p])
                {
                    GameManager.Instance.playersPontos[p.gameObject.transform.parent.gameObject] += players.Count - 1;
                }
            }
            TimeGameController.Instance.acabou = true;
        }
    }

    public override void MovementRule(PlayerController player)
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
    }

    void LockZ()
    {
        foreach (var player in players)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            if (player.transform.position.x <= -10)
                player.transform.position = new Vector3(-10, player.transform.position.y, 0);
            if (player.transform.position.x >= 10)
                player.transform.position = new Vector3(10, player.transform.position.y, 0);
            if (player.transform.position.y <= -6)
                player.transform.position = new Vector3(player.transform.position.x, -6, 0);
            if (player.transform.position.y >= 6)
                player.transform.position = new Vector3(player.transform.position.x, 6, 0);

        }
    }

    public override void PointRule(PlayerController player)
    {

        GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += _positionOfDeath[player] - 1;

    }

    public override void RotationRule(PlayerController player)
    {
    }

    public override void WinRule()
    {

        if (TimeGameController.Instance.AcabouMesmo())
            GameManager.Instance.WinMinigame();

    }

    void SceneMechanics() //Comportamento geral do cenário
    {

    }

    void ObstaculoGenerator() //Comportamento e geração dos obstaculos
    {
        timeForInstantiate -= Time.deltaTime;
        if (timeForInstantiate <= 0)
        {
            GameObject x = Instantiate(InstanciarObstaculo(), posInstantiate, Quaternion.identity, this.transform);
            x.transform.position = RandomPosYObstaculo(x, 5);
            x.GetComponent<ObstaculoFB>().gravidade = new Vector3(-1.0f - velocidadeDificult, 0.0f, 0.0f);
            Destroy(x, 10);

            velocidadeDificult += velocidade;
            timeForInstantiate = timeFI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        //WinRule();
        WinRule();
    }

    void FixedUpdate()
    {
        LockZ();
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        SceneMechanics();
        ObstaculoGenerator();
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    GameObject InstanciarObstaculo()
    {
        return obstaculos[Random.Range(0, obstaculos.Length)];
    }

    Vector3 RandomPosYObstaculo(GameObject obj, float minMax)
    {
        return new Vector3(obj.transform.position.x, Random.Range(-minMax, minMax), obj.transform.position.z);
    }
}
