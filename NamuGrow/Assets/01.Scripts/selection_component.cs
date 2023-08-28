using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_component : MonoBehaviour
{
    private Color DefaultColor;
    private Renderer renderer;
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