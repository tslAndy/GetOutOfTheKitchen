using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BottleExplodeState : BottleBaseState
{
    public override void EnterState(BottleStates context)
    {
        context.DestroyObject();
    }
    public override void UpdateState(BottleStates context)
    {

    }
    public override void OnCollisionEnterState(BottleStates context)
    {

    }

    

}
