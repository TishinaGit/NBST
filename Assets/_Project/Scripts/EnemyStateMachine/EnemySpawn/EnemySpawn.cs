using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{ 
    [SerializeField] private EnemyHealth _enemyInstantiate;
    [SerializeField] private Transform _targetSpawn;
    [SerializeField] private ControlEnemySpawn _controlEnemySpawn;
    [SerializeField] private float _time = 6f;
    [SerializeField] private bool _isColdown = true;
    
    private void Update()
    {
        SpawnEnemy(); 
    }

    private void SpawnEnemy()
    {
        if (_isColdown == true && _time >= 6f && _controlEnemySpawn._currentEnemyCount < _controlEnemySpawn._maxEnemyCount)
        { 
            Instantiate(_enemyInstantiate, _targetSpawn.transform);
            _enemyInstantiate = GetComponentInChildren<EnemyHealth>();
            _controlEnemySpawn.TakeCommponent(_enemyInstantiate);

            _controlEnemySpawn._currentEnemyCount++;
            _controlEnemySpawn.MinusEnemy();

            _isColdown = false;
            _time = 0;
        }
        else
        {
            if (_time != 6 && _controlEnemySpawn._currentEnemyCount < _controlEnemySpawn._maxEnemyCount)
            {
                

                _time += Time.deltaTime;
                _isColdown = true;
            } 
        }
    } 
}
