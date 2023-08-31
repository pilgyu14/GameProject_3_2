
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallComp : MonoBehaviour
{
    
}

public class AddTroops
{
    public int addTroops; 
    
}
[CreateAssetMenu(menuName = "SO/ProductionDataSO")]
public class ProductionDataSO : ScriptableObject
{
    public EnergyType produceType; // 생산 아이템 타입 
    public int amount; 

}


public class MainTree : AbTreeBase, IAddTroops
{
    private static MainTree instance = null;

    public static MainTree Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(MainTree)).GetComponent<MainTree>();
            }

            return instance;
        }
    }

    [SerializeField] private ProduceComp produceComp;
    [SerializeField] private TroopsComp troopsComp;

    private List<AbTreeSystemComp> treeSystemCompList = new List<AbTreeSystemComp>();

    // 프로퍼티
    public int Troops => troopsComp.TroopsAmount;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        produceComp ??= GetComponent<ProduceComp>();
        troopsComp ??= GetComponent<TroopsComp>();
    }

    protected override void Start()
    {
        base.Start();
        treeSystemCompList.Add(produceComp);
        treeSystemCompList.Add(troopsComp);

        InitCompData();

        onUpgradeEvt.AddListener(UpdateUpgradeData); 
    }

    /// <summary>
    /// 업그레이드시 생산 데이터 업데이트 
    /// </summary>
    private void UpdateUpgradeData()
    {
        foreach (var _treeSystem in treeSystemCompList)
        {
            _treeSystem.UpdateUpgrade();
        }
    }
    public void AddTroops(int _troops)
    {
    }

    private void InitCompData()
    {
        foreach (var treeComp in treeSystemCompList)
        {
            treeComp.InitSO(this,treeDataSO);
        }
        
    }

}
