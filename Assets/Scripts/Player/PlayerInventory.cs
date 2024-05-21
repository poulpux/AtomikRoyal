using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int cursorPos, nbCoins;
    List<Usable> Inventory;
    List<int> nbInInventory;
    List<string> cantThrowItem;
    int nbCasesInventory;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

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
        Inventory.Add(usable);
        nbInInventory.Add(nb <= usable.SO.nbMaxInventory ? nb : usable.SO.nbMaxInventory);
    }

    private void EchangeInventoryItem(Usable usable, int nb)
    {
        Inventory[cursorPos] = usable;
        nbInInventory[cursorPos] = nb <= usable.SO.nbMaxInventory ? nb : usable.SO.nbMaxInventory;
        //Jouer la logique pour poser l'autre object au sol
    }

    public void UseAnItem()
    {
        Inventory[cursorPos].Use();
        nbInInventory[cursorPos]--;
        if (nbInInventory[cursorPos] <= 0)
            Inventory[cursorPos] = null;
    }
}
