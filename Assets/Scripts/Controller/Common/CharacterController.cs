using System;
using UnityEngine;

public class CharacterController : IController
{
    protected ICharacterState _stopState, _moveState, _attackState, _beatenState;
    protected Transform Object;
    private ICharacterContext _context;
    
    public Equipment Equip;
    public CharacterSightController Sight;
    public MovableConfig MoveStatus;
    
    

    protected virtual void Awake()
    {
        _context = new CharacterContext(this);

        _attackState = gameObject.AddComponent<CharacterAttackState>();
        _stopState = gameObject.AddComponent<CharcterStopState>();
        _moveState = gameObject.AddComponent<CharacterMoveState>();

        Object = gameObject.GetComponentInChildren<Transform>();

        
    }

    public void Move()
    {
        _context.Transition(_moveState);
    }

    public void Stop()
    {
        _context.Transition(_stopState);
    }

    public void Attack()
    {
        if (_context is CharacterContext)
        {
            //공격 이전 상태를 찾음
            var crnt = ((CharacterContext)_context).CurrentState;
            _context.Transition(_attackState);
            //공격 이전 상태로 복귀
            _context.Transition(crnt ?? _stopState);
        }
        else
            _context.Transition(_attackState);
    }
}