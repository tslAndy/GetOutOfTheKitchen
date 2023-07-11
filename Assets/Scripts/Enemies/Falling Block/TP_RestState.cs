using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TP_RestState : BaseTopBlockState   //Top Block Rest State, the state with courutine that is waiting to fall
{
    private float _timer = 0f;
    public override void Enter(TopBlockStateManager context)
    {
        context.transform.position = context.topPosition;       // Set block position to its initial position (to prevent inaccuracies)
        context.transform.rotation = context.topRotation;       // Set block rotation to its initial rotation (to prevent inaccuracies)
    }

    public override void BlockStateUpdate(TopBlockStateManager context)
    {
        WaitUntilFall(context);
    }
  
    public override void OnCollisonEnter2D(TopBlockStateManager context)
    {   
    }

    public override void Exit(TopBlockStateManager context)       // This method is working 1 time from TopBlockStateManager, from SwitchState method
    {
    }

    private void WaitUntilFall(TopBlockStateManager context)
    {
        if (_timer < context.secondsUntileNextFall)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
            context.SwitchStates(context.fallState);
        }
    }



    /*
    private IEnumerator WaitUntilNextFall(TopBlockStateManager context)
    {
        yield return new WaitForSeconds(2f);
        context.SwitchStates(context.fallState);
        Debug.Log("Waiting end ");

    }
    */

   
}
