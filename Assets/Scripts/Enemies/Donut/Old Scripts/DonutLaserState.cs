using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DonutLaserState : DonutBaseState
{
    private Transform _firstStop;
    private Transform _secondStop;
    private Transform _zeroStop;
    private int _currentPosition;
    private float _speed;
    private int amountOfRounds = 0;

    public override void EnterState(DonutStateManager context)
    {
        _speed = context.speedInAttack;
        _zeroStop = context.transform;
        _firstStop = context.topPoint;
        _secondStop = context.sidePoint;

    }
    public override void UpdateState(DonutStateManager context)
    {
        switch (_currentPosition)
        {
           
            case 0:
                context.transform.position = Vector2.MoveTowards(_zeroStop.position, _firstStop.position, _speed * Time.deltaTime);
                if (Vector3.Distance(context.transform.position, _firstStop.position) <= 0.1f)
                {
                    _currentPosition = 1;   
                    Debug.Log(_currentPosition);

                }
                break;

             case 1:
                 context.transform.position = Vector2.MoveTowards(_firstStop.position, _secondStop.position, _speed * Time.deltaTime);
                 if (context.transform.position == _secondStop.position)
                 {
                     _currentPosition = 2;
                     Debug.Log(_currentPosition);
                 }
                 break;

             case 2:
                 context.transform.position = Vector2.MoveTowards(_secondStop.position, _zeroStop.position, _speed * Time.deltaTime);
                 if (context.transform.position == _zeroStop.position)
                 {
                     if (amountOfRounds >= 4)
                     {
                         context.SwitchState(context.donutExitState); 
                     }
                     else
                     {
                        amountOfRounds++;
                         _currentPosition = 0;
                     }
                 }
                 break;
         
          
        }
    }
    

    public override void ExitState(DonutStateManager context)
    {

    }

}
