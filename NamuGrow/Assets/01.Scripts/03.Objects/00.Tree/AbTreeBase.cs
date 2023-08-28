using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// 나무마다 다르게    
/// </summary>
[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class RegacyTreeDataSO : ScriptableObject
{
    [Header("최대 레벨 설정")] public int maxBranchLv;
    public int maxLeafLv;
    public int maxWoodenPostLv;
    public int maxRootLv;
}


[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class TreeData2SO : ScriptableObject
{
    public TreeType treeType;
    public EnergyType productEnergyType; // 자원 생산 타입 
    public TreeLevel maxLevel; // 최대 레벨 
    public TreeLevel curLevel; // 현재 레벨

    public List<TreeByLevel> treeByLevelList = new List<TreeByLevel>(); // 레벨에 따른 능력치 

    // 현재 나무 데이터 
    public TreeByLevel CurLevelData => treeByLevelList.Find((x) => x.level == curLevel);

    /// <summary>
    /// MaxLevel이 되면 false 반환 
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public bool LevelUp(int _value = 1)
    {
        ++curLevel;
        return curLevel < maxLevel;
    }
}

/// <summary>
/// 레벨에 따른 나무 데이터 
/// </summary>
[Serializable]
public class TreeByLevel
{
    public TreeLevel level; // 레벨 
    public GameObject changeTree; // 변할 나무 
    public int consumeEnergy; // 소비 에너지양

    public int addTroops; // 추가 병력수  
    public int productAmount; // 생산 에너지양 
    public int attackPower;
    public int hp; 
    
    // 강화에 필요한 에너지 
    public NeedEnergyDic needEnergyDic = new NeedEnergyDic();
}

[Serializable]
public class TreeData
{
    public float water; // 물 
    public float sunEnergy; // 빛 에너지
    public float sap; // 수액

    //public float nutrient; // 양분 
}


public abstract class AbTreeBase : MonoBehaviour, ITree, ITreeElement, IUpdateObj
{
    //private WoodenPostBase post; // 기둥

    // private List<RootBase> rootList = new List<RootBase>(); // 뿌리 
    // private List<BranchBase> branchBaseList = new List<BranchBase>(); // 가지 
    // private List<LeafBase> leafBaseList = new List<LeafBase>(); // 잎 



    [FormerlySerializedAs("treeDataSO")] [SerializeField]
    private RegacyTreeDataSO regacyTreeDataSo;

    // 현재 가지고 있는 에너지 데이터 
    [SerializeField] private TreeLevel treeLevel;
    [SerializeField] private TreeData2SO treeDataSO;

    protected UnityEvent onUpgradeEvt = null;
    // 프로퍼티 

    public UnityEvent OnUpgradeEvt
    {
        get => onUpgradeEvt;
        set => onUpgradeEvt = value; 
    }
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



    public virtual void OnUpdate()
    {

    }

    public virtual void OnLateUpdate()
    {
    }

    public virtual void OnFixedUpdate()
    {
    }




    /// <summary>
    /// 나무 요소들 캐싱 
    /// </summary>
    private void CashingElements()
    {
        //post = GetComponentInChildren<WoodenPostBase>();
        //rootList = GetComponentsInChildren<RootBase>().ToList();
        //leafBaseList = GetComponentsInChildren<LeafBase>().ToList();
    }

    /// <summary>
    /// 관찰하며 에너지 얻을 거 체크 
    /// </summary>
    private void InitTreeElements()
    {
        //produceEnergyElementList.Clear();
        /*produceEnergyElementList.AddRange(leafBaseList);
        produceEnergyElementList.AddRange(rootList);*/
    }


    public bool Upgrade()
    {
        var _treeData = treeDataSO.CurLevelData;
        
        // 하나의 자원이라도 부족하면 false 
        if (_treeData.needEnergyDic.Dictionary.Any(
                _needEnergy => EnergyManager.Instance.IsEnough(_needEnergy.Key, _needEnergy.Value) == true))
        {
            return false;
        }
        
        
        foreach (var _needEnergy in _treeData.needEnergyDic.Dictionary)
        {
            // 자원 줄이고 
            EnergyManager.Instance.RemoveEnergy(_needEnergy.Key, _needEnergy.Value);
        }

        // 업그레이드 
        treeDataSO.LevelUp();
        // 할 일 수행 
        onUpgradeEvt?.Invoke();

        return true; 
    }
}