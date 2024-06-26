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

        foreach (var item in allInteractionsList)
            item.Init();

        GF.AddInBinaryMask(ref binaryMaskMap[5, 5], (int)ELEMENTS.GLUE);
        GF.AddInBinaryMask(ref binaryMaskMap[5, 5], (int)ELEMENTS.WALL);
        print(GF.IsOnBinaryMask(binaryMaskMap[5,5], (int)ELEMENTS.WALL));
        allInteractionsList[(int)ELEMENTS.WALL].PlayInteractions(5, 5);
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

    /*[HideInInspector]*/ public List<EnviroInteractionMother> allEnableInteractions ;

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
        AddToList(wallInteraction);
        AddToList(fireWallInteraction);
        AddToList(explosionInteraction);
        AddToList(waterInteraction);
        AddToList(fireInteraction);
        AddToList(gazInteraction);
        AddToList(smokeInteraction);
        AddToList(glueInteraction);
        AddToList(bushInteraction);

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

    private void AddToList(EnviroInteractionMother interaction)
    {
        if (interaction != null && !interaction.typeDisable.Contains(elementType))
            allEnableInteractions.Add(interaction);
    }
}
