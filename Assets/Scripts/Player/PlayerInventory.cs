using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IDesactiveWhenPlayerIsDead, IActWhenPlayerIsDead
{
    PlayerInfos infos;
    public int cursorPos { get; private set; }
    public int nbCoins;

    public List<UsableMother> inventory { get; private set; } = new List<UsableMother>();
    public List<int> nbInInventory { get; private set; } = new List<int>();
    public List<string> cantUseItem { get; private set; } = new List<string>();

    void Start()
    {
        nbCoins = 100;
        infos = GetComponent<PlayerInfos>();
        SetCursorEvent();
        infos.inputSystem.mouseScrollEvent.AddListener((side) => CursorMoveLogic(side));
        infos.inputSystem.isUsingUsableEvent.AddListener(() => UseItem());


        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            inventory.Add(null);
        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            nbInInventory.Add(0);
        
        //Permet d'ajouter le punch, qui doit toujours être en première position de la liste
        AddObject(_StaticChest.ToFindInChestUtility[0], 5, null);
    }

    public void WhenDied()
    {
        foreach (var item in inventory)
            Destroy(item);
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

    public void AddObject(UsableSOMother SO, int nbOnGround, UsableOnGround usableOnGround)
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
        if (inventory[cursorPos] == null || cantUseItem.Count != 0) return;
        
        inventory[cursorPos].TryUse();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private bool CheckIfObjectInCase(UsableSOMother SO,ref int index)
    {
        List<string> itemNames = new List<string>();
        itemNames.Add(SO.name);
        int counter = 0;
        foreach (var item in inventory)
        {
            if(item == null) continue;
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
        foreach (var item in inventory)
        {
            if (item == null)
                return true;
            index++;
        }
        return false;
    }

    private void SetCursorInventory(int nb)
    {
        cursorPos = nb;
    }

    private void CursorMoveLogic(int sub)
    {
        cursorPos = (cursorPos + sub) % _StaticPlayer.nbCasesInventory;
        if (cursorPos % _StaticPlayer.nbCasesInventory < 0f)
            cursorPos += _StaticPlayer.nbCasesInventory;
    }

    private void CompleteACase(UsableSOMother SO, int nb, int index, UsableOnGround usableOnGround)
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

    private void AddInVoidCases(UsableSOMother SO, int nb, UsableOnGround usableOnGround)
    {
        int index = GetIndexVoidCase();
        inventory[index] = GF.SetScripts<UsableMother>(SO.script, gameObject);
        nbInInventory[index] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
        inventory[index].UseEvent.AddListener(() => SubstractOneItem(index));
        inventory[index].SO = SO;

        if(usableOnGround != null)
            usableOnGround.nb = 0;
    }

    private int GetIndexVoidCase()
    {
        int index = 0;
        foreach (var item in inventory)
        {
            if(item == null)
                return index;
            index ++;
        }
        return cursorPos;
    }

    private void SubstractOneItem(int index)
    {
        if(index == 0) return; //Permet d'utiliser à l'infini les poings

        nbInInventory[index] -= 1;
        if (nbInInventory[index] <= 0)
        {
            inventory[index].UseEvent.RemoveAllListeners();
            DestroyCurrentItem(index);
        }
    }

    private void EchangeInventoryItem(UsableSOMother SO, int nb)
    {
        GameObject objet = _StaticChest.InstantiateUsable(transform.position, inventory[cursorPos].SO, nbInInventory[cursorPos]);
        UsableOnGround onGround = objet.GetComponent<UsableOnGround>();
        onGround.nb = nbInInventory[cursorPos];
        onGround.SO = inventory[cursorPos].SO;

        Destroy(inventory[cursorPos]);
        inventory[cursorPos] = GF.SetScripts<UsableMother>(SO.script, gameObject);
        inventory[cursorPos].SO = SO;
        nbInInventory[cursorPos] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
    }

    private void DestroyCurrentItem(int index)
    {
        Destroy(inventory[index]);
        inventory[index] = null;
    }

    private void SetCursorEvent()
    {
        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
        {
            int index = i;
            infos.inputSystem.inventoryEvent[index].AddListener(() => SetCursorInventory(index));
        }
    }
}
