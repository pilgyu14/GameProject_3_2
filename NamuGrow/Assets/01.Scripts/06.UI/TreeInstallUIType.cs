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

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Bounds bounds;
    
    private void Awake()
    {
        treeManager = TreesManager.Instance;
        treeInstallManager = TreeInstallManager.Instance;
        installGhost = InstallGhost.Instance;
    }

    public void TypeSendTreeManager(TreeDataSO treeDataSo)
    {
        if (treeInstallManager != null)
        {
            treeInstallManager.InstallPrefab = null;
        }
        
        insGameObeject = treeManager.FindTreeData(treeDataSo.treeType).treePrefab.gameObject;
        
        meshFilter = insGameObeject.GetComponent<MeshFilter>();
        mesh = meshFilter.sharedMesh;
        bounds = mesh.bounds;
        height = bounds.size.y / 2;

        treeInstallManager.InstallPrefab = insGameObeject;
        
        installGhost.GhostObjectSet(insGameObeject,height);
        
        Debug.Log(treeInstallManager.InstallPrefab);
    }
}
