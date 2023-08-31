using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class TreesManager : MonoSingleton<TreesManager>
{
    [SerializeField]
    private AllTreeDataSO allTreeDataSO; 
    // 설치된 모든 나무 관리 
    // 현재 보유 묘목 가지고 있고 
    // 설치할 때 여기서 설치 

    public TreeDataSO FindTreeData(TreeType _treeType)
    {
        var _treeDataSo = allTreeDataSO.allTreeDataSOList.Find((x) => x.treeType == _treeType);
        return _treeDataSo;
    }
    public void PlantTree()
    {
        
    }

    public void RemoveTree()
    {
        
    }
    // 나무 설치 
    // 나무 삭제 
}
