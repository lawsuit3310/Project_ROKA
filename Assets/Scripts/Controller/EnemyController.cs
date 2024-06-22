using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
    }

    private void OnEnable()
    {   
        Move();
        StartCoroutine(FollowPlayer());
    }

    private void OnDisable()
    {
        ResetEnemy();
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
    
    IEnumerator FollowPlayer()
    {
        Player = !Player ? FindObjectOfType<PlayerController>() : Player;
        while(true)
        {
            if (!_isAttacking)
                MoveStatus.TargetPosition = Player.transform.position - this.transform.position;
            else
            {
                //TODO
                // 공격 상태 진입
            }
            yield return null;
        }
    }
}