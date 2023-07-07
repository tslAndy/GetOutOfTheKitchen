
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BottleJumpState : BottleBaseState
{
    Vector3 rotationAmount = new Vector3(0f, 0f, 2f);
    Rigidbody2D localRb;
    public override void EnterState(BottleStates context)
    {
        localRb = context.gameObject.GetComponent<Rigidbody2D>();
        localRb.AddForce(new Vector2(-0.5f, 0.5f) * context.forceOfTheJump);
    }
    public override void UpdateState(BottleStates context)
    {
        context.transform.Rotate(rotationAmount, Space.Self);
    }
    public override void OnCollisionEnterState(BottleStates context)
    {
        context.SwitchState(context.explodeState);
    }
}
