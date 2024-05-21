using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public enum CONFIG
{
    Default,
}
public class PlayerInputSystem : MonoBehaviour
{
    //Toutes les variables interressantes
    [HideInInspector] public UnityEvent isUsingUsableEvent = new UnityEvent(), isOpeningInventoryEvent = new UnityEvent(), isOpeningMapEvent = new UnityEvent();
    [HideInInspector] public List<UnityEvent> inventoryEvent;
    public Vector2 direction;
    public bool isInteracting;

    class Button
    {
        public string name;
        public bool canUpdate;
    }

    PlayerInput playerInput;
    private InputActionMap DefaultActions;
    List<Button> buttonList  = new List<Button>();

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        DefaultActions = playerInput.actions.FindActionMap("Default");

        isUsingUsableEvent.AddListener(() => print("marche"));

        EnableActionMap(DefaultActions);
        SetAllButton();
        SetButtonInventoryList();
    }

    private void Update()
    {
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
    }

    private void SetButtonInventoryList()
    {
        for (int i = 0; i < 6; i++)
            inventoryEvent.Add(new UnityEvent());
    }

    private void SwitchConfig(CONFIG config)
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(config.ToString());
        playerInput.currentActionMap.Enable();
    }

    private void EnableActionMap(InputActionMap action)
    {
        action["Move"].performed += MoveAct;
        action["Move"].canceled += MoveSleep;
        action["Interact"].performed += InteractAct;
        action["Interact"].canceled += InteractSleep;


        //All buttons
        action["UseUsable"].started += UseUsableAct;
        action["UseUsable"].canceled += UseUsableSleep;
        action["OpenInventory"].started += OpenInventoryAct;
        action["OpenInventory"].canceled += OpenInventorySleep;
        action["OpenMap"].started += OpenMapAct;
        action["OpenMap"].canceled += OpenMapSleep;
        action["Inventory1"].started += Inventory1Act;
        action["Inventory1"].canceled += Inventory1Sleep;
        action["Inventory2"].started += Inventory2Act;
        action["Inventory2"].canceled += Inventory2Sleep;
        action["Inventory3"].started += Inventory3Act;
        action["Inventory3"].canceled += Inventory3Sleep;
        action["Inventory4"].started += Inventory4Act;
        action["Inventory4"].canceled += Inventory4Sleep;
        action["Inventory5"].started += Inventory5Act;
        action["Inventory5"].canceled += Inventory5Sleep;
        action["Inventory6"].started += Inventory6Act;
        action["Inventory6"].canceled += Inventory6Sleep;
    }

    private void DisableActionMap(InputActionMap action)
    {
        action["Move"].performed -= MoveAct;
        action["Move"].canceled -= MoveSleep;
        action["Interact"].performed -= InteractAct;
        action["Interact"].canceled -= InteractSleep;


        //All buttons
        action["UseUsable"].started -= UseUsableAct;
        action["UseUsable"].canceled -= UseUsableSleep;
        action["OpenInventory"].started -= OpenInventoryAct;
        action["OpenInventory"].canceled -= OpenInventorySleep;
        action["OpenMap"].started -= OpenMapAct;
        action["OpenMap"].canceled -= OpenMapSleep;
        action["Inventory1"].started -= Inventory1Act;
        action["Inventory1"].canceled -= Inventory1Sleep;
        action["Inventory2"].started -= Inventory2Act;
        action["Inventory2"].canceled -= Inventory2Sleep;
        action["Inventory3"].started -= Inventory3Act;
        action["Inventory3"].canceled -= Inventory3Sleep;
        action["Inventory4"].started -= Inventory4Act;
        action["Inventory4"].canceled -= Inventory4Sleep;
        action["Inventory5"].started -= Inventory5Act;
        action["Inventory5"].canceled -= Inventory5Sleep;
        action["Inventory6"].started -= Inventory6Act;
        action["Inventory6"].canceled -= Inventory6Sleep;
    }

    private void MoveAct(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }
    
    private void MoveSleep(InputAction.CallbackContext value)
    {
        direction = Vector2.zero;
    }
    
    private void InteractAct(InputAction.CallbackContext value)
    {
        isInteracting = value.ReadValue<float>() > 0f;
    }
    
    private void InteractSleep(InputAction.CallbackContext value)
    {
        isInteracting = false;
    }

    private void UseUsableAct(InputAction.CallbackContext value)
    {
        ButtonAct(0);
    }

    private void UseUsableSleep(InputAction.CallbackContext value)
    {
        ButtonSleep(0);
    }

    private void OpenInventoryAct(InputAction.CallbackContext value)
    {
        ButtonAct(1);
    }
    
    private void OpenInventorySleep(InputAction.CallbackContext value)
    {
        ButtonSleep(1);
    }
    
    private void OpenMapAct(InputAction.CallbackContext value)
    {
        ButtonAct(2);
    }
    
    private void OpenMapSleep(InputAction.CallbackContext value)
    {
        ButtonSleep(2);
    }
    
    private void Inventory1Act(InputAction.CallbackContext value)
    {
        ButtonAct(3);
    }
    
    private void Inventory1Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(3);
    }
    private void Inventory2Act(InputAction.CallbackContext value)
    {
        ButtonAct(4);
    }
    
    private void Inventory2Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(4);
    }
    private void Inventory3Act(InputAction.CallbackContext value)
    {
        ButtonAct(5);
    }
    
    private void Inventory3Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(5);
    }

    private void Inventory4Act(InputAction.CallbackContext value)
    {
        ButtonAct(6);
    }
    
    private void Inventory4Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(6);
    }
    
    private void Inventory5Act(InputAction.CallbackContext value)
    {
        ButtonAct(7);
    }
    
    private void Inventory5Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(7);
    }
    
    private void Inventory6Act(InputAction.CallbackContext value)
    {
        ButtonAct(8);
    }
    
    private void Inventory6Sleep(InputAction.CallbackContext value)
    {
        ButtonSleep(8);
    }

    private void ButtonAct(int index)
    {
        if (buttonList[0].canUpdate)
            GetEvent(index).Invoke();
        buttonList[0].canUpdate = false;
    }

    private void ButtonSleep(int index)
    {
        buttonList[0].canUpdate = true;
    }

    private void SetAllButton()
    {
        SetButton("UseUsable");
        SetButton("OpenInventory");
        SetButton("OpenMap");
        SetButton("Inventory1");
        SetButton("Inventory2");
        SetButton("Inventory3");
        SetButton("Inventory4");
        SetButton("Inventory5");
        SetButton("Inventory6");
    }

    private void SetButton(string name)
    {
        Button button = new Button();
        button.name = name;
        buttonList.Add(button);
    }

    private UnityEvent GetEvent(int index)
    {
        if (index == 0)
            return isUsingUsableEvent;
        else if (index == 1)
            return isOpeningInventoryEvent;
        else if (index == 2)
            return isOpeningMapEvent;
        else
        {
            return inventoryEvent[index - 3];
        }
    }
}
