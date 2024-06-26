using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnviroInteractionManager : SingletonMother<EnviroInteractionManager>
{
    public EnviroCase[,] binaryMaskMap { get; private set; }
    [SerializeField] private List<EnviroInteractPackage> allInteractionsList = new List<EnviroInteractPackage>((int)GF.GetMaxValue<ELEMENTS>());

    [HideInInspector] public UnityEvent<int, int, ELEMENTS> AddElementEvent = new UnityEvent<int, int, ELEMENTS>();
    [HideInInspector] public UnityEvent<int, int, ELEMENTS> RemoveElementEvent = new UnityEvent<int, int, ELEMENTS>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Start()
    {
        InstantiateTab();

        AddElementEvent.AddListener((x, y, element) => AddElement(x, y, element));
        RemoveElementEvent.AddListener((x, y, element) => GF.RemoveInBinaryMask(ref binaryMaskMap[x, y].binaryMask, (int)element));

        foreach (var item in allInteractionsList)
            item.Init();


        //AddElementEvent.Invoke(5, 5, ELEMENTS.WATER);
        //print(binaryMaskMap[5, 5].binaryMask);
        //AddElementEvent.Invoke(5, 5, ELEMENTS.FIRE);
        //print(binaryMaskMap[5, 5].binaryMask);

        //AddElementEvent.Invoke(5, 5, ELEMENTS.BUSH);
        //print(binaryMaskMap[5, 5].binaryMask);
        //AddElementEvent.Invoke(5, 5, ELEMENTS.GLUE);
        //print(binaryMaskMap[5, 5].binaryMask);
        //RemoveElementEvent.Invoke(5, 5, ELEMENTS.WATER);
        //print(binaryMaskMap[5, 5].binaryMask);
        //AddElementEvent.Invoke(5, 5, ELEMENTS.GLUE);
        //print(binaryMaskMap[5, 5].binaryMask);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void AddElement(int x, int y, ELEMENTS element)
    {
        GF.AddInBinaryMask(ref binaryMaskMap[x, y].binaryMask, (int)element);
        allInteractionsList[(int)element].PlayInteractions(x, y);
    }

    private void InstantiateTab()
    {
        binaryMaskMap = new EnviroCase[_StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution, _StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution];
        for (int i = 0; i < _StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution; i++)
        {
            for (int y = 0; y < _StaticEnvironement.mapLenght / _StaticEnvironement.tabResolution; y++)
                binaryMaskMap[i, y] = new EnviroCase();
        }
    }
}

public class EnviroCase
{
    public int binaryMask;
    public float flammableHp;
}

[Serializable]
public class EnviroInteractPackage
{
    [Serializable]
    public class ConditionEnviro
    {
        public EnviroInteractionMother script;
        public ELEMENTS interactWith;

        public ConditionEnviro(EnviroInteractionMother script, ELEMENTS interactWith)
        {
            this.script = script;
            this.interactWith = interactWith;
        }

    }
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


    /*[HideInInspector]*/ public List<ConditionEnviro> allEnableInteractions = new List<ConditionEnviro>();

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void PlayInteractions(int x, int y)
    {
        foreach (var item in allEnableInteractions)
        {
            if (!GF.IsOnBinaryMask(EnviroInteractionManager.Instance.binaryMaskMap[x, y].binaryMask, (int)elementType))
                return;
            if(GF.IsOnBinaryMask(EnviroInteractionManager.Instance.binaryMaskMap[x, y].binaryMask, (int)item.interactWith))
                item.script.Interact(x, y);
        }
    }

    public void Init()
    {
        foreach (var item in _StaticEnvironement.elementsInteractionsPriority)
            AddToList(ReturnGoodMother((int)item), item);

        //Priorités
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

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void AddToList(EnviroInteractionMother interaction, ELEMENTS element)
    {
        if (interaction != null)            
            allEnableInteractions.Add(new ConditionEnviro(interaction, element));
    }
}
