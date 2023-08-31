using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 업그레이드시 나무 기능 변경 
/// </summary>
public interface IUpdateUpgrade
{
    public void UpdateUpgrade();
}
/// <summary>
/// 병력 수 증가 인터페이스 
/// </summary>
public interface IAddTroops
{
    public void AddTroops(int _troops);     
    public int Troops { get;  }
}
/// <summary>
/// 증식 업그레이드 인터페이스 
/// </summary>
public interface IIncreaseUpgrade
{
    public bool IncreaseUpgrade(); // 개수 증가 
}

public interface IProduceEnergy
{
    // 생산량 
    public int ProduceAmount { get; set;  }
    public EnergyType ProduceEnergyType { get;  }
    public float ProduceEnergy(); //에너지 얻기 

}

/// <summary>
/// 나무 요소 인터페이스 
/// </summary>
public interface ITreeElement
{
    // 나를 가지고 있는 나무
    // 업그레이드 return bool // 각각 자신ㄴ에 맞는 재료 ㅇㅇ 
    // 업그레이드 필요 재료 연동 설정 
    public TreeType TreeType { get;  }
    public bool Upgrade(); // 크기 강화 
}

public abstract class AbTreeElement<T> : MonoBehaviour, ITreeElement where T : AbTreeBase
{
    public T ownerTree;

    public abstract  EnergyType ProduceEnergyType { get; }
    public TreeType TreeType { get; }
    public abstract bool Upgrade();
}
