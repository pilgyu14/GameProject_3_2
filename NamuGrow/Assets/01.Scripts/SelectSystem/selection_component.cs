using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;

/// <summary>
/// 추가시 색 변경 
/// </summary>
public class selection_component : MonoBehaviour
{
    private Color DefaultColor;
    private Renderer renderer;
    private static GameObject selectionDecalPrefab; 
    
    private  const string decalAddress = "DecalSelection"; 
    
    void Start()
    {
        //renderer = GetComponent<Renderer>();
        //DefaultColor = renderer.material.color;
        //renderer.material.color = Color.red;

        if (selectionDecalPrefab == null)
        {
           // selectionDecalPrefab = AddressablesManager.Instance.GetResource<GameObject>(decalAddress);
        }

     //   Instantiate(selectionDecalPrefab, transform); 
    }

    private void OnDestroy()
    {
        //renderer.material.color = DefaultColor;
    }
}