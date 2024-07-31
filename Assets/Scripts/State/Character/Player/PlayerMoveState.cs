using UnityEngine;

public class PlayerMoveState : CharacterMoveState
{
    protected override Vector3 CalcTargetPosition()
    {
        var dir =  _controller.MoveStatistics.TargetPosition - _controller.transform.position;
        return dir;
    }
}