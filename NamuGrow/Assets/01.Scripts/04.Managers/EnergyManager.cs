using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class HaveEnergyData
{
    public int sap;
    public int sweetSap;
    public int acorn;
    public int kiwi;

}
public class EnergyManager : MonoSingleton<EnergyManager>
{

    [SerializeField]
    private HaveEnergyData haveEnergyData;
    

    public int Sap
    {
        get => haveEnergyData.sap;
        set => haveEnergyData.sap = value; 
    }
    public int SweetSap
    {
        get => haveEnergyData.sweetSap;
        set => haveEnergyData.sweetSap = value; 
    }
    
    public int Acorn
    {
        get => haveEnergyData.acorn;
        set => haveEnergyData.acorn = value; 
    }
    
    public int Kiwi
    {
        get => haveEnergyData.kiwi;
        set => haveEnergyData.kiwi = value; 
    }

    public bool RemoveEnergy(EnergyType _energyType, int _amount)
    {
        // 강화 가능한만큼 자원이 있는지 체크 
        bool _isAble = IsEnough(_energyType, _amount);
        if (_isAble == false) return false;
        
        switch (_energyType)
        {
            case EnergyType.None:
                break;
            case EnergyType.Sap:
                Sap-= _amount; 
                break;
            case EnergyType.SweetSap:
                SweetSap -= _amount; 
                break;
            case EnergyType.Acorn:
                Acorn -= _amount; 
                break;
            case EnergyType.Kiwi:
                Kiwi -= _amount; 
                break;
        }

        return true; 
    }
    
    public bool AddEnergy(EnergyType _energyType, int _amount)
    {
        // 강화 가능한만큼 자원이 있는지 체크 
        bool _isAble = IsEnough(_energyType, _amount);
        if (_isAble == false) return false;

        switch (_energyType)
        {
            case EnergyType.None:
                break;
            case EnergyType.Sap:
                Sap+= _amount; 
                break;
            case EnergyType.SweetSap:
                SweetSap += _amount; 
                break;
            case EnergyType.Acorn:
                Acorn += _amount; 
                break;
            case EnergyType.Kiwi:
                Kiwi += _amount; 
                break;
        }
        return true;
    }
    
    /// <summary>
    /// 에너지가 충분한가
    /// </summary>
    /// <param name="_energyTYpe"></param>
    /// <param name="_amount"></param>
    /// <returns></returns>
    public bool IsEnough(EnergyType _energyTYpe, int _amount)
    {
        int _needEnergy = GetHaveEnergy(_energyTYpe);
        return _amount >= _needEnergy;
    }
    /// <summary>
    ///  현재 보유 에너지 반환 
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    public int GetHaveEnergy(EnergyType _type)
    {
        int _energy = 0; 
        switch (_type)
        {
            case EnergyType.None:
                break;
            case EnergyType.Sap:
                _energy = Sap; 
                break;
            case EnergyType.SweetSap:
                _energy = SweetSap;
                break;
            case EnergyType.Acorn:
                _energy = Acorn; 
                break;
            case EnergyType.Kiwi:
                _energy = Kiwi; 
                break;
        }

        return _energy; 
    }


}
