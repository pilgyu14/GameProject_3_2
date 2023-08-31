using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// 나무마다 다르게    
/// </summary>
//[CreateAssetMenu(menuName = "SO/Tree/TreeDataSO")]
public class RegacyTreeDataSO : ScriptableObject
{
    [Header("최대 레벨 설정")] public int maxBranchLv;
    public int maxLeafLv;
    public int maxWoodenPostLv;
    public int maxRootLv;
}

[Serializable]
public class TreeData1
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
    [SerializeField] protected TreeLevel treeLevel;
    [SerializeField] protected TreeDataSO treeDataSO;

    protected GameObject model;  
    protected UnityEvent onUpgradeEvt = null;
    // 프로퍼티
    public TreeType TreeType => treeDataSO.treeType; 


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
        
        model = transform.Find("Model").gameObject;
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
        var _treeData = treeDataSO.CurLevelSoData;
        
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
        // 사이즈 업그레이드 
        model.transform.DOScale(treeDataSO.CurLevelSoData.scale,1f);
        // 할 일 수행 
        onUpgradeEvt?.Invoke();

        return true; 
    }
}