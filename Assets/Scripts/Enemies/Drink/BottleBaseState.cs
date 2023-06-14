using UnityEngine;

public abstract class BottleBaseState
{
    public abstract void EnterState(BottleStateManager context);
    public abstract void UpdateState(BottleStateManager context);
    public abstract void OnCollisionEnterState(BottleStateManager context);

}
