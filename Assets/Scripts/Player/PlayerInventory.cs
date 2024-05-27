using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    public int cursorPos, nbCoins;
    List<Usable> Inventory = new List<Usable>();
    List<int> nbInInventory = new List<int>();
    List<string> cantThrowItem = new List<string>();
    int nbCasesInventory;
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        SetCursorEvent();
        infos.inputSystem.mouseScrollEvent.AddListener((side) => CursorMoveLogic(side));
    }
    void Update()
    {
        
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void DontThrowItem(string nameOfInterdiction)
    {
        cantThrowItem.Add(nameOfInterdiction);
    }

    public void CanThrowItem(string nameOfInterdiction)
    {
        cantThrowItem.RemoveAll(delete => delete == nameOfInterdiction);
    }

    public void AddObject(Usable usable)
    {
        if (Inventory.Contains(usable))
            CompleteACase(usable, usable.SO.nbRecolted /* remplacerParLeNbDobjetsAuSOl*/);
        else if (Inventory.Count < nbCasesInventory)
            AddInVoidCases(usable, usable.SO.nbRecolted /* remplacerParLeNbDobjetsAuSOl*/);
        else
            EchangeInventoryItem(usable, usable.SO.nbRecolted/* remplacerParLeNbDobjetsAuSOl*/);
    }
    public void UseAnItem()
    {
        Inventory[cursorPos].TryUse();
        nbInInventory[cursorPos]--;
        if (nbInInventory[cursorPos] <= 0)
            DestroyCurrentItem();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetCursorInventory(int nb)
    {
        cursorPos = nb;
    }

    private void CursorMoveLogic(int sub)
    {
        cursorPos = (cursorPos + sub) % _StaticPlayer.nbCaseInventory + ((cursorPos + sub) % _StaticPlayer.nbCaseInventory < 0f ? _StaticPlayer.nbCaseInventory : 0);
    }

    private void CompleteACase(Usable usable, int nb)
    {
        int index = Inventory.FindIndex(listIndex => listIndex == usable);
        if (nbInInventory[index]+nb <= usable.SO.nbMaxInventory)
            nbInInventory[index] += nb;
        else
        {
            nbInInventory[index] = usable.SO.nbMaxInventory;
            //Play Logic Of put down item
        }
       
    }

    private void AddInVoidCases(Usable usable, int nb)
    {
        Inventory.Add(GF.SetScripts<Usable>(usable.SO.script, gameObject));
        nbInInventory.Add(nb <= usable.SO.nbMaxInventory ? nb : usable.SO.nbMaxInventory);
    }

    private void EchangeInventoryItem(Usable usable, int nb)
    {
        Inventory[cursorPos] = usable;
        nbInInventory[cursorPos] = nb <= usable.SO.nbMaxInventory ? nb : usable.SO.nbMaxInventory;
        //Jouer la logique pour poser l'autre object au sol
    }


    private void DestroyCurrentItem()
    {
        Destroy(Inventory[cursorPos]);
        Inventory[cursorPos] = null;
    }

    private void SetCursorEvent()
    {
        for (int i = 0; i < _StaticPlayer.nbCaseInventory; i++)
        {
            int index = i;
            infos.inputSystem.inventoryEvent[index].AddListener(() => SetCursorInventory(index));
        }
    }

    public void WhenDead()
    {
        foreach (var item in Inventory)
            Destroy(item);
        enabled = false;
    }
}
