using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxEnemyHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private GameObject _drop;
    [SerializeField] private Transform _spawnDrop;

    private bool _spawn = true;
    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = maxEnemyHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        Dead();
    }

    public void Dead()
    {
        if (IsDead == true)
        {
            SpawnDrop();
            Destroy(gameObject);
        }
    }

    

    private void SpawnDrop()
    {
        if (IsDead == true && _spawn == true)
        {
            _spawn = false;
            Instantiate(_drop);
            _drop.transform.position = _spawnDrop.transform.position + new Vector3(0, 2, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            TakeDamage(20);
        }
    }
}
