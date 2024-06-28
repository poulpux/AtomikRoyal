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

    //[Header("Fire")]
    static public float CDWDamageFire { get; private set; }
    static public int damageFire { get; private set; }

    //[Header("GAZ")]
    static public float CDWDamageGaz { get; private set; }
    static public int damageGaz { get; private set; }

    //[Header("Ring")]
    //[Space(10)]
    static public float CDWDamageRing { get; private set; }
    static public AnimationCurve damageRingCurve { get; private set; }

    static public GameObject wallPrefab { get; private set; }
    static public GameObject flammableWallPrefab { get; private set; }
    static public GameObject waterPrefab { get; private set; }
    static public GameObject bushPrefab { get; private set; }
    static public GameObject gluePrefab { get; private set; }
    static public GameObject smokePrefab { get; private set; }
    static public GameObject gazPrefab { get; private set; }
    static public GameObject firePrefab { get; private set; }
    static public GameObject explosionPrefab { get; private set; }

    static public string enviroLayerName{ get; private set; }
    static public string waterTag { get; private set; }
    static public string bushTag { get; private set; }
    static public string glueTag { get; private set; }
    static public string smokeTag { get; private set; }
    static public string gazTag { get; private set; }
    static public string fireTag { get; private set; }
    static public string ringTag { get; private set; }
    static public List<ELEMENTS> elementsInteractionsPriority { get; private set; }


    static public void Init(StaticEnvironementSO SO)
    {
        mapLenght = SO.mapLenght;
        tabResolution = SO.tabResolution;
        elementsInteractionsPriority = SO.elementsInteractionsPriority;

        CDWDamageFire = SO.CDWDamageFire;
        damageFire = SO.damageFire;

        CDWDamageGaz = SO.CDWDamageGaz;
        damageGaz = SO.damageGaz;

        CDWDamageRing = SO.CDWDamageRing;
        damageRingCurve = SO.damageRingCurve;

        wallPrefab = SO.wallPrefab;
        flammableWallPrefab = SO.flammableWallPrefab;
        waterPrefab = SO.waterPrefab;
        bushPrefab = SO.bushPrefab;
        gluePrefab = SO.gluePrefab;
        smokePrefab = SO.smokePrefab;   
        gazPrefab = SO.gazPrefab;
        firePrefab = SO.firePrefab;
        explosionPrefab = SO.explosionPrefab;
        ringTag = SO.ringTag;

        enviroLayerName = SO.enviroLayerName;
        waterTag = SO.waterTag;
        bushTag = SO.bushTag;
        glueTag = SO.glueTag;
        smokeTag = SO.smokeTag;
        gazTag = SO.gazTag;
        fireTag = SO.fireTag;

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

    static public int GetDamageOfZone(int nbRingClosed) =>
    (int)damageRingCurve.Evaluate(nbRingClosed);
}
