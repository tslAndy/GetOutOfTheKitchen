using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutStateManager : MonoBehaviour
{
    DonutBaseState currentState;
    [HideInInspector] public DonutEnterState donutEnterState = new();
    [HideInInspector] public DonutLaserState donutLaserState = new();
    [HideInInspector] public DonutExitState donutExitState = new();

    public Transform topPoint;
    public Transform sidePoint;
    public float speedInAttack;

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
