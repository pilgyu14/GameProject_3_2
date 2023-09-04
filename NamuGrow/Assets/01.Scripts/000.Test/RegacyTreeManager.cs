using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegacyTreeManager : MonoBehaviour
{
    public Terrain terrain; // 트리가 있는 Terrain 오브젝트
    public GameObject newTreePrefab; // 추가할 새 트리 프리팹

    [ContextMenu("나무 생성 ")]
    public void MakeTree()
    {
        // 새로운 트리 프로토타입 생성
        TreePrototype newTreePrototype = new TreePrototype();
        newTreePrototype.prefab = newTreePrefab;

        // 트리 프로토타입 추가
        terrain.terrainData.treePrototypes = AddTreePrototype(terrain.terrainData.treePrototypes, newTreePrototype);

        // 트리 인스턴스 추가 (예를 들어 특정 위치에)
        AddTreeInstance(terrain, terrain.SampleHeight(new Vector3(10f, 0f, 10f)) + terrain.transform.position.y,
            newTreePrototype, 0f, Vector3.one);
    }

    private void Start()
    {
    }

    // 새로운 트리 프로토타입 추가하는 함수
    private TreePrototype[] AddTreePrototype(TreePrototype[] prototypes, TreePrototype newPrototype)
    {
        TreePrototype[] newPrototypes = new TreePrototype[prototypes.Length + 1];
        prototypes.CopyTo(newPrototypes, 0);
        newPrototypes[prototypes.Length] = newPrototype;
        return newPrototypes;
    }

    // 새로운 트리 인스턴스 추가하는 함수
    private void AddTreeInstance(Terrain terrain, float height, TreePrototype prototype, float rotation, Vector3 scale)
    {
        TreeInstance newTree = new TreeInstance();
        newTree.prototypeIndex = terrain.terrainData.treePrototypes.Length - 1; // 마지막에 추가한 프로토타입
        newTree.position = new Vector3(10f, height, 10f);
        newTree.rotation = rotation;
        newTree.heightScale = scale.y;
        newTree.widthScale = scale.x;
        newTree.color = Color.white;
        newTree.lightmapColor = Color.white;

        terrain.AddTreeInstance(newTree);
    }
}