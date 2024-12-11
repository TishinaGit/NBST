using Platformer;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PlayerDetector))]
public class Enemy : Entity
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerDetector _playerDetector;



    public float _patrolRange = 10f;
    private float _timeBetweenAttacks = 1f;
    private StateMachine _stateMachine;

    CountdownTimer attackTimer;

    private void Start()
    {
        attackTimer = new CountdownTimer(_timeBetweenAttacks);
        _stateMachine = new StateMachine();

        var patrolState = new EnemyPatrolState(this, _animator, _agent, _patrolRange);
        var followState = new EnemyFollowState(this, _animator, _agent, _playerDetector.Player);
        var attackState = new EnemyAttackState(this, _animator, _agent, _playerDetector.Player);

        At(patrolState, followState, new FuncPredicate(() => _playerDetector.CanDetectPlayer()));
        At(followState, patrolState, new FuncPredicate(() => !_playerDetector.CanDetectPlayer()));
        At(followState, attackState, new FuncPredicate(() => _playerDetector.CanAttackPlayer()));
        At(attackState, followState, new FuncPredicate(() => !_playerDetector.CanAttackPlayer()));
        //Any(patrolState, new FuncPredicate(() => true));

        _stateMachine.SetState(patrolState);
    }

    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

    private void Update()
    {
        _stateMachine.Update();
        attackTimer.Tick(Time.deltaTime);
    }



    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void Attack()
    {
        if (attackTimer.IsRunning) return;

        attackTimer.Start();
    }

  
}
