using UnityEngine;



public partial class PlayerHealth : MonoBehaviour
{
    [SerializeField] private UIHPBarUpdate _uiHPBar;

    [SerializeField] private float _maxPlayerHealth = 100f;
    [SerializeField] private float _currentHealth;

    public bool IsDead => _currentHealth <= 0;

    void Awake()
    {
        _currentHealth = _maxPlayerHealth;
        if(_uiHPBar != null)
        {
            _uiHPBar.HPBarUpdate(_currentHealth, _maxPlayerHealth);
        }
        
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _uiHPBar.HPBarUpdate(_currentHealth, _maxPlayerHealth);
        Dead();
    }
    public void Dead()
    {
        if (IsDead == true)
        {
            //Заглушка
            Debug.Log("DeadPlayer");
        }
    }

   
}
