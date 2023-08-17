using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spawns LSystems for demonstration and performance testing/
/// </summary>
public class Spawner : MonoBehaviour
{

    [SerializeField]
    [Tooltip("사이드 스폰 크기로 지정된 영역에 스폰할 개체")]
    //[Tooltip("Objects to spawn in the area specified by side spawn size.")]
    protected List<GameObject> sideObjects;

    [SerializeField]
    [Tooltip("wideSpawnSize에서 스폰할 객체")]
    //[Tooltip("Objects to spawn in wideSpawnSize")]
    protected List<GameObject> wideObjects;

    [SerializeField]
    [Tooltip("지상 드레싱 개체를 인스턴스화하는 사이의 시간입니다.")]
    //[Tooltip("Time between Instantiating ground dressing objects.")]
    protected float timePerGroundDressingSpawn;

    [SerializeField]
    [Tooltip("측면 개체를 인스턴스화하는 사이의 시간입니다.")]
    //[Tooltip("Time between Instantiating side objects.")]
    protected float timePerSideSpawn;

    [SerializeField]
    [Tooltip("측면 스폰 영역의 크기입니다. 측면에 유의하십시오.")]
    //[Tooltip("Size of the side spawn area. Note its per side.")]
    float sideSpawnSize = 4;

    [SerializeField]
    [Tooltip("중앙 영역의 크기입니다. 사이드 스폰은 여기에서 인스턴스화되지 않습니다.")]
    //[Tooltip("Size of the center area. side spawn is not instantiated here.")]
    float centerSpawnSize = 2;

    [SerializeField]
    [Tooltip("광역의 크기")]
    ///[Tooltip("Size of the wide area.")]
    float wideSpawnSize = 10;

    [SerializeField]
    [Tooltip("전방 축을 따라 스폰에서 거리.")]
    // [Tooltip("Distance from spawn along the forward axis.")]
    float spawnDistance = 5f;

    protected float currentSideTime = 0f;
    protected float currentWideTime = 0f;
    protected bool side;

    protected void Update ()
    {
        // Try create side objects
        currentSideTime += Time.deltaTime;
        if (currentSideTime > timePerSideSpawn)
        {
            float latPos;
            if (side) latPos = Random.Range(-sideSpawnSize - centerSpawnSize, -centerSpawnSize);
            else latPos = Random.Range(centerSpawnSize, centerSpawnSize + sideSpawnSize);
        
            Vector3 positon = transform.position + (Vector3.forward * spawnDistance) + latPos * transform.right;
            GameObject g = (GameObject)Instantiate(sideObjects[Random.Range(0, sideObjects.Count)],  positon, Quaternion.identity);

            // Trigger is used to destroy the mesh when it goes off screen.
            if (g.GetComponent<Collider>() == null)
            {
                Collider coll = g.AddComponent<BoxCollider>();
                coll.isTrigger = true;
            }
            currentSideTime = 0f;
            side = !side; 
        }

        // Try create wide objects
        currentWideTime += Time.deltaTime;
        if (currentWideTime > timePerGroundDressingSpawn)
        {
            float latPos;
            latPos = Random.Range(-wideSpawnSize, wideSpawnSize);

            Vector3 positon = transform.position + (Vector3.forward * spawnDistance) + latPos * transform.right;

            GameObject g = (GameObject)Instantiate(wideObjects[Random.Range(0, wideObjects.Count)], positon, Quaternion.identity);
            if (g.GetComponent<Collider>() == null)
            {
                Collider coll = g.AddComponent<BoxCollider>();
                coll.isTrigger = true;
            }
            currentWideTime = 0f;
        }
    }

    // Destroy Module when it hits this GameObjects collider.
    protected void OnTriggerEnter(Collider coll)
    {
        if(coll.GetComponent<LSystem.Module>() != null)
        {
            Destroy(coll.gameObject);
        }
    }
}
