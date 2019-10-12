using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaptureTheFlag : IGameMode
{
    GameController _gameController;
    PlayerController _auxPlayer = null;
    GameObject _flag = Resources.Load("Mecanicas/Flag") as GameObject;
    float _timeToRespawn = 3;
    float _actualtime = 5;
    public Dictionary<PlayerController, float> pontos = new Dictionary<PlayerController, float>();
    public Dictionary<PlayerController, bool> bandeira = new Dictionary<PlayerController, bool>();

    public CaptureTheFlag(GameController gameController, float time)
    {
        _auxPlayer = null;
        _gameController = GameController.singleton;
        _actualtime = time;
    }

    public void StartGame()
    {
        GameObject.Instantiate(_flag, new Vector3(0, 1, 0), Quaternion.identity);
        AddPlayerPoints();
    }
    public void FinishGame()
    {
        _actualtime -= Time.deltaTime;
        ShowTime();
        AddPoints();
        if (_actualtime <= 0)
            WinRule();
    }
    void AddPlayerPoints()
    {
        foreach (PlayerController player in _gameController.playerManager.playersControllers)
        {
            pontos.Add(player, 0);
            bandeira.Add(player, false);
            player.playerUI.points.text = pontos[player].ToString();
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(_actualtime / 60)).ToString("00"); ;
        string seconds = ((int)(_actualtime % 60)).ToString("00"); ;

        _gameController.time.text = minute + ":" + seconds;
    }
    public void PointRule(PlayerController player)
    {
        if (bandeira[player] == false)
        {
            bandeira[player] = true;
            _auxPlayer = player;
            foreach (PlayerController playerm in GameController.singleton.playerManager.playersControllers)
            {
                if (playerm != player)
                    bandeira[playerm] = false;
            }
        }
    }
    void AddPoints()
    {
        if (_auxPlayer != null)
        {
            pontos[_auxPlayer] += 8 * Time.deltaTime;
            _auxPlayer.playerUI.points.text = pontos[_auxPlayer].ToString("0");
        }
    }
    public void WinRule()
    {
        Time.timeScale = 0;
    }
    public void DeathRule(PlayerController player)
    {
        if (player.canDeath)
        {
            _gameController.playerManager.playerMortos.Add(player, _timeToRespawn);
            _gameController.playerManager.playerMortosPrefabs.Add(player);
            player.playerUI.Respawn.enabled = true;
            Vector3 posAux = player.transform.position;
            if (bandeira[player])
            {
                _auxPlayer = null;
                bandeira[player] = false;
                GameObject.Instantiate(_flag, Vector3.zero, Quaternion.identity);
            }
            player.gameObject.SetActive(false);
        }
    }
}
