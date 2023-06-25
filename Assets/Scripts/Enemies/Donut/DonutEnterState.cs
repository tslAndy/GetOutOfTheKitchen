
using UnityEngine;

public class DonutEnterState : DonutBaseState
{
    private Vector2 _donutInitialPosition;
    private Vector2 _endPosition;
    private Vector2 _endPositionn;
    private float _desiredDuration = 5f;
    private float _currentTime;
    public override void EnterState(DonutStateManager context)
    {
        _donutInitialPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) + new Vector3(2f,2f);
        _endPosition = new Vector2(0f, 0f);

        Debug.Log(_endPositionn);

    }
    public override void UpdateState(DonutStateManager context)
    {
        _currentTime += Time.deltaTime;
        float _percentageComplete = _currentTime / _desiredDuration;
        context.transform.position = Vector2.Lerp(_donutInitialPosition, _endPosition, _percentageComplete);
        Debug.Log("movinf moving");
        if(_currentTime >= _desiredDuration)
        {
            _currentTime = 0f;
            context.SwitchState(context.donutLaserState);
        }
        

    }
    public override void ExitState(DonutStateManager context)
    {
       
    }
}
