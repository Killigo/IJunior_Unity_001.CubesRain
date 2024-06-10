using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Platform _mainPlatform;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _spawnHeight;

    private ObjectPool<Cube> _pool;
    private Coroutine _coroutine;
    private Vector3 _platformSize;

    private void Start()
    {
        _pool = new ObjectPool<Cube>(_prefab, transform, 10);
        _coroutine = StartCoroutine(RainOfCube());
        _platformSize = _mainPlatform.GetSize();
    }

    private void OnDestroy()
    {
        _pool.Reset();
        StopCoroutine(_coroutine);
    }

    private void OnDestroyed(Cube cube)
    {
        cube.Destroyed -= OnDestroyed;
        _pool.PutObject(cube);
    }

    private IEnumerator RainOfCube()
    {
        WaitForSeconds delay = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            SpawnCube();

            yield return delay;
        }
    }

    private void SpawnCube()
    {
        float randomX = Random.Range(-_platformSize.x / 2, _platformSize.x / 2);
        float randomZ = Random.Range(-_platformSize.z / 2, _platformSize.z / 2);

        Vector3 spawnPosition = _mainPlatform.transform.position + new Vector3(randomX, _spawnHeight, randomZ);

        Cube cube = _pool.GetObject();
        cube.transform.position = spawnPosition;
        cube.Destroyed += OnDestroyed;
    }
}
