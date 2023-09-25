using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSettingManager : MonoSingleton<TargetSettingManager>
{
    public TeamTypeLayerMaskDic targetLayerMaskDic = new TeamTypeLayerMaskDic(); 
    public List<LayerMask> targetLayerMask = new List<LayerMask>();

    public LayerMask layerMask; 
   [ContextMenu("Wrr")]
    public void Test()
    {
        Debug.Log((int)layerMask);
    }
    public List<LayerMask> GetTargetLayerMasks(TeamType _ignoreType)
    {
        List<LayerMask> targetLayerMaskList = new List<LayerMask>(); 
        foreach (var _pair in targetLayerMaskDic.Dictionary)
        {
            if (_pair.Key != _ignoreType)
            {
                targetLayerMaskList.Add(_pair.Value);
            }
        }

        return targetLayerMaskList; 
    }

    public int GetLayerMask(TeamType _ignoreType)
    {
        var _layerList = GetTargetLayerMasks(_ignoreType);
        int layerMask = 0; 
        foreach (var _layer in _layerList)
        {
            layerMask += 1 << (int)_layer; 
        }

        return layerMask; 
    }
}
