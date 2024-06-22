using UnityEngine;

public class EnemyMoveState : CharacterMoveState
{
    protected override Vector3 CalcTargetPosition()
    {
        _controller.MoveStatus.CurrentTargetPosition =
            EnemyController.Player.transform.position;
        return base.CalcTargetPosition();
    }
}