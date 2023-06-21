using UnityEngine;

public class DonutLaserState : DonutBaseState
{
    private Vector2 _topPosition;
    private Vector2 _bottomPosition;
    public override void EnterState(DonutStateManager context)
    {
        //_topPosition = new Vector2(context.transform.position.x + 2.5f, context.transform.position.y + 2.5f);
        //_bottomPosition = new Vector2(context.transform.position.x - 2.5f, context.transform.position.y - 2.5f);
    }
    public override void UpdateState(DonutStateManager context)
    {
  
    }
    public override void ExitState(DonutStateManager context)
    {

    }
}
