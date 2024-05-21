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
    }

    // Update is called once per frame
    void Update()
    {
        print("scroll : "+inputSystem.scrollMouse);
        print("mousePos : "+  inputSystem.mousePos);
    }
}
