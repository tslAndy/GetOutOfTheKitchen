using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BottleExplodeState : BottleBaseState
{
    public override void EnterState(BottleStateManager context)
    {
        context.DestroyObject();
    }
    public override void UpdateState(BottleStateManager context)
    {

    }
    public override void OnCollisionEnterState(BottleStateManager context)
    {

    }

    

}
