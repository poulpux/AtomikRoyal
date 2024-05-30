using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IDesactiveWhenPlayerIsDead
{
    PlayerInfos infos;
    public int cursorPos { get; private set; }
    public int  nbCoins { get; private set; }

    private List<UsableMother> _inventory = new List<UsableMother>();
    private List<int> _nbInInventory = new List<int>();
    private List<string> _cantUseItem = new List<string>();

    public List<UsableMother> inventory
    {
        get { return _inventory; }
        private set { _inventory = value; }
    }

    public List<int> nbInInventory
    {
        get { return _nbInInventory; }
        private set { _nbInInventory = value; }
    }

    public List<string> cantUseItem
    {
        get { return _cantUseItem; }
        private set { _cantUseItem = value; }
    }

    void Start()
    {
        infos = GetComponent<PlayerInfos>();
        SetCursorEvent();
        infos.inputSystem.mouseScrollEvent.AddListener((side) => CursorMoveLogic(side));
        infos.inputSystem.isUsingUsableEvent.AddListener(() => UseItem());

        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            _inventory.Add(null);
        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
            _nbInInventory.Add(0);
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
        if (_inventory[cursorPos] == null || _cantUseItem.Count != 0) return;
            _inventory[cursorPos].TryUse();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private bool CheckIfObjectInCase(UsableSOMother SO,ref int index)
    {
        List<string> itemNames = new List<string>();
        itemNames.Add(SO.name);
        int counter = 0;
        foreach (var item in _inventory)
        {
            if(item == null) continue;
            print(SO.name);
            if (_nbInInventory[counter] != item.SO.nbMaxInventory  && item.SO.name == SO.name)
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
        foreach (var item in _inventory)
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
        if (_nbInInventory[index] + nb <= SO.nbMaxInventory)
        {
            usableOnGround.nb = 0;
            _nbInInventory[index] += nb;
        }
        else
        {
            int place = SO.nbMaxInventory - _nbInInventory[index]; //positif
            _nbInInventory[index] = SO.nbMaxInventory;
            usableOnGround.nb = nb - place;
        }
       
    }

    private void AddInVoidCases(UsableSOMother SO, int nb, UsableOnGround usableOnGround)
    {
        int index = GetIndexVoidCase();
        _inventory[index] = GF.SetScripts<UsableMother>(SO.script, gameObject);
        _nbInInventory[index] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
        _inventory[index].UseEvent.AddListener(() => SubstractOneItem(index));
        _inventory[index].SO = SO;

        usableOnGround.nb = 0;

    }

    private int GetIndexVoidCase()
    {
        int index = 0;
        foreach (var item in _inventory)
        {
            if(item == null)
                return index;
            index ++;
        }
        return cursorPos;
    }

    private void SubstractOneItem(int index)
    {
        _nbInInventory[index] -= 1;
        if (_nbInInventory[index] <= 0)
        {
            _inventory[index].UseEvent.RemoveAllListeners();
            DestroyCurrentItem(index);
        }
    }

    private void EchangeInventoryItem(UsableSOMother SO, int nb)
    {
        GameObject objet = _StaticChest.InstantiateUsable(transform.position, _inventory[cursorPos].SO, _nbInInventory[cursorPos]);
        UsableOnGround onGround = objet.GetComponent<UsableOnGround>();
        onGround.nb = _nbInInventory[cursorPos];
        onGround.SO = _inventory[cursorPos].SO;

        Destroy(_inventory[cursorPos]);
        _inventory[cursorPos] = GF.SetScripts<UsableMother>(SO.script, gameObject);
        _inventory[cursorPos].SO = SO;
        _nbInInventory[cursorPos] = nb <= SO.nbMaxInventory ? nb : SO.nbMaxInventory;
    }


    private void DestroyCurrentItem(int index)
    {
        Destroy(_inventory[index]);
        _inventory[index] = null;
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
        foreach (var item in _inventory)
            Destroy(item);
        enabled = false;
    }
}
