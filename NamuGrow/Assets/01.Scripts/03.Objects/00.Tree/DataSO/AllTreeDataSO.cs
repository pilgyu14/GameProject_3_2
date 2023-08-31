using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "SO/Tree/AllTreeDataSO")]
public class AllTreeDataSO : ScriptableObject
{
    [FormerlySerializedAs("allTreeDataSO")] public List<TreeDataSO> allTreeDataSOList; 
}
