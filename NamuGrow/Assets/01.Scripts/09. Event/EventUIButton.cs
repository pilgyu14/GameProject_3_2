using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventUIButton : MonoBehaviour
{
    public float spiderFloat;
    public float beeFloat;
    public float antFloat;
    
    private float currentSpiderFloat;
    private float currentBeeFloat;
    private float currentAntFloat;
    
    public Slider spider;
    public Slider bee;
    public Slider ant;

    public Image spiderFill;
    public Image beeFill;
    public Image antFill;

    private void Start()
    {
        currentSpiderFloat = spider.value;
        currentBeeFloat = bee.value;
        currentAntFloat = ant.value;
    }


    public void MouseEnter()
    {
        Debug.Log("In");
        spider.value += spiderFloat;
        bee.value += beeFloat;
        ant.value += antFloat;
        
        ChangeColor(spider, spiderFill);
        ChangeColor(bee, beeFill);
        ChangeColor(ant, antFill);
    }

    public void MouseExit()
    {
        Debug.Log("Out");
        spider.value = currentSpiderFloat;
        bee.value = currentBeeFloat;
        ant.value = currentAntFloat;
        
        ChangeColor(spider, spiderFill);
        ChangeColor(bee, beeFill);
        ChangeColor(ant, antFill);
    }

    void ChangeColor(Slider a, Image aFill)
    {
        if (a.value >= 0.75f)
        {
            aFill.color = new Color32(77, 185, 222, 255);
        }
        else if(a.value <= 0.3f)
        {
            aFill.color = new Color32(253, 117, 118, 255);
        }
        else
        {
            aFill.color = new Color32(254, 200, 93, 255);
        }
        
    }
}
