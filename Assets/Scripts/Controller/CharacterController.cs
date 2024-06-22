using System;
using UnityEngine;

public class CharacterController : IController
{
    protected ICharacterState _stopState, _moveState, _attackState, _beatenState;
    private ICharacterContext _context;
    
    public FightableConfig FightableConfig;
    public MovableConfig MoveStatus;

    protected virtual void Awake()
    {
        _context = new CharacterContext(this);

        _stopState = gameObject.AddComponent<CharcterStopState>();
        _moveState = gameObject.AddComponent<CharacterMoveState>();
    }

    public void Move()
    {
        _context.Transition(_moveState);
    }

    public void Stop()
    {
        _context.Transition(_stopState);
    }
}