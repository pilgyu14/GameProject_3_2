
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TreeInstallPanel : MonoBehaviour
{
    public RectTransform panel; 
    public Button upDownBtn;

    public bool isOn = true; 
    // Start is called before the first frame update
    void Start()
    {
        upDownBtn.onClick.AddListener(UpDown);
        SetPanel(true); 
    }

    private void UpDown()
    {
        isOn = !isOn;
        SetPanel(isOn);
    }

    private void SetPanel(bool _isUp)
    {
        if (_isUp == true)
        {
            panel.transform.DOMoveY(0,0.2f);
        }
        else
        {
            panel.transform.DOMoveY(-panel.rect.height *2 - 30 ,0.2f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
