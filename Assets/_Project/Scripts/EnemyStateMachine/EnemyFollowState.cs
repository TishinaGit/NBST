using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    private readonly NavMeshAgent _agent;
    private readonly Transform _player;

    public EnemyFollowState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
    {
        _agent = agent;
        _player = player;
    }

    public override void OnEnter()
    {
        _animator.CrossFade(RunHash, _blendSpeed);
    }

    public override void Update()
    {
        _agent.SetDestination(_player.position);
    }
}