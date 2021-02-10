using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

[Serializable]
public class PlayerStateMachine
{
    private PlayerStateBase currentState;

    [NonSerialized]
    public IPlayer player;
    public StatesSet states;
    public event UnityAction<PlayerStateBase> StateChanged;

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public PlayerStateBase State
    {
        get => currentState;
        set
        {
            if (value == currentState)
                return;

            currentState?.Exit();
            currentState = value;
            currentState.Enter();

            StateChanged?.Invoke(currentState);
        }
    }

    public void Init(IPlayer player)
    {
        this.player = player;
        InitStates();
        State = states.idle;
    }

    private void InitStates()
    {
        states = new StatesSet
        {
            idle = (PlayerIdleState)new PlayerIdleState().Init(this),
            walk = (PlayerWalkState)new PlayerWalkState().Init(this)
        };
    }

    private void SetThisRefToStates()
    {
        var fields = typeof(StatesSet).GetFields();
        object statesSet = states;
        foreach (var field in fields)
        {
            var state = field.GetValue(statesSet) as PlayerStateBase;
            state.machine = this;
        }
    }

    #region switches
    public void SwitchToIdle() => State = states.idle;
    public void SwitchToWalking() => State = states.walk;
    #endregion

    public struct StatesSet
    {
        public PlayerIdleState idle;
        public PlayerWalkState walk;
    }
}
