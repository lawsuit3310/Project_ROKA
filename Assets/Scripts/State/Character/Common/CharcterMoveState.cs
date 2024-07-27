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
        _controller.MoveStatus.CurrentMovSpd = _controller.MoveStatus.MovSpd == 0 ? MovableConfig.DefaultMovSpd : _controller.MoveStatus.MovSpd;

    }

    protected virtual void Update()
    {
        if (_controller)
        {
            if (_controller.MoveStatus.CurrentMovSpd > 0.1f)
            {
                //  // 기본 이동 알고리즘.  
                var dir = CalcTargetPosition().normalized;
                var displacement = dir * (Time.deltaTime * _controller.MoveStatus.CurrentMovSpd);
                this.transform.Translate(displacement, Space.World);

            }
        }
    }
    protected virtual Vector3 CalcTargetPosition()
    {
        return _controller.MoveStatus.TargetPosition -
               new Vector3(0, _controller.MoveStatus.TargetPosition.y, 0);
    }
}