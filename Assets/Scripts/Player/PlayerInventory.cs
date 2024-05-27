using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    public int cursorPos, nbCoins;
    public List<Usable> Inventory = new List<Usable>();
    public List<int> nbInInventory = new List<int>();
    List<string> cantThrowItem = new List<string>();
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
        else if (Inventory.Count <= _StaticPlayer.nbCasesInventory)
            AddInVoidCases(usable, usable.SO.nbRecolted /* remplacerParLeNbDobjetsAuSOl*/);
        else
            EchangeInventoryItem(usable, usable.SO.nbRecolted/* remplacerParLeNbDobjetsAuSOl*/);
    }
    public void UseAnItem()
    {
        Inventory[cursorPos].TryUse();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void SetCursorInventory(int nb)
    {
        cursorPos = nb;
    }

    private void CursorMoveLogic(int sub)
    {
        cursorPos = (cursorPos + sub) % _StaticPlayer.nbCasesInventory + ((cursorPos + sub) % _StaticPlayer.nbCasesInventory < 0f ? _StaticPlayer.nbCasesInventory : 0);
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
        Inventory[Inventory.Count-1].UseEvent.AddListener(() => SubstractOneItem(Inventory.Count-1));
    }

    private void SubstractOneItem(int index)
    {
        nbInInventory[index] -= 1;
        if (nbInInventory[index] <= 0)
        {
            Inventory[index].UseEvent.RemoveAllListeners();
            DestroyCurrentItem(index);
        }
    }

    private void EchangeInventoryItem(Usable usable, int nb)
    {

        print("echange : ");
        Inventory[cursorPos] = usable;
        nbInInventory[cursorPos] = nb <= usable.SO.nbMaxInventory ? nb : usable.SO.nbMaxInventory;
        //Jouer la logique pour poser l'autre object au sol
    }


    private void DestroyCurrentItem(int index)
    {
        Destroy(Inventory[index]);
        Inventory[index] = null;
    }

    private void SetCursorEvent()
    {
        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
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
