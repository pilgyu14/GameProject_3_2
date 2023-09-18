using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

public enum PoolType
{
    Test, 

    HitEffect_Red =100, 
}


public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField]
    private PoolingListSO poolingListSO; 
    private Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

    [SerializeField]
    private Transform _trmParent;

    private void Awake()
    {
        poolingListSO ??= AddressablesManager.Instance.GetResource<PoolingListSO>("PoolingListSO");
        _trmParent ??= new GameObject("PoolingParent").transform;
    }

    private void Start()
    {
        foreach (var _pool in poolingListSO.list)
        {
            CreatePool(_pool.prefab, _pool.poolCnt);
        }
        
    }

    public void CreatePool(GameObject prefab, int count = 10)
    {
        Pool pool = new Pool(prefab, _trmParent, count);
        if (_pools == null)
            _pools = new Dictionary<string, Pool>(); 
        _pools.Add(prefab.gameObject.name, pool);

    }

    public T Pop<T>(string prefabName) where T : class, IPoolable 
    {
        if (!_pools.ContainsKey(prefabName))
        {
            Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        T item = _pools[prefabName].Pop<T>();
        item.Reset();
        return item;
    }

    public void Push(GameObject obj)
    {
        _pools[obj.name.Trim()].Push(obj);
    }
}