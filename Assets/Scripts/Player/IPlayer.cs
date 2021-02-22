using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public interface IPlayer : IPositionProvider, IRotationProvider
{
    Vector2 MotionInput { get; set; }
    Vector3 Velocity { get; set; }
    PlayerStats OriginalStats { get; set; }
    PlayerStats FinalStats { get; }
    Vector2 LookAngles { get; set; }
    PlayerSettings Settings { get; set; }
    Transform Transform { get; }
    Quaternion TargetRotation { get; set; }

    event UnityAction MoveStopped;
    event UnityAction MoveStarted;
    event UnityAction MoveProceed;
    event UnityAction GotOffTheLand;
    event UnityAction Landed;

    void Move(Vector3 motion);
    void SetTargetRotationToCameraRotation();
    void SetTargetRotationTowardsVelocity();
}
