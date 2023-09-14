using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(menuName = "SO/Evnet/EventSO")]
public class EventSO : ScriptableObject
{
    public string text;

    public List<int> acceptOffer = new List<int>(new int[3]);
    public List<int> rejectionOffer = new List<int>(new int[3]);
    
    
}
