using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEstInputSystem : MonoBehaviour
{
    PlayerInputSystem inputSystem;
    void Start()
    {
        inputSystem = GetComponent<PlayerInputSystem>();
        inputSystem.isOpeningInventoryEvent.AddListener(() => print("openInventory"));
        inputSystem.isOpeningMapEvent.AddListener(() => print("openMap"));
        inputSystem.inventoryEvent[0].AddListener(() => print("inventory " + 0.ToString()));
        inputSystem.inventoryEvent[1].AddListener(() => print("inventory " + 1.ToString()));
        inputSystem.inventoryEvent[2].AddListener(() => print("inventory " + 2.ToString()));
        inputSystem.inventoryEvent[3].AddListener(() => print("inventory " + 3.ToString()));
        inputSystem.inventoryEvent[4].AddListener(() => print("inventory " + 4.ToString()));
        inputSystem.inventoryEvent[5].AddListener(() => print("inventory " + 5.ToString()));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
