using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase
{
    protected readonly Queue<MonoBehaviour> _pool = new();
    protected readonly MonoBehaviour _prefab;
    protected readonly Transform _parent;
    protected MonoBehaviour _lastObject;

    public ObjectPoolBase(MonoBehaviour prefab, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public abstract MonoBehaviour GetObject();
    public abstract void ReturnObject(MonoBehaviour obj);
    protected abstract MonoBehaviour InstantiatePrefab();

    public MonoBehaviour GetLastObject() => _lastObject;
}

public class ObjectPool<T> : ObjectPoolBase where T : MonoBehaviour
{
    public ObjectPool(T prefab, Transform parent = null) : base(prefab, parent)
    {
    }

    protected override MonoBehaviour InstantiatePrefab()
    {
        var instance = UnityEngine.Object.Instantiate(_prefab, _parent);
        instance.gameObject.SetActive(false);
        return instance;
    }

    public override MonoBehaviour GetObject()
    {
        MonoBehaviour obj;
        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
        }
        else
        {
            obj = InstantiatePrefab();
        }
        obj.gameObject.SetActive(true);
        _lastObject = obj;
        return obj;
    }

    public override void ReturnObject(MonoBehaviour obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public T GetTyped() => (T)GetObject();
    public void ReturnTyped(T obj) => ReturnObject(obj);
}

public class ObjectPoolManager
{
    private readonly Dictionary<Type, ObjectPoolBase> _pools = new();
    private Transform _parent;

    public ObjectPoolManager()
    {
        
    }

    public void Register<T>(T prefab, Transform parent, int initialCount = 5) where T : MonoBehaviour
    {
        _parent = parent;
        var pool = new ObjectPool<T>(prefab, _parent);
        _pools[typeof(T)] = pool;

        // Pre-instantiate
        for (int i = 0; i < initialCount; i++)
        {
            pool.GetObject();
            pool.ReturnTyped(pool.GetTyped());
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (!_pools.TryGetValue(typeof(T), out var pool))
        {
            Debug.LogWarning($"[ObjectPoolManager] Pool not registered for {typeof(T).Name}");
            return null;
        }
        return ((ObjectPool<T>)pool).GetTyped();
    }

    public void Return<T>(T obj) where T : MonoBehaviour
    {
        if (!_pools.TryGetValue(typeof(T), out var pool))
        {
            Debug.LogWarning($"[ObjectPoolManager] No pool for {typeof(T).Name}");
            return;
        }
        ((ObjectPool<T>)pool).ReturnTyped(obj);
    }
}
