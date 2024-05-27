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

        infos.inputSystem.isUsingUsableEvent.AddListener(() => UseItem());
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

    public void AddObject(UsableSO SO, int nbOnGround, UsableOnGround usableOnGround)
    {
        int index = 0;
        if (CheckIfObjectInCase(SO, ref index))
            CompleteACase(SO, nbOnGround, index, usableOnGround);
        else if (Inventory.Count < _StaticPlayer.nbCasesInventory)
            AddInVoidCases(SO, nbOnGround);
        else
            EchangeInventoryItem(SO, nbOnGround);
    }
    public void UseItem()
    {
        if(Inventory.Count - 1 >= cursorPos && Inventory[cursorPos-1] != null)
            Inventory[cursorPos-1].TryUse();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private bool CheckIfObjectInCase(UsableSO SO,ref int index)
    {
        HashSet<string> itemNames = new HashSet<string>();
        itemNames.Add(SO.name);
        int counter = 0;
        foreach (var item in Inventory)
        {
            if(item == null || item.SO == null) continue;
            if (nbInInventory[counter] != Inventory[counter].SO.nbMaxInventory && !itemNames.Add(item.SO.name) )
            {
                index = counter;
                return true;
            }
            counter++;
        }
        return false;
    }

    private void SetCursorInventory(int nb)
    {
        cursorPos = nb;
    }

    private void CursorMoveLogic(int sub)
    {
        cursorPos = (cursorPos + sub) % _StaticPlayer.nbCasesInventory + ((cursorPos + sub) % _StaticPlayer.nbCasesInventory < 0f ? _StaticPlayer.nbCasesInventory : 0);
    }

    private void CompleteACase(UsableSO SO, int nb, int index, UsableOnGround usableOnGround)
    {
        print("complete"+ index);
        if (nbInInventory[index]+nb <= SO.nbMaxInventory)
            nbInInventory[index] += nb;
        else
        {
            nb -= SO.nbMaxInventory - nbInInventory[index];
            nbInInventory[index] = SO.nbMaxInventory;

            if(Inventory.Count <= _StaticPlayer.nbCasesInventory)
                AddInVoidCases(SO, nb);
            else
                usableOnGround.nb = nb;
            //Play Logic Of put down item
        }
       
    }

    private void AddInVoidCases(UsableSO SO, int nb)
    {
        Inventory.Add(GF.SetScripts<Usable>(SO.script, gameObject));
        nbInInventory.Add(nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory);
        Inventory[Inventory.Count-1].UseEvent.AddListener(() => SubstractOneItem(Inventory.Count-1));
        Inventory[Inventory.Count - 1].SO = SO;
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

    private void EchangeInventoryItem(UsableSO SO, int nb)
    {
        Destroy(Inventory[cursorPos]);
        Inventory[cursorPos] = GF.SetScripts<Usable>(SO.script, gameObject);
        GameObject objet = _StaticChest.InstantiateUsable(transform.position, Inventory[cursorPos].SO, nbInInventory[cursorPos]);
        objet.GetComponent<UsableOnGround>().nb = nbInInventory[cursorPos];
        nbInInventory[cursorPos] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
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
