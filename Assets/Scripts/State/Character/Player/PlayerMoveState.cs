using UnityEngine;

public class PlayerMoveState : CharacterMoveState
{
    protected override Vector3 CalcTargetPosition()
    {
        var dir =  _controller.moveStatistics.TargetPosition - _controller.transform.position;
        return dir;
    }
}