using Controller;
using UnityEngine;

public abstract class EnemyBaseState : IState
{
    protected readonly Enemy _enemy;
    protected readonly Animator _animator;

    protected static readonly int idleHash = Animator.StringToHash("Idle");
    protected static readonly int RunHash = Animator.StringToHash("Run");
    protected static readonly int AttackHash = Animator.StringToHash("Attack");

    protected const float _blendSpeed = 4f;
    protected EnemyBaseState(Enemy enemy, Animator animator)
    {
        _enemy = enemy;
        _animator = animator;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void OnExit()
    {
        
    }

}
