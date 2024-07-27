using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : CharacterController
{
    protected override void Awake()
    {
        base.Awake();
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<PlayerMoveState>();
        StartCoroutine(PlayerControl());
    }

    private void Start()
    {
        Sight = gameObject.AddComponent<PlayerSightController>();
        using (var factory = new EquipmentFactory())
        {
            Equip = factory.CreateEquipment(0, this);
        }
    }

    IEnumerator PlayerControl()
    {
        bool isMoving = false;
        while (true)
        {
            yield return null;
            if (Sight is null) continue;
            
            this.Object.LookAt(Sight.point);

            if (Input.GetMouseButtonDown(1))
            {
                Attack();
            }
            
            if (Input.GetMouseButton(0))
            {
                if (!isMoving)
                {
                    Move();
                    isMoving = true;
                }
                MoveStatus.TargetPosition = Sight.point;
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