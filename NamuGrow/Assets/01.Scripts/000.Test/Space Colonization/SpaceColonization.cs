
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpaceColonization : MonoBehaviour
{
    [Header("Attraction points")] [SerializeField]
    private List<Vector3> attractionPointList = new List<Vector3>();

    [SerializeField] private int nbAttactionPoints = 200;
    [FormerlySerializedAs("radiusAttractionPOintsContainer")] [SerializeField] private int radiusAttractionPointsContainer = 10;

    [SerializeField] private GameObject sphere;
    
    public List<Vector3> AttractionPointList
    {
        get => attractionPointList;  
        set => attractionPointList = value; 
    }
    private void Start()
    {
        CreateAttractionParts();
    }

    void CreateAttractionParts()
    {
        for (int i = 0; i < nbAttactionPoints; i++)
        {
            attractionPointList.Add(Random.insideUnitSphere * radiusAttractionPointsContainer);

            foreach (var point in attractionPointList)
            {
                GameObject newSphere = Instantiate(sphere);
                newSphere.transform.position = point; 
            }
        }
    }
}

