using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public static class _StaticEnvironement 
{
    static public int[] maskInteraction {  get; private set; }
    static public int mapLenght;
    static public int tabResolution;
    static public void Init(StaticEnvironementSO SO)
    {
        mapLenght = SO.mapLenght;
        tabResolution = SO.tabResolution;

        maskInteraction = new int[(int)GF.GetMaxValue<ELEMENTS>()];
        for (int i = 0; i < (int)GF.GetMaxValue<ELEMENTS>(); i++)
        {
            List<ELEMENTS> listTemps = GetListToRead(i, SO);
            int currentValue = 0;
            foreach (var item in listTemps)
                currentValue +=(int)Mathf.Pow(2f, (float)item);
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
