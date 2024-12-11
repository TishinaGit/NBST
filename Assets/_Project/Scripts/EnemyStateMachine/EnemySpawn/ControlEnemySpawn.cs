using System.Collections.Generic;
using UnityEngine;

public class ControlEnemySpawn : MonoBehaviour
{
    [SerializeField] private List<EnemyHealth> _enemyHealth;
    [SerializeField] private List<EnemySpawn> _enemySpwan;

    public int _maxEnemyCount = 10;
    public int _minEnemyCount = 0;
    public int _currentEnemyCount;
    private void Update()
    {
        
    }
    public void TakeCommponent(EnemyHealth enemyHealth)
    {
        _enemyHealth.Add(enemyHealth);
    }

    public void MinusEnemy()
    {
        for (int i = 0; i < _enemyHealth.Count; i++)
        {
            if (_enemyHealth[i].IsDead == true)
            {
                _currentEnemyCount--;
            }
        }
    }

 
}
