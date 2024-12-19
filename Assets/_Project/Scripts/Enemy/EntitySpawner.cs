using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
	public enum SpawnMode
	{
		Start,
		Loop
	}

	[SerializeField] private Entity[] _entityPrefab;
	[SerializeField] private CubeArea _area;
	[SerializeField] private SpawnMode _spawnMode;
	[SerializeField] private int _numSpawn;
	[SerializeField] private float _respawnTime;

	private float _timer;

	private void Start()
	{
		if (_spawnMode == SpawnMode.Start) SpawnEntities();
		  
		_timer = _respawnTime;
	}

	private void Update()
	{
		if (_timer >= 0) _timer -= Time.deltaTime;

		if (_spawnMode == SpawnMode.Loop && _timer < 0)
		{
			SpawnEntities();
			 
			_timer = _respawnTime;
		}
	}

	private void SpawnEntities()
	{
		for (int i = 0; i < _numSpawn; i++)
		{
			int index = Random.Range(0, _entityPrefab.Length);

			GameObject e = Instantiate(_entityPrefab[index].gameObject);

			e.transform.position = _area.GetRandomInsideZone();
		}

	}
}
