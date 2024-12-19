 using UnityEngine;

public class RandomSpawnGamePortal : MonoBehaviour
{
    [SerializeField] private GameObject _gamePortal;
    [SerializeField] private Transform[] _spawnTarget; 
 
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
