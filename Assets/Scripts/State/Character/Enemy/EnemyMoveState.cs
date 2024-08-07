using UnityEngine;

public class EnemyMoveState : CharacterMoveState
{
    private EnemyController enmCon;
    protected override Vector3 CalcTargetPosition()
    {
        var dir = _controller.moveStatistics.TargetPosition;
        if (_controller is EnemyController)
        {
            enmCon = (EnemyController)_controller;
            dir =  enmCon.sight.point -  enmCon.transform.position;
        }
        return dir;
    }
}