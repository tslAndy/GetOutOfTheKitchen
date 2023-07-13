using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_FallState : BaseTopBlockState
{
    private float _direction;
    public override void Enter(TopBlockStateManager context)
    {
    }

    public override void BlockStateUpdate(TopBlockStateManager context)
    {
        context.transform.position += -Vector3.up * context.speedOfGoingDown * Time.deltaTime;
    }

    public override void OnCollisonEnter2D(TopBlockStateManager context)
    {
        context.SwitchStates(context.goUpState);
    }

    public override void Exit(TopBlockStateManager context)    // This method is working 1 time from TopBlockStateManager, from SwitchState method
    {
        
    }

    /*
    public void MakeRaycast(TopBlockStateManager context)
    {
        Ray ray = new Ray();
        if(Physics.Raycast(ray, out RaycastHit hitInfo, context.maxDistance))
        {
            context.transform.position += hitInfo.distance * context.durationOfGoingUp * Time.deltaTime * Vector3.up;
        }
    }
    */
    
}
