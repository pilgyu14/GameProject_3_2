using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedDictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedDic = new Dictionary<int, GameObject>();

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
            selectedDic.Add(id, go);
            go.AddComponent<selection_component>();
            Debug.Log("Added " + id + " to selected dict");
        }
    }

    public void Deselect(int id)
    {
        Destroy(selectedDic[id].GetComponent<selection_component>());
        selectedDic.Remove(id);
    }

    /// <summary>
    /// 선택 취소 
    /// </summary>
    public void DeselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedDic)
        {
            if(pair.Value != null)
            {
                Destroy(selectedDic[pair.Key].GetComponent<selection_component>());
            }
        }
        selectedDic.Clear();
    }
}