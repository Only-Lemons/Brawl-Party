using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FreeForAll : IGameMode
{
    GameController _gameController;
    float _actualtime;
    float _timeToRespawn;
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();

    public FreeForAll(GameController gameController, float time)
    {
        _gameController = gameController;
        _actualtime = time;
        _timeToRespawn = 3;
    }

    public void DeathRule(PlayerController player)
    {
        if (player.canDeath)
        {
            player.gameObject.SetActive(false);
            if (player.playerLastDamage != null && player.playerLastDamage != player)
            {
                PlayerController auxp = player.playerLastDamage;
                PointRule(auxp);
            }

            _gameController.playerManager.playerMortos.Add(player, _timeToRespawn);
            _gameController.playerManager.playerMortosPrefabs.Add(player);
            player.playerUI.Respawn.enabled = true;
        }
    }
    public void FinishGame()
    {
        _actualtime -= Time.deltaTime;
        ShowTime();
        if (_actualtime <= 0)
            WinRule();
    }
    public void ShowTime()
    {
        string minute = ((int)(_actualtime / 60)).ToString("00"); ;
        string seconds = ((int)(_actualtime % 60)).ToString("00"); ;
        _gameController.time.text = minute + ":" + seconds;
    }
    public void PointRule(PlayerController player)
    {
        pontos[player] += 1;
        player.playerUI.points.text = pontos[player].ToString();
    }
    public void StartGame()
    {
        AddPlayerPoints();
        _gameController.playerManager.timeRespawn = _timeToRespawn;
    }
    void AddPlayerPoints()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            pontos.Add(player, 0);
            player.playerUI.points.text = pontos[player].ToString();
        }
    }
    public void WinRule()
    {
        PlayerController playerMaior = null;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in _gameController.playerManager.playersControllers)
        {
            if (pontos[player] > maiorPonto)
            {
                maiorPonto = pontos[player];
                playerMaior = player;
            }
        }
        Time.timeScale = 0;
        //SceneManager.LoadScene(1);
    }

}
