using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateManager : MonoSingleton<EnemyCreateManager>
{
    public List<TestEnemy> enemyList = new List<TestEnemy>();

    public GameObject CreateEnemy(string _monsterName, Vector3 _pos)
    {
        var _enemy  = PoolManager.Instance.Pop<TestEnemy>(_monsterName);
        _enemy.transform.position = _pos;
        return _enemy.gameObject; 
    }
}
