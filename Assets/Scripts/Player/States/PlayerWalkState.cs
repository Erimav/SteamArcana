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
        var motionInput = player.MotionInput;
        var motion = new Vector3(motionInput.x, 0f, motionInput.y);
        player.Move(motion * player.FinalStats.speed * Time.deltaTime);
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
