using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProductionEnergy : IProduceEnergy
{
    private int produceAmount;
    [SerializeField]
    private EnergyType energyType; 
    public int ProduceAmount
    {
        get => produceAmount;
        set => produceAmount = value;
    }
    public EnergyType ProduceEnergyType => energyType;

    public ProductionEnergy(EnergyType _type)
    {
        energyType = _type; 
    }
    public float ProduceEnergy()
    {
        // 데이터 받고 리턴 
        return ProduceAmount; 
    }
}
public class SapProduction : IProduceEnergy
{
    private int produceAmount; 
    public int ProduceAmount
    {
        get => produceAmount;
        set => produceAmount = value;
    }
    public EnergyType ProduceEnergyType => EnergyType.Sap;
    public float ProduceEnergy()
    {
        // 데이터 받고 리턴 
        return ProduceAmount; 
    }
}