
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallComp : AbTreeSystemComp,IDamagable, IUpdateObj
{
    [SerializeField] private StateType stateType = StateType.Idle; 
    [SerializeField] private float hp;
    [SerializeField] private float hpHealAmount = 1; 
    [SerializeField]
    private float time = 5f;

    private float curTime = 0; 
    
    public void Die()
    {
    }

    [field:SerializeField] public bool IsDied { get; set; } = false; 

    [field: SerializeField] public MoveType MoveType => MoveType.ground; 
    [field: SerializeField] public float MaxHealth { get; set; } = 100f; 
    
    //public float MaxHealth { get=>maxHp; set=>maxHp = value; }
    public float CurHealth { get => hp; set => hp =value; }

    public float Hp
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(hp, 0, MaxHealth);
        }
    }

    private void Start()
    {
        hp = MaxHealth; 
    }

    public override void UpdateUpgrade()
    {
        MaxHealth =treeDataSO.CurLevelSoData.hp;
    }

    public void Damaged(float _damageAmount)
    {
        stateType = StateType.Damaged;
        hp -= _damageAmount;
        if (hp <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        
    }

    public void OnUpdate()
    {
        if (stateType == StateType.Damaged)
        {
            CheckTime(); 
        }
        else if (stateType == StateType.Idle)
        {
            Heal(); 
        }
    }

    private void CheckTime()
    {
        curTime += Time.deltaTime;
        if (curTime >= time)
        {
            // 상태 변경 
            stateType = StateType.Idle;
            curTime = 0; 
        }
    }

    private void Heal()
    {
        
    }

    #region 안 쓰는 Update

    public void OnLateUpdate()
    {
    }

    public void OnFixedUpdate()
    {
    }

    #endregion

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
