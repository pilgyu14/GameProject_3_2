using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class TreeData
{
    // 뿌리 리스트 
    // 가지 리스트
    // 기둥 클래스 
    // 잎 리스트 

    public TreeLevel treeLevel; 
    
    /*
     * 나뭇가지 레벨, 
    나무기둥 레벨,        
    나무뿌리 레벨, 
    나뭇잎 레벨, 
     */
    
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
    private RootBase root; // 뿌리 
    private WoodenPostBase post; // 기둥
    private BranchBase branchBase; // 가지 
    private LeafBase leafBase; // 잎 

    private List<BranchBase> branchBaseList = new List<BranchBase>(); // 가지 
    private List<LeafBase> leafBaseList = new List<LeafBase>(); // 잎 

    private List<ITreeElement> treeElementList = new List<ITreeElement>(); 

    // 현재 가지고 있는 에너지 데이터 
    private TreeData treeEnergyData;
    private Dictionary<EnergyType, float> treeEnergyDic = new Dictionary<EnergyType, float>(); 

    protected virtual void Awake()
    {
        CashingElements();
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
        }    }

    public virtual void OnLateUpdate() { }

    public virtual void OnFixedUpdate() { }
    

    /// <summary>
    /// 나무 요소들 캐싱 
    /// </summary>
    private void CashingElements()
    {
        root = GetComponentInChildren<RootBase>(); 
        post = GetComponentInChildren<WoodenPostBase>(); 
        branchBase = GetComponentInChildren<BranchBase>(); 
        leafBase = GetComponentInChildren<LeafBase>(); 
    }

    /// <summary>
    ///  에너지 생산  
    /// </summary>
    protected void ProduceEnergy()
    {
        foreach (var treeElement in treeElementList)
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
