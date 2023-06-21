using UnityEngine;

public abstract class DonutBaseState 
{
    public abstract void EnterState(DonutStateManager context);
    public abstract void UpdateState(DonutStateManager context);
    public abstract void ExitState(DonutStateManager context);
}
