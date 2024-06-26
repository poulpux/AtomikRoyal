using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnviroInteractionManager : SingletonMother<EnviroInteractionManager>
{
    public int[,] binaryMaskMap ;
    [SerializeField] private List<EnviroInteractPackage> allInteractionsList = new List<EnviroInteractPackage>((int)GF.GetMaxValue<ELEMENTS>());
    [HideInInspector] public UnityEvent InstantiateAllInteractionEvent = new UnityEvent();
    public void Start()
    {
        binaryMaskMap = new int[_StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution, _StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution];

        VerifyAllPosibleInteraction();

        foreach (var item in allInteractionsList)
            item.Init();

        allInteractionsList[(int)ELEMENTS.WALL].PlayInteractions(5, 5);
    }

    private void VerifyAllPosibleInteraction()
    {
        for (int i = 0; i < (int)GF.GetMaxValue<ELEMENTS>(); i++)
        {
            for (int y = 0; y < (int)GF.GetMaxValue<ELEMENTS>(); y++)
            {
                if (!GF.IsOnBinaryMask(_StaticEnvironement.maskInteraction[i], y))
                {
                    EnviroInteractionMother currentInteraction = allInteractionsList[i].ReturnGoodMother(y);
                    if (currentInteraction != null)
                        currentInteraction.typeDisable.Add((ELEMENTS)y);
                }
            }
        }
    }
}

[Serializable]
public class EnviroInteractPackage
{
    public ELEMENTS elementType;
    public EnviroInteractionMother wallInteraction;
    public EnviroInteractionMother fireWallInteraction ;
    public EnviroInteractionMother waterInteraction ;
    public EnviroInteractionMother bushInteraction ;
    public EnviroInteractionMother glueInteraction ;
    public EnviroInteractionMother smokeInteraction ;
    public EnviroInteractionMother gazInteraction ;
    public EnviroInteractionMother fireInteraction ;
    public EnviroInteractionMother explosionInteraction ;

    /*[HideInInspector]*/ public List<EnviroInteractionMother> allEnableInteractions = new List<EnviroInteractionMother>();

    public void PlayInteractions(int x, int y)
    {
        foreach (var item in allEnableInteractions)
        {
            if (!GF.IsOnBinaryMask(EnviroInteractionManager.Instance.binaryMaskMap[x, y], (int)elementType))
                return;
            item.Interact(x, y);
        }
    }

    public void Init()
    {
        foreach (var item in _StaticEnvironement.elementsInteractionsPriority)
            AddToList(ReturnGoodMother((int)item), item);

        //AddToList(wallInteraction, ELEMENTS.WALL);
        //AddToList(fireWallInteraction, ELEMENTS.FLAMMABLEWALL);
        //AddToList(explosionInteraction, ELEMENTS.EXPLOSION);
        //AddToList(waterInteraction, ELEMENTS.WATER);
        //AddToList(fireInteraction, ELEMENTS.FIRE);
        //AddToList(gazInteraction, ELEMENTS.GAZ);
        //AddToList(smokeInteraction, ELEMENTS.SMOKE);
        //AddToList(glueInteraction, ELEMENTS.GLUE);
        //AddToList(bushInteraction, ELEMENTS.BUSH);

        wallInteraction = null;
        fireWallInteraction = null;
        explosionInteraction = null;
        waterInteraction = null;
        fireInteraction = null;
        gazInteraction = null;
        smokeInteraction = null;
        glueInteraction = null;
        bushInteraction = null;
    }

    public EnviroInteractionMother ReturnGoodMother(int enumNumber)
    {
        if (enumNumber == 0)
            return wallInteraction;
        else if (enumNumber == 1)
            return fireWallInteraction;
        else if (enumNumber == 2)
            return waterInteraction;
        else if (enumNumber == 3)
            return bushInteraction;
        else if (enumNumber == 4)
            return glueInteraction;
        else if (enumNumber == 5)
            return smokeInteraction;
        else if (enumNumber == 6)
            return gazInteraction;
        else if (enumNumber == 7)
            return fireInteraction;
        else if (enumNumber == 8)
            return explosionInteraction;

        return wallInteraction;
    }     

    private void AddToList(EnviroInteractionMother interaction, ELEMENTS element)
    {
        if (interaction != null && !interaction.typeDisable.Contains(element))            
            allEnableInteractions.Add(interaction);
    }
}
