﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        AddListeners();
        player.Velocity = Vector3.zero;
    }

    public override void Exit()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {   
        machine.player.MoveStarted += machine.SwitchToWalking;
    }

    private void RemoveListeners()
    {
        machine.player.MoveStarted -= machine.SwitchToWalking;
    }
}
