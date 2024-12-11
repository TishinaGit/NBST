using Unity.VisualScripting;
using UnityEngine;

public  class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _damageEnemy;
    [SerializeField] private PlayerHealth PlayerHealth;

    private void Awake()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "Player")
            {
                PlayerHealth.TakeDamage(_damageEnemy);
            }
        }
    }
    
}
