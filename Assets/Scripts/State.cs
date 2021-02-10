using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public abstract class State
{
    public abstract void Enter();
    public virtual void OnUpdate() { }
    public abstract void Exit();
}
