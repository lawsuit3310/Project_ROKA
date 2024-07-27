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
               //  var v = (CalcTargetPosition().normalized) *
               //          _controller.MoveStatus.CurrentMovSpd;
               //  v *= Time.deltaTime;
               // _controller.transform.Translate(v);

               // _controller.transform.position
               //     = Vector3.Lerp(
               //         _controller.transform.position, _controller.MoveStatus.TargetPosition,
               //         Time.timeScale) ;
               
               var dir = (_controller.MoveStatus.TargetPosition - _controller.transform.position).normalized;
               dir *= (Time.deltaTime * _controller.MoveStatus.CurrentMovSpd);
               this.transform.Translate(dir, Space.World);
               

               //_controller.MoveStatus.CurrentTargetPosition = _controller.MoveStatus.TargetPosition;
            }
        }
    }
    
    protected override Vector3 CalcTargetPosition()
    {

        var dir =  _controller.MoveStatus.TargetPosition - _controller.transform.position;
        // var dir = Vector3.Lerp(
        //     _controller.transform.position,
        //     _controller.MoveStatus.TargetPosition, 0.2f);
        return dir;
    }
}