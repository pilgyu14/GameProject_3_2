using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstallUIType : MonoBehaviour
{
    private GameObject treePrefab;

    private void Awake()
    {
    }

    public void TypeSendTreeManager(TreeDataSO treeDataSo)
    {
        TreesManager.Instance.FindTreeData(treeDataSo.treeType);
        Debug.Log(TreesManager.Instance.FindTreeData(treeDataSo.treeType));
    }
}
