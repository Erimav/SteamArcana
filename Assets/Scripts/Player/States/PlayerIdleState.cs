using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        AddListeners();
        player.AnimatorHelper.SetWalking(true);
    }

    public override void Exit()
    {
        RemoveListeners();
        player.AnimatorHelper.SetWalking(false);
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
