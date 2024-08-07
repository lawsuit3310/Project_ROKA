using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class EnemyController : CharacterController
{
    private bool _isAttacking = false;
    
    public static PlayerController Player;

    public IObjectPool<EnemyController> Pool;
    
    protected override void Awake()
    {
        base.Awake();
        
        Player = !Player ? FindObjectOfType<PlayerController>() : Player;
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<EnemyMoveState>();
        
        sight = gameObject.AddComponent<EnemySightController>();
    }

    private void OnEnable()
    {
        if (Vector3.Distance(this.transform.position, Player.transform.position) < 10)
        {
            switch (Random.Range(0,4))
            {
                case 0:
                    this.transform.position += Vector3.right * 25;
                    break;
                case 1:
                    this.transform.position += Vector3.left * 25;
                    break;
                case 2:
                    this.transform.position += Vector3.back * 25;
                    break;
                case 3:
                    this.transform.position += Vector3.forward * 25;
                    break;
            }
        }
        Move();
    }

    private void OnDisable()
    {
        ResetEnemy();
    }

    public override void OnDamaged()
    {
        base.OnDamaged();
    }

    private void ResetEnemy()
    {
        //적 체력등 재설정
        Stop();
    }
    

    public void ReturnToPool()
    {
        Pool.Release(this);
    }

    
    
}