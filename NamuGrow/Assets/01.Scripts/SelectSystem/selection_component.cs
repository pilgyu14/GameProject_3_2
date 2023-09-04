using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 추가시 색 변경 
/// </summary>
public class selection_component : MonoBehaviour
{
    private Color DefaultColor;
    private Renderer renderer;
    private GameObject selectionDecal; 
    void Start()
    {
        renderer = GetComponent<Renderer>();
        DefaultColor = renderer.material.color;
        renderer.material.color = Color.red;
        
    }

    private void OnDestroy()
    {
        renderer.material.color = DefaultColor;
    }
}