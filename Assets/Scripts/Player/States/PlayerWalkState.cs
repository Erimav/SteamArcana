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
        var motion = player.Transform.right * motionInput.x + player.Transform.forward * motionInput.y;
        player.Velocity = motion * player.FinalStats.speed;
        //player.SetTargetRotationTowardsVelocity();
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
