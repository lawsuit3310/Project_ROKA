using System;
using System.Collections;
using UnityEngine;

public class PlayerController : CharacterController
{
    private PlayerSightController _sight;
    protected override void Awake()
    {
        base.Awake();
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<PlayerMoveState>();
        StartCoroutine(PlayerControl());
    }

    private void Start()
    {
        _sight = gameObject.AddComponent<PlayerSightController>();
    }

    IEnumerator PlayerControl()
    {
        bool isMoving = false;
        while (true)
        {
            yield return null;
            if (_sight is null) continue;
            
            this.Object.LookAt(_sight.point);
            
            if (Input.GetMouseButton(0))
            {
                if (!isMoving)
                {
                    Move();
                    isMoving = true;
                }
                MoveStatus.TargetPosition = _sight.point;
            }
            else
            {
                var dis = Vector3.Distance(this.transform.position, MoveStatus.TargetPosition);
                if (dis < 1f)
                {
                    if (isMoving)
                    {
                        Stop();
                        isMoving = false;
                    }
                }
            }
        }
    }
}