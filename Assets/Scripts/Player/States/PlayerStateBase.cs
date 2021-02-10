public abstract class PlayerStateBase : State
{
    public PlayerStateMachine machine;
    public IPlayer player;

    public virtual PlayerStateBase Init(PlayerStateMachine machine)
    {
        this.machine = machine;
        this.player = machine.player;
        return this;
    }
}
