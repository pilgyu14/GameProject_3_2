using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
using System.Linq;

/// <summary>
/// 나무마다 다르게    
/// </summary>
[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class TreeDataSO : ScriptableObject
{
    [Header("최대 레벨 설정")]
    public int maxBranchLv; 
    public int maxLeafLv; 
    public int maxWoodenPostLv; 
    public int maxRootLv; 
}

[Serializable]
public class TreeData
{
    public float water; // 물 
    public float sunEnergy; // 빛 에너지
    public float sap; // 수액
    public float nutrient; // 양분 

}

public enum TreeLevel
{
    tree_level_first, // 묘목 
    tree_level_second, // 애기 나무
    tree_level_third, // 청소년 나무 
    tree_level_fourth, // 성인 나무 
    tree_level_fifth,  // 고목 
}

public enum EnergyType
{
    None, // 없음 
    Water, // 물  
    SunEnergy, // 빛 에너지 
    Sap, // 수액 
    Nutrient, // 양분
}

public abstract class AbTreeBase : MonoBehaviour, ITree, IUpdateObj
{
    
    private WoodenPostBase post; // 기둥

    private List<RootBase> rootList = new List<RootBase>(); // 뿌리 
    private List<BranchBase> branchBaseList = new List<BranchBase>(); // 가지 
    private List<LeafBase> leafBaseList = new List<LeafBase>(); // 잎 

    /// <summary>
    /// 에너지를 얻어내는 것들 리스트 
    /// </summary>
    private List<IProduceEnergy> produceEnergyElementList = new List<IProduceEnergy>();

    [SerializeField]
    private TreeDataSO treeDataSO; 
    
    // 현재 가지고 있는 에너지 데이터 
    [SerializeField]
    private TreeLevel treeLevel; 
    [SerializeField]
    private TreeData treeEnergyData;

    // 프로퍼티 
    public TreeData TreeEnergyData => treeEnergyData;
    public TreeDataSO TreeSettingData => treeDataSO; 
    
    // 테스트용 
    public float testUpgradeAmount;

    [ContextMenu("강화 테스트")]
    public void UpradeTest()
    {
        
    }
    protected virtual void Awake()
    {
        CashingElements();
        InitTreeElements();
    }

    protected virtual void Start()
    {
        UpdateManager.Instance.AddUpdateObj(this);
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.RemoveUpdateObj(this);
    }

    [SerializeField]
    private float cycleTime = 1f; 
    [SerializeField]
    private float curCycleTime = 1f; 

    public virtual void OnUpdate()
    {
        // 시간마다 에너지 생산 
        curCycleTime += Time.deltaTime;
        if (curCycleTime >= cycleTime)
        {
            curCycleTime = 0;
            ProduceEnergy();
            Debug.Log("에너지 생산");
        }    
    }

    public virtual void OnLateUpdate() { }

    public virtual void OnFixedUpdate() { }


    /// <summary>
    /// 외부에서 에너지를 얻을 경우 
    /// </summary>
    /// <param name="_energyType"></param>
    /// <param name="_amount"></param>
    public void AddEnergy(EnergyType _energyType, float _amount)
    {
        
    }
    /// <summary>
    /// 나무 요소들 캐싱 
    /// </summary>
    private void CashingElements()
    {
        post = GetComponentInChildren<WoodenPostBase>();
        rootList = GetComponentsInChildren<RootBase>().ToList(); 
        leafBaseList = GetComponentsInChildren<LeafBase>().ToList(); 
    }

    /// <summary>
    /// 관찰하며 에너지 얻을 거 체크 
    /// </summary>
    private void InitTreeElements()
    {
        produceEnergyElementList.Clear();
        produceEnergyElementList.AddRange(leafBaseList);
        produceEnergyElementList.AddRange(rootList);
    }

    /// <summary>
    ///  에너지 생산  
    /// </summary>
    protected void ProduceEnergy()
    {
        foreach (var treeElement in produceEnergyElementList)
        {
            float outputEnergy = treeElement.ProduceEnergy(); //  생산된 에너지 

            switch (treeElement.ProduceEnergyType)
            {
                case EnergyType.Water:
                    treeEnergyData.water = outputEnergy;
                    break;
                case EnergyType.SunEnergy:
                    treeEnergyData.sunEnergy = outputEnergy;
                    break;
                case EnergyType.Sap:
                    treeEnergyData.sap = outputEnergy;
                    break;
                case EnergyType.Nutrient:
                    treeEnergyData.nutrient = outputEnergy;
                    break;
            }
        }
    }


}
