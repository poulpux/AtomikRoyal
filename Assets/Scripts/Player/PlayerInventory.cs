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
    public List<string> cantUseItem = new List<string>();
    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        SetCursorEvent();
        infos.inputSystem.mouseScrollEvent.AddListener((side) => CursorMoveLogic(side));
        infos.inputSystem.isUsingUsableEvent.AddListener(() => UseItem());

        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            Inventory.Add(null);
        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            nbInInventory.Add(0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            nbCoins += collision.GetComponent<OnGroundCoins>().nb;
            Destroy(collision.gameObject);
        }
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void AddObject(UsableSO SO, int nbOnGround, UsableOnGround usableOnGround)
    {
        int index = 0;
        if (CheckIfObjectInCase(SO, ref index))
            CompleteACase(SO, nbOnGround, index, usableOnGround);
        else if (CheckIfVoidCase())
            AddInVoidCases(SO, nbOnGround, usableOnGround);
        else
            EchangeInventoryItem(SO, nbOnGround);
    }
    public void UseItem()
    {
        if (Inventory[cursorPos] == null || cantUseItem.Count != 0) return;
            Inventory[cursorPos].TryUse();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private bool CheckIfObjectInCase(UsableSO SO,ref int index)
    {
        List<string> itemNames = new List<string>();
        itemNames.Add(SO.name);
        int counter = 0;
        foreach (var item in Inventory)
        {
            if(item == null) continue;
            print(SO.name);
            if (nbInInventory[counter] != item.SO.nbMaxInventory  && item.SO.name == SO.name)
            {
                index = counter;
                return true;
            }
            counter++;
        }
        return false;
    }

    private bool CheckIfVoidCase()
    {
        int index = 0;
        foreach (var item in Inventory)
        {
            if (item == null)
                return true;
            index++;
        }
        return false;
    }

    private void SetCursorInventory(int nb)
    {
        print("appuis touche");
        cursorPos = nb;
    }

    private void CursorMoveLogic(int sub)
    {
        cursorPos = (cursorPos + sub) % _StaticPlayer.nbCasesInventory;
        if (cursorPos % _StaticPlayer.nbCasesInventory < 0f)
            cursorPos += _StaticPlayer.nbCasesInventory;
    }

    private void CompleteACase(UsableSO SO, int nb, int index, UsableOnGround usableOnGround)
    {
        if (nbInInventory[index] + nb <= SO.nbMaxInventory)
        {
            usableOnGround.nb = 0;
            nbInInventory[index] += nb;
        }
        else
        {
            int place = SO.nbMaxInventory - nbInInventory[index]; //positif
            nbInInventory[index] = SO.nbMaxInventory;
            usableOnGround.nb = nb - place;
        }
       
    }

    private void AddInVoidCases(UsableSO SO, int nb, UsableOnGround usableOnGround)
    {
        int index = GetIndexVoidCase();
        Inventory[index] = GF.SetScripts<Usable>(SO.script, gameObject);
        nbInInventory[index] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
        Inventory[index].UseEvent.AddListener(() => SubstractOneItem(index));
        Inventory[index].SO = SO;

        usableOnGround.nb = 0;

    }

    private int GetIndexVoidCase()
    {
        int index = 0;
        foreach (var item in Inventory)
        {
            if(item == null)
                return index;
            index ++;
        }
        return cursorPos;
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
        GameObject objet = _StaticChest.InstantiateUsable(transform.position, Inventory[cursorPos].SO, nbInInventory[cursorPos]);
        UsableOnGround onGround = objet.GetComponent<UsableOnGround>();
        onGround.nb = nbInInventory[cursorPos];
        onGround.SO = Inventory[cursorPos].SO;

        Destroy(Inventory[cursorPos]);
        Inventory[cursorPos] = GF.SetScripts<Usable>(SO.script, gameObject);
        Inventory[cursorPos].SO = SO;
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
