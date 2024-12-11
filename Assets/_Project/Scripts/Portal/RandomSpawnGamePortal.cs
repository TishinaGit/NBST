using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnGamePortal : MonoBehaviour
{
    [SerializeField] private GameObject _gamePortal;
    [SerializeField] private Transform[] _spawnTarget;
    private Transform g;
 
    private void RandomSpawn()
    {
        
        int randomPoint = Random.Range(0, _spawnTarget.Length);
        Instantiate(_gamePortal, _spawnTarget[randomPoint].transform);
       
    }

    private void Start()
    {
        RandomSpawn();
    }
}
