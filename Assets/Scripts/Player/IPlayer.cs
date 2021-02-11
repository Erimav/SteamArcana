using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public interface IPlayer : IPositionProvider, IRotationProvider
{
    IPlayerAnimatorHelper AnimatorHelper { get; }
    Vector2 MotionInput { get; set; }
    PlayerStats OriginalStats { get; set; }
    PlayerStats FinalStats { get; }
    Vector2 LookAngles { get; set; }
    PlayerSettings Settings { get; set; }

    event UnityAction MoveStopped;
    event UnityAction MoveStarted;
    event UnityAction MoveProceed;
    event UnityAction GotOffTheLand;
    event UnityAction Landed;

    void Move(Vector3 motion);
}
