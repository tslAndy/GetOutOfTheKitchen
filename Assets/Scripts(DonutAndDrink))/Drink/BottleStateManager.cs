
using Unity.VisualScripting;
using UnityEngine;

public class BottleStateManager : MonoBehaviour
{
    public float forceOfTheJump;
    BottleBaseState currentState;

    [HideInInspector] public BottleExplodeState explodeState = new BottleExplodeState();
    [HideInInspector] public BottleFallState fallState = new BottleFallState();
    [HideInInspector] public BottleJumpState jumpState = new BottleJumpState();
    private void Start()
    {
        currentState = fallState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        currentState.OnCollisionEnterState(this);
    }

    public void SwitchState(BottleBaseState nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
