
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceComp : MonoBehaviour
{
    [SerializeField] private float cycleTime = 1f;
    [SerializeField] private float curCycleTime = 1f;
    
    /// <summary>
    /// 에너지를 얻어내는 것들 리스트 
    /// </summary>
    private List<IProduceEnergy> produceEnergyElementList = new List<IProduceEnergy>();
    // 프로퍼티 
    public List<IProduceEnergy> ProduceEnergyElementList => produceEnergyElementList; 
    
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
        }
    }

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

public interface ITreeBehaviour
{
    
}

[CreateAssetMenu(menuName = "SO/Test/T")]
public class BehaviourDataSO : ScriptableObject, ITreeBehaviour
{
}
public class MainTree : AbTreeBase, IAddTroops
{
    [SerializeField]
    private ProduceComp produceComp;

    public ITreeBehaviour a; 

    protected override void Awake()
    {
        base.Awake();
        produceComp ??= GetComponent<ProduceComp>(); 
    }
    
    public void AddTroops(int _troops)
    {
    }

}
