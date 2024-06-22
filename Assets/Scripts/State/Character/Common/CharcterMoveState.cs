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
        _controller.MoveStatus.CurrentMovSpd = MovableConfig.MovSpd;
        
        //방향 전환은 이걸 상속 받은 후, EnemyMoveState, 혹은 PlayerMoveState 에서 확장 구현
    }

    protected void Update()
    {
        if (_controller)
        {
            if (_controller.MoveStatus.CurrentMovSpd > 0)
            {
                // 기본 이동 알고리즘.  
                this.transform.LookAt(CalcTargetPosition());
                var v = (Vector3.forward) *
                        _controller.MoveStatus.CurrentMovSpd;
                v *= Time.deltaTime;
                
                _controller.transform.Translate(v);
            }
        }
    }

    protected virtual Vector3 CalcTargetPosition()
    {
        return _controller.MoveStatus.CurrentTargetPosition -
               new Vector3(0, _controller.MoveStatus.CurrentTargetPosition.y, 0);
    }
}