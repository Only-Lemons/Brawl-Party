using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattleMode : IGameMode
{

    bool canShoot(bool canShoot);
    Arma ChangeL(List<Arma> armas);
    Arma ChangeR(List<Arma> armas);

}
