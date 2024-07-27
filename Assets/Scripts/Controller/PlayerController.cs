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
        bool isMoving = false, isInput = false;
        while (true)
        {
            yield return null;
            if (_sight is null) continue;
            if (Input.GetMouseButton(1))
            {
                if (!isMoving) Move();
                MoveStatus.TargetPosition = _sight.point;
            }
            else
            {
                if (Vector3.Distance(this.transform.position, _sight.point) < 0.001f )
                {
                    if (!isMoving) Stop();
                }
            }
        }
    }
}