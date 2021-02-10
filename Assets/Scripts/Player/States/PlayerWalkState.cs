using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    public override void Enter()
    {
        AddListeners();
    }

    public override void Exit()
    {
        RemoveListeners();
    }

    public override void OnUpdate()
    {

    }

    private void AddListeners()
    {
        machine.player.MoveStopped += machine.SwitchToIdle;
    }

    private void RemoveListeners()
    {
        machine.player.MoveStopped -= machine.SwitchToWalking;
    }
}
