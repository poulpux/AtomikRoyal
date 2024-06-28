using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class _StaticEnvironement 
{
    static public int[] maskInteraction {  get; private set; }
    static public int mapLenght { get; private set; }
    static public int tabResolution { get; private set; }
    static public GameObject wallPrefab;
    static public GameObject flammableWallPrefab;
    static public GameObject waterPrefab;
    static public GameObject bushPrefab;
    static public GameObject gluePrefab;
    static public GameObject smokePrefab;
    static public GameObject gazPrefab;
    static public GameObject firePrefab;
    static public GameObject explosionPrefab;
    static public List<ELEMENTS> elementsInteractionsPriority { get; private set; }
    static public void Init(StaticEnvironementSO SO)
    {
        mapLenght = SO.mapLenght;
        tabResolution = SO.tabResolution;
        elementsInteractionsPriority = SO.elementsInteractionsPriority;
        wallPrefab = SO.wallPrefab;
        flammableWallPrefab = SO.flammableWallPrefab;
        waterPrefab = SO.waterPrefab;
        bushPrefab = SO.bushPrefab;
        gluePrefab = SO.gluePrefab;
        smokePrefab = SO.smokePrefab;   
        gazPrefab = SO.gazPrefab;
        firePrefab = SO.firePrefab;
        explosionPrefab = SO.explosionPrefab;
        maskInteraction = new int[(int)GF.GetMaxValue<ELEMENTS>()];
        for (int i = 0; i < (int)GF.GetMaxValue<ELEMENTS>(); i++)
        {
            List<ELEMENTS> listTemps = GetListToRead(i, SO);
            int currentValue = 0;
            foreach (var item in listTemps)
                GF.AddInBinaryMask(ref currentValue, (int)item); 
            maskInteraction[i] = currentValue;
        }
    }

    static private List<ELEMENTS> GetListToRead(int index, StaticEnvironementSO SO)
    {
        InteractionElement interactionElement = SO.allInteractions.FirstOrDefault(e => e.currentElement == (ELEMENTS)index);
        if (interactionElement != null)
            return interactionElement.interactions;
        
        Debug.LogError("on ne trouve pas de currentElement pour chaque element de l'enum");

        return null;
    }
}
