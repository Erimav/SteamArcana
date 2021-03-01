using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LocalPlayer : PlayerBase
{
    protected override void Awake()
    {
        base.Awake();
        var playersSystem = Resources.Load<PlayersSystem>("Systems/PlayersSystem");
        playersSystem.SetLocalPlayer(this);
    }
}

