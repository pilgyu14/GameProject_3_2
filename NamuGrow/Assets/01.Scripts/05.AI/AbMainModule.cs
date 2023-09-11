using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

public enum ModuleType
{
    Move,
    AIMove,
}
public class AbMainModule : MonoBehaviour
{
    [SerializeField] protected string unitDataSoAddress = ""; 
    [field:SerializeField] public UnitDataSO UnitDatUaSO { get; set;  }  
    public Dictionary<ModuleType, AbBaseModule> ModuleDic { get; set; } = new Dictionary<ModuleType, AbBaseModule>();

    protected virtual void Awake()
    {
        UnitDatUaSO ??= AddressablesManager.Instance.GetResource<UnitDataSO>(unitDataSoAddress);
    }
    protected virtual void Start()
    {
    }

    public T GetModule<T>(ModuleType _moduleType) where T : AbBaseModule
    {
        if (ModuleDic.TryGetValue(_moduleType, out AbBaseModule _module) == true)
        {
            return _module as T; 
        }
        Debug.Log(_moduleType + "모듈이 없습니다");
        return null; 
    }

    public void AddModule(ModuleType _moduleType, AbBaseModule _newModule)
    {
        if (ModuleDic.ContainsKey(_moduleType) == false)
        {
            ModuleDic.Add(_moduleType, _newModule);
        }
        else
        {
            Debug.Log("동일한 모듈이 추가 되어 있습니다." + System.Enum.GetName(typeof(ModuleType),_moduleType));
        }
    }
    
}
