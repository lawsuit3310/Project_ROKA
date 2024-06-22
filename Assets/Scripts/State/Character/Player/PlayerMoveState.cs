using UnityEngine;

public class PlayerMoveState : CharacterMoveState
{
    protected override Vector3 CalcTargetPosition()
    {
        var dir = Vector3.Lerp(
            _controller.MoveStatus.CurrentTargetPosition,
            _controller.MoveStatus.TargetPosition, 0.2f);
        return dir;
    }
}