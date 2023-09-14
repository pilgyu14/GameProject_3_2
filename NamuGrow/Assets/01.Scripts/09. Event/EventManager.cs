using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{
    private EventSO nowEvent;
    
    public int spider;
    public int bee;
    public int ant;

    private int acceptSpider;
    private int acceptBee;
    private int acceptAnt;

    private int rejectionSpider;
    private int rejectionBee;
    private int rejectionAnt;
    
    void Start()
    {
        spider = 500;
        bee = 500;
        ant = 500;
    }

    public void EventSet(EventSO eventSo)
    {
        nowEvent = eventSo;

        acceptSpider = nowEvent.acceptOffer[0];
        acceptBee = nowEvent.acceptOffer[1];
        acceptAnt = nowEvent.acceptOffer[2];

        rejectionSpider = nowEvent.rejectionOffer[0];
        rejectionBee = nowEvent.rejectionOffer[1];
        rejectionAnt = nowEvent.rejectionOffer[2];
    }
    
    public void Set(int spiderChange, int beeChange, int antChange)
    {
        spider += spiderChange;
        bee += beeChange;
        ant += antChange;
    }
}
