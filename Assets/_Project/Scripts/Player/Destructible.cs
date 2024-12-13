using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An object to be destroyed on the stage, something that can be destroyed
/// </summary>
public class Destructible : Entity, ISerializableEntity
{
	#region Properties

	/// <summary>
	/// Object ignores damage
	/// </summary>
	[SerializeField] private bool m_Indestructible;
	public bool IsIndestructible => m_Indestructible;

	/// <summary>
	/// Start amount of Hit points
	/// </summary>
	[SerializeField] private int m_HitPoints;
	public int HealthPoints => m_HitPoints;

	/// <summary>
	/// Current amount of Hit points
	/// </summary>
	private int m_CurrentHitPoints;
	public int HitPoints => m_CurrentHitPoints;

	private bool m_IsDeath = false;
	public bool IsDeath => m_IsDeath;

	#endregion


	#region Unity Events

	protected virtual void Start()
	{
		m_CurrentHitPoints = m_HitPoints;
	}

	#endregion

	#region Public API

	public void SetHitPoint(int hitPoint)
	{
		m_CurrentHitPoints = Mathf.Clamp(hitPoint, 0, m_HitPoints);
	}

	/// <summary>
	/// Make damage to object
	/// </summary>
	/// <param name="damage"> Amount of damage </param>
	public void ApplyDamage(int damage, Destructible other)
	{

		if (m_Indestructible || m_IsDeath)
			return;

		m_CurrentHitPoints -= damage;

		OnGetDamage?.Invoke(other);

		m_EventOnGetDamage?.Invoke();

		if (m_CurrentHitPoints <= 0)
		{
			m_IsDeath = true;

			OnDeath();
		}
	}

	public void ApplyHeal(int heal)
	{
		m_CurrentHitPoints += heal;

		if (m_CurrentHitPoints > m_HitPoints)
			m_CurrentHitPoints = m_HitPoints;
	}

	public void HealFull()
	{
		m_CurrentHitPoints = m_HitPoints;
	}

	public void MakeIndestructible(bool b)
	{
		m_Indestructible = b;
	}

	#endregion

	/// <summary>
	/// Destroys object, when hit points are less then 0
	/// </summary>
	protected virtual void OnDeath()
	{
		Destroy(gameObject);
		m_EventOnDeath?.Invoke();
	}


	public static Destructible FindNearest(Vector3 position)
	{
		float minDist = float.MaxValue;
		Destructible target = null;

		foreach (Destructible dest in m_AllDestructibles)
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

		foreach (Destructible dest in m_AllDestructibles)
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

		foreach (Destructible dest in m_AllDestructibles)
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

		foreach (Destructible dest in m_AllDestructibles)
		{
			if (dest.TeamId != teamId)
			{
				teamDestructable.Add(dest);
			}
		}

		return teamDestructable;
	}


	private static HashSet<Destructible> m_AllDestructibles;

	public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

	protected virtual void OnEnable()
	{
		if (m_AllDestructibles == null)
			m_AllDestructibles = new HashSet<Destructible>();

		m_AllDestructibles.Add(this);
	}

	protected virtual void OnDestroy()
	{
		m_AllDestructibles.Remove(this);
	}


	public const int TeamIdNeutral = 0;

	[SerializeField] private int m_TeamId;
	public int TeamId => m_TeamId;


	[SerializeField] private UnityEvent m_EventOnDeath;
	public UnityEvent EventOnDeath => m_EventOnDeath;

	#region Score

	[SerializeField] private int m_ScoreValue;
	public int ScoreValue => m_ScoreValue;

	public long Entity => throw new System.NotImplementedException();

	#endregion
	[SerializeField] private UnityEvent m_EventOnGetDamage;
	public UnityAction<Destructible> OnGetDamage;

	// Serialize

	[System.Serializable]
	public class State
	{
		public Vector3 position;
		public int hitPoints;

		public State() { }
	}

	[SerializeField] private int m_EntityId;
	public long EntityId => m_EntityId;

	public virtual bool IsSerializable()
	{
		return m_CurrentHitPoints > 0;
	}

	public virtual string SerializeState()
	{
		State s = new State();

		s.position = transform.position;
		s.hitPoints = m_CurrentHitPoints;

		return JsonUtility.ToJson(s);
	}

	public virtual void DeserializeState(string state)
	{
		State s = JsonUtility.FromJson<State>(state);

		transform.position = s.position;
		m_HitPoints = s.hitPoints;
	}
}
