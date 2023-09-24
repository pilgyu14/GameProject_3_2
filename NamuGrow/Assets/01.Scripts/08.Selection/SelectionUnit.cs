using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionUnit : MonoBehaviour
{
    private GameObject selectedMark;

    private void Awake()
    {
        Transform _findSelectedMark = transform.Find("SelectedMark");
        if (_findSelectedMark != null)
        {
            selectedMark = _findSelectedMark.gameObject; 
        }
        else
        {
            Debug.LogError("@자식 오브젝트에 SelectedMark 가 없습니다");
        }
    }

    private void Start()
    {
        ActiveSelectedMark(false); 
    }

    /// <summary>
    /// 유닛 선택 마크 활성화 설정 
    /// </summary>
    /// <param name="_isActive"></param>
    public void ActiveSelectedMark(bool _isActive)
    {
        if (selectedMark != null)
        {
            selectedMark.SetActive(_isActive);  
        }
    }
}
