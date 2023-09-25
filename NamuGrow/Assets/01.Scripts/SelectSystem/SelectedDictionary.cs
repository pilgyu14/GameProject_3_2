using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDictionary : MonoBehaviour
{
    public IntSelectionUnitDic selectedDic = new IntSelectionUnitDic(); 

    /// <summary>
    /// InstanceID 를 키값으로 Dictionary에 저장 후
    /// 색 변경 
    /// </summary>
    /// <param name="go"></param>
    public void AddSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (selectedDic.ContainsKey(id) == false)
        {
            var _mark = go.GetComponent<SelectionUnit>(); 
            selectedDic.Add(id, _mark);
            _mark.ActiveSelectedMark(true);
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void Deselect(int id)
    {
        selectedDic[id].ActiveSelectedMark(false);
        selectedDic.Remove(id);
    }

    /// <summary>
    /// 선택 취소 
    /// </summary>
    public void DeselectAll()
    {
        foreach(KeyValuePair<int,SelectionUnit> pair in selectedDic.Dictionary)
        {
            if(pair.Value != null)
            {
                pair.Value.ActiveSelectedMark(false);
            }
        }
        selectedDic.Clear();
    }
}