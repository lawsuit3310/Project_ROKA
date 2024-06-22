using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : CharacterController
{
    public static PlayerController Player;

    private bool _isAttacking = false;
    protected override void Start()
    {
        base.Start();
        
        Player = !Player ? FindObjectOfType<PlayerController>() : Player; 
        
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<EnemyMoveState>();
        
        Move();
        StartCoroutine(FollowPlayer());
    }

    IEnumerator FollowPlayer()
    {
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