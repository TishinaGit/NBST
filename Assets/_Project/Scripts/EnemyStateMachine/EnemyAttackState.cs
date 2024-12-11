using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _player;
    public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
    {
        _agent = agent;
        _player = player;
    }

    public override void OnEnter()
    {
        
        _animator.CrossFade(AttackHash, _blendSpeed);
    }

    public override void Update()
    {
        _agent.SetDestination(_player.position);
        _enemy.Attack();
    }

}