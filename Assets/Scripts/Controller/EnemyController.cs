using System;
using UnityEngine;
public class EnemyController : CharacterController
{
    public static PlayerController Player;
    protected override void Start()
    {
        base.Start();
        
        Player = !Player ? FindObjectOfType<PlayerController>() : Player; 
        
        Destroy(_moveState as CharacterMoveState);
        _moveState = gameObject.AddComponent<EnemyMoveState>();
        
        Move();
    }
}