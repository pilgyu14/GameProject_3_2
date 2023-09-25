using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<GameObject> _pool = new Stack<GameObject>();
    private GameObject _prefab;
    private Transform _parent;

    public Pool(GameObject prefab, Transform parent, int count = 10)
    {
        _prefab = prefab;
        _parent = parent;

        for(int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }

    public T Pop<T>() where T : class, IPoolable 
    {
        GameObject obj = null;
        if(_pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj as T;
    }

    public void Push(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }

}