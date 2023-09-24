using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    private EnergyManager energyManager;

    public TextMeshProUGUI sapText;
    public TextMeshProUGUI sweetSapText;
    public TextMeshProUGUI acornText;
    public TextMeshProUGUI kiwiText;
    
    void Start()
    {
        energyManager = EnergyManager.Instance;
    }

    void Update()
    {
        sapText.text = EnergyManager.Instance.Sap.ToString() + "L";
        sweetSapText.text = EnergyManager.Instance.SweetSap.ToString() + "L";
        acornText.text = EnergyManager.Instance.Acorn.ToString() + "개";
        kiwiText.text = EnergyManager.Instance.Kiwi.ToString() + "무나";
    }
}
