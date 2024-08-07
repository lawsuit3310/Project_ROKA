using System;
using UnityEngine;

public class CharacterMoveState : MonoBehaviour, ICharacterState
{
    protected CharacterController _controller;

    public void Handle(IController controller)
    {
        if (!_controller)
        {
            _controller = (CharacterController)controller;
        }           
        _controller.moveStatistics.CurrentMovSpd = _controller.moveStatistics.MovSpd == 0 ? MovableConfig.DefaultMovSpd : _controller.moveStatistics.MovSpd;

    }

    protected virtual void Update()
    {
        if (_controller && _controller.Statistics.CurrentHitPoint > 0)
        {
            if (_controller.moveStatistics.CurrentMovSpd > 0.1f)
            {
                //  // 기본 이동 알고리즘.  
                var dir = CalcTargetPosition().normalized;
                var displacement = dir * (Time.deltaTime * _controller.moveStatistics.CurrentMovSpd);
                this.transform.Translate(displacement, Space.World);

            }
        }
    }
    protected virtual Vector3 CalcTargetPosition()
    {
        return _controller.moveStatistics.TargetPosition -
               new Vector3(0, _controller.moveStatistics.TargetPosition.y, 0);
    }
}