using System;
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
    [HideInInspector] public UnityEvent needToGoUpInteractEvent = new UnityEvent();
    [HideInInspector] public UnityEvent<int> mouseScrollEvent = new UnityEvent<int>();
    [HideInInspector] public List<UnityEvent> inventoryEvent = new List<UnityEvent>();
    [HideInInspector] public List<UnityEvent> upgradeStatEvent = new List<UnityEvent>();
    [HideInInspector] public Vector2 direction { get; private set; }
    [HideInInspector] public Vector2 mousePos { get; private set; }
    [HideInInspector] public Vector2 mouseDirection { get; private set; }
    [HideInInspector] public bool isInteracting { get; private set; }
    private float scrollMouse, scrollMouseTimer;

    PlayerInfos infos;
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
        infos = GetComponent<PlayerInfos>();
        playerInput = GetComponent<PlayerInput>();
        DefaultActions = playerInput.actions.FindActionMap("Default");
        needToGoUpInteractEvent.AddListener(() => isInteracting = false);
    }
    private void OnEnable()
    {
        SetAllButton();
        SetButtonInventoryList();
        SetButtonUpgradeStatList();
        EnableActionMap(DefaultActions);
    }

    private void FixedUpdate()
    {
        ThrowScrollMouseEvent();
    }



    private void OnDisable()
    {
        DisableActionMap(DefaultActions);
    }

    //1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111

    private void ThrowScrollMouseEvent()
    {
        if (scrollMouse == 0)
            return;

        scrollMouseTimer = scrollMouse > 0f && scrollMouseTimer >= 0f? scrollMouseTimer + Time.fixedDeltaTime : 
                           scrollMouse > 0f ? 0f : 
                           scrollMouse < 0f && scrollMouseTimer <= 0f ? scrollMouseTimer - Time.fixedDeltaTime : 0f;

        if(Mathf.Abs( scrollMouseTimer) > _StaticPlayer.scrollCdwCursor)
        {
            scrollMouseTimer = 0;
            mouseScrollEvent.Invoke(scrollMouse > 0 ? 1 : -1);
        }
    }

    private void SetButtonInventoryList()
    {
        for (int i = 0; i < 6; i++)
            inventoryEvent.Add(new UnityEvent());
    }
    
    private void SetButtonUpgradeStatList()
    {
        for (int i = 0; i < 8; i++)
            upgradeStatEvent.Add(new UnityEvent());
    }

    private void SwitchConfig(CONFIG config)
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(config.ToString());
        playerInput.currentActionMap.Enable();
    }

    private void ButtonAct(int index)
    {
        if (buttonList[index].canUpdate)
            GetEvent(index).Invoke();
        buttonList[index].canUpdate = false;
    }

    private void ButtonSleep(int index)
    {
        buttonList[index].canUpdate = true;
    }

    private void SetAllButton()
    {
        SetButton("UseUsable");
        SetButton("OpenInventory");
        SetButton("OpenMap");
        SetButton("Inventory1");
        for (int i = 0; i < 6; i++)
        {
            int index = i;
            SetButton("Inventory" + index);
        }
        for (int i = 0; i < 8; i++)
        {
            int index = i;
            SetButton("UpgradeStat" + index);
        }
    }

    private void SetButton(string name)
    {
        Button button = new Button();
        button.name = name;
        button.canUpdate = true;
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
        else if(index < 6+3)
            return inventoryEvent[index - 3];
        else
            return upgradeStatEvent[index - 3 -6];
    }

    //2222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222

    private void EnableActionMap(InputActionMap action)
    {
        action["Move"].performed += MoveAct;
        action["Move"].canceled += MoveSleep;
        action["Interact"].performed += InteractAct;
        action["Interact"].canceled += InteractSleep;
        action["ScrollMouse"].performed += ScrollMouseAct;
        action["ScrollMouse"].canceled += ScrollMousSleep;
        action["MousePosition"].performed += MousePositionAct;
        action["MousePosition"].canceled += MousePositionSleep;


        //All buttons
        action["UseUsable"].started += test => InventoryAct(0);
        action["UseUsable"].canceled += test => InventorySleep(0);
        action["OpenInventory"].started += test => InventoryAct(1);
        action["OpenInventory"].canceled += test => InventorySleep(1);
        action["OpenMap"].started += test => InventoryAct(2);
        action["OpenMap"].canceled += test => InventorySleep(2);

        for (int i = 0; i < 6; i++)
        {
            int index = i;
            action["Inventory"+(index+1).ToString()].started += test => InventoryAct(index+3);
            action["Inventory"+(index+1).ToString()].canceled += test => InventorySleep(index+3);
        }
        for (int i = 0; i < 8; i++)
        {
            int index = i;
            action["UpgradeStat" + (index + 1).ToString()].started += test => InventoryAct(index+3+6);
            action["UpgradeStat" + (index + 1).ToString()].canceled += test => InventorySleep(index+3+6);
        }
    }

    private void DisableActionMap(InputActionMap action)
    {
        action["Move"].performed -= MoveAct;
        action["Move"].canceled -= MoveSleep;
        action["Interact"].performed -= InteractAct;
        action["Interact"].canceled -= InteractSleep;
        action["ScrollMouse"].performed -= ScrollMouseAct;
        action["ScrollMouse"].canceled -= ScrollMousSleep;
        action["MousePosition"].performed -= MousePositionAct;
        action["MousePosition"].canceled -= MousePositionSleep;


        //All buttons
        action["UseUsable"].started -= test => InventoryAct(0);
        action["UseUsable"].canceled -= test => InventorySleep(0);
        action["OpenInventory"].started -= test => InventoryAct(1);
        action["OpenInventory"].canceled -= test => InventorySleep(1);
        action["OpenMap"].started -= test => InventoryAct(2);
        action["OpenMap"].canceled -= test => InventorySleep(2);

        for (int i = 0; i < _StaticPlayer.nbCasesInventory; i++)
        {
            int index = i;
            action["Inventory" + (index + 1).ToString()].started -= test => InventoryAct(index + 3);
            action["Inventory" + (index + 1).ToString()].canceled -= test => InventorySleep(index + 3);
        }
        for (int i = 0; i < 8; i++)
        {
            int index = i;
            action["UpgradeStat" + (index + 1).ToString()].started -= test => InventoryAct(index + 3 +6);
            action["UpgradeStat" + (index + 1).ToString()].canceled -= test => InventorySleep(index + 3 + 6);
        }

    }

    private void MousePositionAct(InputAction.CallbackContext value)
    {
        mousePos = GameManager.Instance.cam.ScreenToWorldPoint(value.ReadValue<Vector2>());
        mouseDirection = (mousePos - (Vector2)transform.position).normalized;
    }
    
    private void MousePositionSleep(InputAction.CallbackContext value)
    {
        mousePos = Vector2.zero;
    }
    
    private void ScrollMouseAct(InputAction.CallbackContext value)
    {
        scrollMouse = value.ReadValue<float>();
    }
    
    private void ScrollMousSleep(InputAction.CallbackContext value)
    {
        scrollMouse = 0f;
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

    private void InventoryAct(int index)
    {
        ButtonAct(index);
    }
    
    private void InventorySleep(int index)
    {
        ButtonSleep(index);
    }
}
