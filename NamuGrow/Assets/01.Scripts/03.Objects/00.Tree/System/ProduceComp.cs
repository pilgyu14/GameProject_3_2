using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbTreeSystemComp : MonoBehaviour
{
    protected AbTreeBase owner; 
    protected TreeDataSO treeDataSO; 
    
    public void InitSO(AbTreeBase _owner, TreeDataSO _treeSO)
    {
        this.owner = _owner;
        this.treeDataSO = _treeSO; 
    }

    public abstract void UpdateUpgrade();
}
public class ProduceComp : AbTreeSystemComp
{
    [SerializeField] private float cycleTime = 1f;
    [SerializeField] private float curCycleTime = 1f;
    
    [SerializeField]
    private  List<ProductionEnergy> productionEnergyList =new List<ProductionEnergy>();

    public List<ProductionEnergy> ProductionEnergyList => productionEnergyList;

    
    /// <summary>
    /// 에너지를 얻어내는 것들 리스트 
    /// </summary>
    private List<IProduceEnergy> produceEnergyElementList = new List<IProduceEnergy>();
    // 프로퍼티 
    public List<IProduceEnergy> ProduceEnergyElementList => produceEnergyElementList;

    private void Start()
    {
        InitProduceEnergy(); 
    }

    /// <summary>
    /// 생산할 에너지 SO에 맞게 초기화 
    /// </summary>
    private void InitProduceEnergy()
    {
        productionEnergyList.Clear();
        foreach (var _energyType in treeDataSO.CurLevelSoData.produceAmountDic.KeyList)
        {
            productionEnergyList.Add(new ProductionEnergy(_energyType));
        }

        foreach (var _produceEnergy in productionEnergyList)
        {
            _produceEnergy.ProduceAmount = treeDataSO.CurLevelSoData.produceAmountDic[_produceEnergy.ProduceEnergyType];
        }
    }
    public void Add(EnergyType _energyType)
    {
                    
    }
    /// <summary>
    /// 외부에서 에너지를 얻을 경우 
    /// </summary>
    /// <param name="_energyType"></param>
    /// <param name="_amount"></param>
    public void AddEnergy(EnergyType _energyType, float _amount)
    {
    }

    public void Update()
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
    /// <summary>
    ///  에너지 생산  
    /// </summary>
    protected virtual void ProduceEnergy()
    {
        foreach (var treeElement in produceEnergyElementList)
        {
            float outputEnergy = treeElement.ProduceEnergy(); //  생산된 에너지 

            EnergyManager.Instance.AddEnergy(treeElement.ProduceEnergyType, (int)outputEnergy);

            #region 이전

            switch (treeElement.ProduceEnergyType)
            {

                /*case EnergyType.Water:
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
                    break;*/

            }

            #endregion
           
        }
    }

    public override void UpdateUpgrade()
    {
        InitProduceEnergy();
    }
}

