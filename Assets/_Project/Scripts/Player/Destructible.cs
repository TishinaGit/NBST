using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructible : Entity, ISerializableEntity
{
	#region Properties

	 
	[SerializeField] private bool _indestructible;
	public bool IsIndestructible => _indestructible;

	 
	[SerializeField] private int _hitPoints;
	public int HealthPoints => _hitPoints;

	 
	private int _currentHitPoints;
	public int HitPoints => _currentHitPoints;

	private bool _isDeath = false;
	public bool IsDeath => _isDeath;

	#endregion


	#region Unity Events

	protected virtual void Start()
	{
		_currentHitPoints = _hitPoints;
	}

	#endregion

	#region Public API

	public void SetHitPoint(int hitPoint)
	{
		_currentHitPoints = Mathf.Clamp(hitPoint, 0, _hitPoints);
	}

	 
	public void ApplyDamage(int damage, Destructible other)
	{

		if (_indestructible || _isDeath)
			return;

		_currentHitPoints -= damage;

		OnGetDamage?.Invoke(other);

		_eventOnGetDamage?.Invoke();

		if (_currentHitPoints <= 0)
		{
			_isDeath = true;

			OnDeath();
		}
	}

	public void ApplyHeal(int heal)
	{
		_currentHitPoints += heal;

		if (_currentHitPoints > _hitPoints)
			_currentHitPoints = _hitPoints;
	}

	public void HealFull()
	{
		_currentHitPoints = _hitPoints;
	}

	public void MakeIndestructible(bool b)
	{
		_indestructible = b;
	}

	#endregion

	 
	protected virtual void OnDeath()
	{
		Destroy(gameObject);
		_eventOnDeath?.Invoke();
	}


	public static Destructible FindNearest(Vector3 position)
	{
		float minDist = float.MaxValue;
		Destructible target = null;

		foreach (Destructible dest in _allDestructibles)
		{
			float curDist = Vector3.Distance(dest.transform.position, position);

			if (curDist < minDist)
			{
				minDist = curDist;
				target = dest;
			}
		}

		return target;
	}

	public static Destructible FindNearestNonTeamMember(Destructible destructible)
	{
		float minDist = float.MaxValue;
		Destructible target = null;

		foreach (Destructible dest in _allDestructibles)
		{
			float curDist = Vector3.Distance(dest.transform.position, destructible.transform.position);

			if (curDist < minDist && destructible.TeamId != dest.TeamId)
			{
				minDist = curDist;
				target = dest;
			}
		}

		return target;
	}

	public static List<Destructible> GetAllTeamMember(int teamId)
	{
		List<Destructible> teamDestructable = new List<Destructible>();

		foreach (Destructible dest in _allDestructibles)
		{
			if (dest.TeamId == teamId)
			{
				teamDestructable.Add(dest);
			}
		}

		return teamDestructable;
	}

	public static List<Destructible> GetAllNonTeamMember(int teamId)
	{
		List<Destructible> teamDestructable = new List<Destructible>();

		foreach (Destructible dest in _allDestructibles)
		{
			if (dest.TeamId != teamId)
			{
				teamDestructable.Add(dest);
			}
		}

		return teamDestructable;
	}


	private static HashSet<Destructible> _allDestructibles;

	public static IReadOnlyCollection<Destructible> AllDestructibles => _allDestructibles;

	protected virtual void OnEnable()
	{
		if (_allDestructibles == null)
			_allDestructibles = new HashSet<Destructible>();

		_allDestructibles.Add(this);
	}

	protected virtual void OnDestroy()
	{
		_allDestructibles.Remove(this);
	}


	public const int TeamIdNeutral = 0;

	[SerializeField] private int _teamId;
	public int TeamId => _teamId;


	[SerializeField] private UnityEvent _eventOnDeath;
	public UnityEvent EventOnDeath => _eventOnDeath;

	#region Score

	[SerializeField] private int _scoreValue;
	public int ScoreValue => _scoreValue;

	public long Entity => throw new System.NotImplementedException();

	#endregion
	[SerializeField] private UnityEvent _eventOnGetDamage;
	public UnityAction<Destructible> OnGetDamage;

	// Serialize

	[System.Serializable]
	public class State
	{
		public Vector3 position;
		public int hitPoints;

		public State() { }
	}

	[SerializeField] private int _entityId;
	public long EntityId => _entityId;

	public virtual bool IsSerializable()
	{
		return _currentHitPoints > 0;
	}

	public virtual string SerializeState()
	{
		State s = new State();

		s.position = transform.position;
		s.hitPoints = _currentHitPoints;

		return JsonUtility.ToJson(s);
	}

	public virtual void DeserializeState(string state)
	{
		State s = JsonUtility.FromJson<State>(state);

		transform.position = s.position;
		_hitPoints = s.hitPoints;
	}
}
