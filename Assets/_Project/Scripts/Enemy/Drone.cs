 using UnityEngine;
 
public class Drone : Destructible
{
	[Header("Main")]
	[SerializeField] private Transform _mainMesh;
    [SerializeField] private Weapon[] _weapons;

	[Header("View")]
	[SerializeField] private GameObject[] _meshComponents;
    [SerializeField] private Renderer[] _meshRenderers;
    [SerializeField] protected Material[] _deadMaterials;
     
    [Header("Movement")]
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float _rotationLerpFactor;
    [SerializeField] protected float _hoverAmplitude;
    [SerializeField] protected float _hoverSpeed;

    private Vector3 _heightTargetPos;
    public Transform MainMesh => _mainMesh;
     
     
    protected override void OnDeath()
    {
        EventOnDeath?.Invoke();

        enabled = false;

        for (int i = 0;  i < _meshComponents.Length; i++)
        {
            if (_meshComponents[i].GetComponent<Rigidbody>() == null)
            {
                _meshComponents[i].AddComponent<Rigidbody>();
            }
        }

        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = _deadMaterials[i];
            
        }
    }
     
    public void LookAt(Vector3 target)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target - transform.position, Vector3.up), Time.deltaTime * _rotationLerpFactor);
    }

    public void MoveTo(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _movementSpeed);
    }

    public void Fire(Vector3 target)
    {
        _heightTargetPos.y = 0.5f;

        for (int i = 0; i < _weapons.Length; i++) 
        {
            _weapons[i].FirePointLookAt(target + _heightTargetPos);
            _weapons[i].Fire();
        }
    }
}
