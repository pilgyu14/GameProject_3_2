using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 증식 업그레이드 인터페이스 
/// </summary>
public interface IIncreaseUpgrade
{
    public bool IncreaseUpgrade(); // 개수 증가 
}

/// <summary>
/// 나무 요소 인터페이스 
/// </summary>
public interface ITreeElement
{
    // 나를 가지고 있는 나무
    // 업그레이드 return bool // 각각 자신ㄴ에 맞는 재료 ㅇㅇ 
    // 업그레이드 필요 재료 연동 설정 
    public EnergyType ProduceEnergyType { get;  }
    public bool UpgradeSize(); // 크기 강화 
    public float ProduceEnergy(); //에너 얻기 
}

public abstract class AbTreeElement<T> : MonoBehaviour, ITreeElement where T : AbTreeBase
{
    public T ownerTree;

    public abstract  EnergyType ProduceEnergyType { get; }
    public abstract bool UpgradeSize();
    public abstract float ProduceEnergy();
}
