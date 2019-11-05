using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FreeForAll : IBattleMode
{
    bool adicionolPoint = false;
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

    public void HitRule(PlayerController player)
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
    public void Update()
    {
        if (adicionolPoint == false)
        {
            _actualtime -= Time.deltaTime;
            ShowTime();
        }
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
        _gameController.playerManager.RemovePlayerofDeath();
        PlayerController playerMaior = null;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in _gameController.playerManager.playersControllers)
        {
            player.gameObject.SetActive(true);
            player.playerUI.Respawn.enabled = false;
            player.ResetarPlayer();
            if (pontos[player] > maiorPonto)
            {
                maiorPonto = pontos[player];
                playerMaior = player;
            }
        }
        if (adicionolPoint == false)
        {
            GameManager.Instance.pontosGeral[_gameController.playerManager.playersControllers.IndexOf(playerMaior)] += 1;
            _gameController.FinishGame();
            adicionolPoint = true;
        }
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        player.position += dir * speed * Time.deltaTime;
    }

    public void RotationRule(Vector3 dir, Transform player)
    {
        Quaternion _targetRotation = Quaternion.identity;
        if (dir != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(dir);

        }
        player.rotation = Quaternion.Lerp(_targetRotation, Quaternion.identity, Time.deltaTime);
    }

    public void Action(PlayerController player)
    {
      
    }
    public Arma ChangeL(List<Arma> armas)
    {
        Arma aux = armas[armas.Count];
        armas.Remove(aux);
        armas.Add(aux);
        return aux;
    }

    public Arma ChangeR(List<Arma> armas)
    {
        Arma aux = armas[0];
        armas.Remove(aux);
        armas.Add(aux);
        return aux;
    }

    public bool canShoot(bool canShoot)
    {
        return !canShoot;
    }
}