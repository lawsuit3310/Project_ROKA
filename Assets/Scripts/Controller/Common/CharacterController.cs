using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : IController, IFightable
{
    protected ICharacterState _stopState, _moveState, _attackState, _beatenState, _deadState;
    protected Transform Object;
    private ICharacterContext _context;
    [FormerlySerializedAs("Statistics")] [SerializeField]
    private FightableConfig _statistics;
    
    public Equipment Equip;
    public CharacterSightController sight;
    public Animator anim;
    [FormerlySerializedAs("MoveStatistics")] public MovableConfig moveStatistics;
    public FightableConfig Statistics => _statistics;
    
    
    protected virtual void Awake()
    {
        _context = new CharacterContext(this);

        _attackState = gameObject.AddComponent<CharacterAttackState>();
        _stopState = gameObject.AddComponent<CharcterStopState>();
        _moveState = gameObject.AddComponent<CharacterMoveState>();
        _beatenState = gameObject.AddComponent<CharacterBeatenState>();
        _deadState = gameObject.AddComponent<CharacterDeadState>();

        Object = gameObject.GetComponentInChildren<Transform>();
        _statistics.CurrentHitPoint = _statistics.HitPoint;

        anim = transform.GetComponentInChildren<Animator>();
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
    

    public void GiveDamage(CharacterController caller)
    {
        //todo  체력이 0이 되었을 때
        if (!_statistics.ModifyHealth(-caller.Equip.Config.AttackPoint))
        {
            OnDead();
        }
        else
        {
            //todo 처 맞았는데 0이 아닐때 
            OnDamaged();
        }
    }

    public void Heal(float amount)
    {
        //todo 치유 받았을 때, 당장은 구현  x
    }

    public virtual void OnDamaged()
    {
        _context.Transition(_beatenState);
        
    }

    public virtual void OnDead()
    {
        if (_context is CharacterContext)
        {
            //공격 이전 상태를 찾음
            var crnt = ((CharacterContext)_context).CurrentState;
            _context.Transition(_deadState);
            //공격 이전 상태로 복귀
            _context.Transition(crnt ?? _stopState);
        }
        else
            _context.Transition(_deadState);
    }
    
    
}