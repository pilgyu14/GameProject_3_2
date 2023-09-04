using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModuleType
{
    Move,
}
public class AbMainModule : MonoBehaviour
{
    public Dictionary<ModuleType, AbBaseModule> ModuleDic { get; set; } = new Dictionary<ModuleType, AbBaseModule>();

    public T GetModule<T>(ModuleType _moduleType) where T : AbBaseModule
    {
        if (ModuleDic.TryGetValue(_moduleType, out AbBaseModule _module) == true)
        {
            return _module as T; 
        }
        Debug.Log(_moduleType + "모듈이 없습니다");
        return null; 
    }
    
}
