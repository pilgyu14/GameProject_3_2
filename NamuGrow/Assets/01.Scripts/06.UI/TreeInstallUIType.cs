using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstallUIType : MonoBehaviour
{
    private GameObject treePrefab;

    private TreesManager treeManager;
    private TreeInstallManager treeInstallManager;
    private InstallGhost installGhost;
    
    private GameObject insGameObeject;
    private float height;

    //물체 높이 구하기 위해 필요한 변수들
    /*private MeshFilter meshFilter;
    private Mesh mesh;
    private Bounds bounds;*/
    
    private void Awake()
    {
        treeManager = TreesManager.Instance;
        treeInstallManager = TreeInstallManager.Instance;
        installGhost = InstallGhost.Instance;
    }

    public void TypeSendTreeManager(TreeDataSO treeDataSo)
    {
        if (treeInstallManager.InstallObjectGet() != null)
        {
            treeInstallManager.InstallObjectSet(null);
        }
        // 

        foreach (var _needEnergy in treeDataSo.CurLevelSoData.needEnergyDic.Dictionary)
        {
            if (EnergyManager.Instance.IsEnough(_needEnergy.Key, _needEnergy.Value) == false)
            {
                Debug.Log("자원이 부족합니다 ");
                return; 
            }
        }
        insGameObeject = treeManager.FindTreeData(treeDataSo.treeType).treePrefab.gameObject;

        
        // 물체 높이 구하는 법. (메쉬 필터가 있는 오브젝트)
        /*meshFilter = insGameObeject.GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;
        bounds = mesh.bounds;
        height = bounds.size.y / 2;*/

        treeInstallManager.InstallObjectSet(insGameObeject);
        
        installGhost.GhostObjectSet(insGameObeject,0);
    }
}
