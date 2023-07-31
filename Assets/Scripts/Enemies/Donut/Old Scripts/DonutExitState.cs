using UnityEngine;

public class DonutExitState : DonutBaseState
{
    private Vector2 _donutInitialPosition;
    private Vector2 _endPosition;
    private float _desiredDuration = 5f;
    private float _currentTime;
    public override void EnterState(DonutStateManager context)
    {
        _endPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) + new Vector3(2f, 2f);
        _endPosition.y *= -1;

        _donutInitialPosition = new Vector2(0f, 0f);
    }
    public override void UpdateState(DonutStateManager context)
    {
        Debug.Log("exit");
        _currentTime += Time.deltaTime;
        float _percentageComplete = _currentTime / _desiredDuration;
        context.transform.position = Vector2.Lerp(_donutInitialPosition, _endPosition, _percentageComplete);

        if (_currentTime >= _desiredDuration)
        {
            context.DestroyObject();
        }
    }
    public override void ExitState(DonutStateManager context)
    {

    }
}
