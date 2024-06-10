using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;
    private Queue<T> _pool;

    public IEnumerable<T> PooledObject => _pool;

    public ObjectPool(T prefab, Transform container, int prewarmObjectsCount = 0)
    {
        _prefab = prefab;
        _container = container;
        _pool = new Queue<T>();

        for (int i = 0; i < prewarmObjectsCount; i++)
        {
            CreateObject();
        }
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            CreateObject();
        }

        T poolObject = _pool.Dequeue();
        poolObject.gameObject.SetActive(true);

        return poolObject;
    }

    public void PutObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        poolObject.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _pool.Clear();
    }

    private void CreateObject()
    {
        T poolObject = Object.Instantiate(_prefab, _container);
        _pool.Enqueue(poolObject);
        poolObject.gameObject.SetActive(false);
    }
}
