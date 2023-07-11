using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_GoUpState : BaseTopBlockState
{
    Vector3 _direction;
    float _procentageComplete = 0f;
    float _elapsedTime = 0f;

    public override void Enter(TopBlockStateManager context)
    {    
    }
    public override void BlockStateUpdate(TopBlockStateManager context)
    {
        _elapsedTime += Time.deltaTime;
        _procentageComplete = _elapsedTime / context.durationOfGoingUp;
        context.transform.position = Vector3.Lerp(context.bottomPosition, context.topPosition, _procentageComplete);
        if (Vector3.Distance(context.transform.position, context.topPosition) < 0.01)
        {
            context.SwitchStates(context.restState);
        }

        /*
        _direction = (context.topPosition - context.transform.position).normalized;
        context.transform.position += _direction * context.durationOfGoingUp * Time.deltaTime;
        if(Vector2.Distance(context.transform.position, context.topPosition) <= 0.05)
        {
            context.SwitchStates(context.restState);
        }
        context.transform.rotation = Quaternion.Lerp(context.bottomRotation, context.topRotation, context.lerpSpeed * Time.deltaTime);
        */

    }

    public override void OnCollisonEnter2D(TopBlockStateManager context)
    {      
    }
    public override void Exit(TopBlockStateManager context)              // This method is working 1 time from TopBlockStateManager, from SwitchState method
    {
        _procentageComplete = 0f;
        _elapsedTime = 0f;
    }
}
