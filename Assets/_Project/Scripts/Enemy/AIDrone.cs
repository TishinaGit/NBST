using Unity.VisualScripting;
using UnityEngine;

public class AIDrone : MonoBehaviour
{
    [SerializeField] private CubeArea _movementArea;
    [SerializeField] private float _angryDistance;

    private Drone _drone;
    private Vector3 _movementPosition;
    private Transform _shootTarget;
    private Transform _player;

    private void Start()
    {
        _drone = GetComponent<Drone>();
        _drone.EventOnDeath.AddListener(OnDroneDeath);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _movementArea = FindAnyObjectByType<CubeArea>();
    }

    private void OnDestroy()
    {
        _drone.EventOnDeath.RemoveListener(OnDroneDeath);
    }

    private void Update()
    {
        UpdateAI();
    }

    private void OnDroneDeath()
    {
        enabled = false;
    }

    private void UpdateAI()
    {
        if (transform.position == _movementPosition)
        {
            _movementPosition = _movementArea.GetRandomInsideZone();
        }

        if (Physics.Linecast(transform.position, _movementPosition) == true) 
        {
            _movementPosition = _movementArea.GetRandomInsideZone();
        }

        if (Vector3.Distance(transform.position, _player.position) < _angryDistance)
        {
            _shootTarget = _player;
        }

        _drone.MoveTo(_movementPosition);

        if (_shootTarget != null )
        {
            _drone.LookAt(_shootTarget.position);
        }
        else
        {
            _drone.LookAt(_movementPosition);
        }

        if (_shootTarget != null )
        {
            _drone.Fire(_shootTarget.position);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _angryDistance);
    }
#endif
}