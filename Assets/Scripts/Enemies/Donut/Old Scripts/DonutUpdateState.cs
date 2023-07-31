using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DonutUpdateState : DonutBaseState
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
        _zeroStop = context.zeroStop;
        _firstStop = context.firstStop;
        _secondStop = context.secondStop;

    }
    public override void UpdateState(DonutStateManager context)
    {
        switch (_currentPosition)
        {
           
            case 0:
                context.transform.position = Vector2.MoveTowards(context.transform.position, _firstStop.position, _speed * Time.deltaTime);
                if (Vector3.Distance(context.transform.position, _firstStop.position) <= 0.1f)
                {
                    _currentPosition = 1;
                    Debug.Log("0 t o 1");
                    

                }
                break;

             case 1:
                 context.transform.position = Vector2.MoveTowards(context.transform.position, _secondStop.position, _speed * Time.deltaTime);
                 if (Vector3.Distance(context.transform.position, _secondStop.position) <= 0.1f)
                 {
                    _currentPosition = 2;
                    Debug.Log("1 t o 2");

                }
                 break;

             case 2:
                 context.transform.position = Vector2.MoveTowards(context.transform.position, _zeroStop.position, _speed * Time.deltaTime);
                 if (Vector3.Distance(context.transform.position, _zeroStop.position) <= 0.1f)
                 {
                    
                     if (GameManager.Instance.IsPlayerDead())
                     {
                        context.DestroyObject();
                     }
                     else
                     {
                        amountOfRounds++;
                        _currentPosition = 0;
                        context.transform.position = _zeroStop.position;
                        Debug.Log("2 t o 0");
                        break;
                        
                    }
                 }
                 break;
         
          
        }
    }
    

    public override void ExitState(DonutStateManager context)
    {
    }

}
