using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateObj
{
    public void OnUpdate(); 
    public void OnLateUpdate(); 
    public void OnFixedUpdate(); 
}

public class UpdateManager : MonoSingleton<UpdateManager>
{
    public HashSet<IUpdateObj> updateObjList = new HashSet<IUpdateObj>();

    public void AddUpdateObj(IUpdateObj _iUpdateObj)
    {
        if(!updateObjList.Add(_iUpdateObj))
        {
            Debug.LogError("중복");
        }          
    }
    public void RemoveUpdateObj(IUpdateObj _iUpdateObj)
    {
        if (!updateObjList.Contains(_iUpdateObj))
        {
            Debug.LogError("없음");
        }
        updateObjList.Remove(_iUpdateObj);
    }

    public void Clear()
    {
        updateObjList.Clear();
    }
    public void Update()
    {
        foreach (var updateObj in updateObjList)
        {
            updateObj.OnUpdate();
        }
    }
    public void FixedUpdate()
    {
        foreach (var updateObj in updateObjList)
        {
            updateObj.OnFixedUpdate();
        }
    }
    public void LateUpdate()
    {
        foreach (var updateObj in updateObjList)
        {
            updateObj.OnLateUpdate();
        }
    }
    

}