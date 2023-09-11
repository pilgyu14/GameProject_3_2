using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbBaseModule : MonoBehaviour
{
    private AbMainModule mainModule;

    protected virtual void Awake()
    {
        
    }
    
    protected virtual void Start(AbMainModule _mainModule)
    {
        this.mainModule = _mainModule; 
    }


}
