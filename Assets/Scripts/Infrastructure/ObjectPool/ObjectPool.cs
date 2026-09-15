using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> _pool = new();
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly string _debugName;

    public ObjectPool(T prefab, Transform parent = null, string debugName = null)
    {
        _prefab = prefab;
        _parent = parent;
        _debugName = debugName;
    }

    public T GetObject()
    {
        if (_pool.Count > 0)
        {
            var obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        var objInstance = Object.Instantiate(_prefab, _parent);
        objInstance.gameObject.SetActive(true);
        return objInstance;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
