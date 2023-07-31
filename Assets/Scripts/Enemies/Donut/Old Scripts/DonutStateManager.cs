using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutStateManager : MonoBehaviour
{
    DonutBaseState currentState;
    [HideInInspector] public DonutEnterState donutEnterState = new();
    [HideInInspector] public DonutUpdateState donutLaserState = new();
    [HideInInspector] public DonutExitState donutExitState = new();

    public Transform firstStop , zeroStop;
    public Transform secondStop;
    public float speedInAttack;
    public int howManyRounds;

    void Start()
    {
        currentState = donutEnterState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(DonutBaseState nextState)
    {
        currentState = nextState;
        currentState.EnterState(this);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
