using System;
using System.Collections;
using UnityEngine;

public class PlayerController_Legacy  : CharacterController
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
        bool isMoving = false, isInput = false;
        while (true)
        {
            isInput = false;
            
            if (Input.GetKey(KeyCode.W))
            {
                isInput = true;
                MoveStatus.TargetPosition.z = 10;
            }
            if (Input.GetKey(KeyCode.S))
            {
                isInput = true;
                MoveStatus.TargetPosition.z = - 10;
            }
            if (Input.GetKey(KeyCode.A))
            {
                isInput = true;
                MoveStatus.TargetPosition.x = - 10;
            }
            if (Input.GetKey(KeyCode.D))
            {
                isInput = true;
                MoveStatus.TargetPosition.x = 10;
            }
            
            if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                MoveStatus.TargetPosition.z = 0;
            if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                MoveStatus.TargetPosition.x = 0;
            
            if (!isInput)
            {
                if(isMoving)
                {
                    isMoving = false;
                    Stop();
                    MoveStatus.TargetPosition = Vector3.zero;
                }
                yield return null;
            }
            else
            {
                if (!isMoving)
                {
                    isMoving = true;
                    Move();
                }
                yield return null;
            }
        }
    }
}