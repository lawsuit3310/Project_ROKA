using UnityEngine;

public class EnemyMoveState : CharacterMoveState
{
    protected override Vector3 CalcTargetPosition()
    {
        var dir = _controller.MoveStatus.TargetPosition;
        return dir;
    }
}