using UnityEngine;

public class Projectile : Entity
{
	[SerializeField] protected float _velocity;

	[SerializeField] private float _lifeTime;

	[SerializeField] protected int _damage; 

	private float _timer;

	private void Update()
	{
		float stepLenght = Time.deltaTime * _velocity;
		Vector3 step = transform.forward * stepLenght;

		RaycastHit hit;// = Physics.Raycast(transform.position, transform.up, stepLenght);

		if (Physics.Raycast(transform.position, transform.forward, out hit, stepLenght))
		{
			if (hit.collider.isTrigger == false)
			{
				Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

				if (dest != null && dest != m_Parent)
				{
					dest.ApplyDamage(_damage, m_Parent);
				} 
			}
		} 

		_timer += Time.deltaTime;

		if (_timer > _lifeTime)
		{
			Destroy(gameObject);
		}

		transform.position += new Vector3(step.x, step.y, step.z);
	} 
	protected Destructible m_Parent;

	public virtual void SetParentShooter(Destructible parent)
	{
		m_Parent = parent;
	}
}
