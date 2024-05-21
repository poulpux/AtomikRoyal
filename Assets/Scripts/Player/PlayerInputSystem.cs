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
    class Button
    {
        public string name;
        public bool canUpdate;
    }

    PlayerInput playerInput;
    [HideInInspector] public InputActionMap DefaultActions;
    List<Button> buttonList  = new List<Button>();
    public UnityEvent isUsingUsableEvent = new UnityEvent(), isOpeningInventoryEvent = new UnityEvent(), isOpeningMapEvent = new UnityEvent();

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        DefaultActions = playerInput.actions.FindActionMap("Default");

        isUsingUsableEvent.AddListener(() => print("marche"));

        EnableActionMap(DefaultActions);
        SetAllButton();
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

    private void SwitchConfig(CONFIG config)
    {
        playerInput.currentActionMap.Disable();
        playerInput.SwitchCurrentActionMap(config.ToString());
        playerInput.currentActionMap.Enable();
    }

    private void EnableActionMap(InputActionMap action)
    {
        action["UseUsable"].started += UseUsableAct;
        action["UseUsable"].canceled += UseUsableSleep;
        //action["Accelerate"].canceled += AccelerateSleep;
        //action["Decelerate"].performed += DecelerateActing;
        //action["Decelerate"].canceled += DecelerateSleep;
        //action["Direction"].performed += GetDirectionActing;
        //action["Direction"].canceled += GetDirectionSleep;
        //action["Drift"].performed += TryDrift;
        //action["Drift"].canceled += DriftSleep;
    }

    private void DisableActionMap(InputActionMap action)
    {
        //action["Accelerate"].performed -= AccelerateActing;
        //action["Accelerate"].canceled -= AccelerateSleep;
        //action["Decelerate"].performed -= DecelerateActing;
        //action["Decelerate"].canceled -= DecelerateSleep;
        //action["Direction"].performed -= GetDirectionActing;
        //action["Direction"].canceled -= GetDirectionSleep;
        //action["Drift"].performed -= TryDrift;
        //action["Drift"].canceled -= DriftSleep;
    }

    private void UseUsableAct(InputAction.CallbackContext value)
    {
        ButtonAct(0);
    }

    private void UseUsableSleep(InputAction.CallbackContext value)
    {
        ButtonSleep(0);
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
            return isUsingUsable;

        return null;
    }
}
