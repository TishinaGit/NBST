using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    private readonly NavMeshAgent _agent;
    private readonly Vector3 _startPoint;
    private readonly float _patrolRange;

    public EnemyPatrolState(Enemy enemy, Animator animator, NavMeshAgent agent ,float patrolRange) : base(enemy, animator)
    {
        _agent = agent;
        _patrolRange = patrolRange;
        _startPoint = enemy.transform.position;
    }

    public override void OnEnter()
    {
        _animator.CrossFade(RunHash, _blendSpeed);
    }

    public override void Update()
    {
        if (HasReachedDestination())
        {
            var randomDirection = Random.insideUnitSphere * _patrolRange;
            randomDirection += _startPoint;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, _patrolRange, 1);
            var finalPosition = hit.position;

            _agent.SetDestination(finalPosition);
        }


    }

    private bool HasReachedDestination()
    {
        return !_agent.pathPending
            && _agent.remainingDistance <= _agent.stoppingDistance
            && (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
    }
}