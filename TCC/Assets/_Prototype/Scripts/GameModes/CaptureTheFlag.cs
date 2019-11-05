using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaptureTheFlag : IBattleMode
{
    bool adicionolPoint = false;
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
    public void Update()
    {
        if (adicionolPoint == false)
        {
            _actualtime -= Time.deltaTime;
            ShowTime();
            AddPoints();
        }
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
        _gameController.playerManager.RemovePlayerofDeath();
        PlayerController playerMaior = null;
        float maiorPonto = int.MinValue;
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
    public void HitRule(PlayerController player)
    {
        if (player.canDeath)
        {
            player.gameObject.SetActive(false);
            _gameController.playerManager.playerMortos.Add(player, _timeToRespawn);
            _gameController.playerManager.playerMortosPrefabs.Add(player);
            player.playerUI.Respawn.enabled = true;
            Vector3 posAux = player.transform.position;
            if (bandeira[player])
            {
                _auxPlayer = null;
                bandeira[player] = false;
                GameObject.Instantiate(_flag, posAux, Quaternion.identity);

             }
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
        throw new System.NotImplementedException();
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
