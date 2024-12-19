using UnityEngine;

public class Weapon : MonoBehaviour
{ 
    [SerializeField] private WeaponProperties _weaponProperties; 
	[SerializeField] private Transform _firePoint;  
    [SerializeField] private AudioSource _audioSource;


    [SerializeField] private WeaponMode _mode;
    public WeaponMode Mode => _mode;

    [SerializeField] private float _primaryMaxEnergy; 
    public float PrimaryMaxEnergy => _primaryMaxEnergy;


    private float _primaryEnergy;
    public float PrimaryEnergy => _primaryEnergy;

    private float _refireTimer;  
    private bool _energyIsRestored;
    public bool CanFire => _refireTimer <= 0 && _energyIsRestored == false;

	 
    private Destructible _owner;
	 
    private void Start()
	{
		_primaryEnergy = _primaryMaxEnergy;

		_owner = transform.root.GetComponent<Destructible>(); 
	}

	protected virtual void Update()
	{
		if (_refireTimer > 0)
			_refireTimer -= Time.deltaTime;

		UpdateEnergy();
	}

	private void UpdateEnergy()
	{
		_primaryEnergy += (float)_weaponProperties.EnergyRegenPerSecond * Time.deltaTime;
		_primaryEnergy = Mathf.Clamp(_primaryEnergy, 0, _primaryMaxEnergy);

		if (_primaryEnergy >= _weaponProperties.EnergyAmountToStartFire)
			_energyIsRestored = false;
	}

	private bool TryDrawEnergy(int count)
	{
		if (count == 0)
		{
			return true;
		}

		if (_primaryEnergy >= count)
		{
			_primaryEnergy -= count;
			return true;
		}

		_energyIsRestored = true;
		return false;
	}

	public void Fire()
	{
		if (_energyIsRestored == true)
			return;

		if (CanFire == false)
			return;

		if (_weaponProperties == null)
			return;
		if (_refireTimer > 0)
			return;

		if (TryDrawEnergy(_weaponProperties.EnergyUsage) == false)
			return;


		Projectile projectile = Instantiate(_weaponProperties.ProjectilePrefab).GetComponent<Projectile>();
		projectile.transform.position = _firePoint.position;
		projectile.transform.forward = _firePoint.forward;

		projectile.SetParentShooter(_owner);

		_refireTimer = _weaponProperties.RateOfFire; 
		{ 
			_audioSource.clip = _weaponProperties.LaunchSFX;
			_audioSource.Play();
		}
	}

	public void FirePointLookAt(Vector3 pos)
	{
		Vector3 offset = Random.insideUnitSphere * _weaponProperties.SpreadShotRange;

		if (_weaponProperties.SpreadShotDistanceFactor != 0)
		{
			offset = offset * Vector3.Distance(_firePoint.position, pos) * _weaponProperties.SpreadShotDistanceFactor;
		}

		_firePoint.LookAt(pos + offset);
	}

    public void AssignLoadout(WeaponProperties props)
	{
		if (Mode != props.Mode)
			return;

		_refireTimer = 0;
		_weaponProperties = props;
	}
}
