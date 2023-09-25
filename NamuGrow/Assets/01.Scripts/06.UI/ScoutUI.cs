using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoutUI : MonoSingleton<ScoutUI>
{
    public TextMeshProUGUI unitName; 
    public Sprite unitSprite;
    public TextMeshProUGUI needGoodsType; 
    public TextMeshProUGUI needGoodsAmount; 
    public Button scoutBtn;
    public Button notScoutBtn;
    
    public UnitDataSO curUnitData;

    private void Awake()
    {
        notScoutBtn.onClick.AddListener(() => ActiveUI(false));
    }

    public void ActiveUI(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }

    public void SetUnitData(UnitDataSO unitDataSO, ITeam _unit)
    {
        curUnitData = unitDataSO;
        unitName.text = curUnitData.name; 
        needGoodsType.text = Enum.GetName(typeof(EnergyType), curUnitData.energyType); 
        needGoodsAmount.text = curUnitData.goodsCount.ToString();

        scoutBtn.onClick = null; 
        scoutBtn.onClick.AddListener(() =>
        {
            if (EnergyManager.Instance.IsEnough(curUnitData.energyType, (int)curUnitData.goodsCount) == true)
            {
                EnergyManager.Instance.RemoveEnergy(curUnitData.energyType, (int)curUnitData.goodsCount);
                _unit.ScoutUnit(TeamType.Player);
            }
            else
            {
                Debug.Log("영입 불가능");
            }
        });
    }
    
}