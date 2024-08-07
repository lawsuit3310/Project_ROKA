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
        Destroy(_stopState as CharcterStopState);
        _moveState = gameObject.AddComponent<PlayerMoveState>();
        _stopState = gameObject.AddComponent<PlayerStopState>();
        StartCoroutine(PlayerControl());
    }

    private void Start()
    {
        sight = gameObject.AddComponent<PlayerSightController>();
        using (var factory = new EquipmentFactory())
        {
            Equip = factory.CreateEquipment(0, this);
        }
    }
    
    private void OnGUI()
    {
        if (GUILayout.Button("Damage 1 to Player"))
        {
            GiveDamage(this);
        }
    }

    IEnumerator PlayerControl()
    {
        bool isMoving = false;
        while (true)
        {
            yield return null;
            if (sight is null) continue;
            
            this.Object.LookAt(sight.point);

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
                moveStatistics.TargetPosition = sight.point;
            }
            else
            {
                var dis = Vector3.Distance(this.transform.position, moveStatistics.TargetPosition);
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