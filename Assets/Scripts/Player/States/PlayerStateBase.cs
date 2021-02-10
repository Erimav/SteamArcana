using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : State
{
    public PlayerStateMachine machine;

    public virtual PlayerStateBase Init(PlayerStateMachine machine)
    {
        this.machine = machine;
        return this;
    }
}
