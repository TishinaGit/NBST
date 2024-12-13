using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
	[SerializeField] protected float m_Velocity;

	[SerializeField] private float m_LifeTime;

	[SerializeField] protected int m_Damage;

	[SerializeField] private ImpactEffect m_ImpactEffectPrefab;
	[SerializeField] private ImpactEffect m_ImpactEffectPrefabMetal;

	[SerializeField] private CubeArea m_Area;

	private float m_Timer;

	private void Update()
	{
		float stepLenght = Time.deltaTime * m_Velocity;
		Vector3 step = transform.forward * stepLenght;

		RaycastHit hit;// = Physics.Raycast(transform.position, transform.up, stepLenght);

		if (Physics.Raycast(transform.position, transform.forward, out hit, stepLenght))
		{
			if (hit.collider.isTrigger == false)
			{
				Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

				if (dest != null && dest != m_Parent)
				{
					dest.ApplyDamage(m_Damage, m_Parent);
				}

				OnProjectileLifeEnd(hit.collider, hit.point, hit.normal);
			}
		}


		m_Timer += Time.deltaTime;

		if (m_Timer > m_LifeTime)
		{
			Destroy(gameObject);
		}

		transform.position += new Vector3(step.x, step.y, step.z);
	}

	public virtual void OnProjectileLifeEnd(Collider col, Vector3 pos, Vector3 normal)
	{
		

		if (col is CharacterController)
			return;

		if (m_ImpactEffectPrefab != null)
		{   
			ImpactEffect impact = Instantiate(m_ImpactEffectPrefab, pos, Quaternion.LookRotation(normal));

			impact.transform.SetParent(col.transform);
		}

		Destroy(gameObject);
	}

	protected Destructible m_Parent;

	public virtual void SetParentShooter(Destructible parent)
	{
		m_Parent = parent;
	}
}
