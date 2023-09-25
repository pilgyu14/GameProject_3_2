using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbBaseModule : MonoBehaviour
{
    protected AbMainModule mainModule;

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        
    }
    public virtual void InitMainModule(AbMainModule _mainModule)
    {
        this.mainModule = _mainModule; 
    }


}
