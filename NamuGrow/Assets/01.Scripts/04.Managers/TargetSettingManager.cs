using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSettingManager : MonoSingleton<TargetSettingManager>
{
    public List<LayerMask> targetLayerMask = new List<LayerMask>();

    public List<LayerMask> GetTargetLayerMasks(LayerMask _ignoreLayer)
    {
        return targetLayerMask.FindAll((x) => x != _ignoreLayer);
    }
}
