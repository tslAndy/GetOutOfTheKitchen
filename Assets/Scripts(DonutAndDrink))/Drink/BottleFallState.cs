using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleFallState : BottleBaseState
{
    Rigidbody2D localRb;
    public override void EnterState(BottleStateManager context)
    {
        localRb = context.gameObject.GetComponent<Rigidbody2D>();
        localRb.isKinematic = false;
        localRb.AddForce(Vector2.up * 200f);

    }
    public override void UpdateState(BottleStateManager context)
    {

    }
    public override void OnCollisionEnterState(BottleStateManager context)
    {
        context.SwitchState(context.jumpState);
    }
}
