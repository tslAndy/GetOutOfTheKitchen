using UnityEngine;

public abstract class BottleBaseState
{
    public abstract void EnterState(BottleStates context);
    public abstract void UpdateState(BottleStates context);
    public abstract void OnCollisionEnterState(BottleStates context);

}
