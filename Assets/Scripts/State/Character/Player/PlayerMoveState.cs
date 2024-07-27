using UnityEngine;

public class PlayerMoveState : CharacterMoveState
{
    protected override void Update()
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
    
    protected override Vector3 CalcTargetPosition()
    {
        var dir =  _controller.MoveStatus.TargetPosition - _controller.transform.position;
        return dir;
    }
}