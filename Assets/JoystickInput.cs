using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    private Joystick _joystick;

    private void Awake()
    {
        _joystick = GetComponent<Joystick>();
    }

    private void Update()
    {
        VirtualInputManager.Instance.XAxis = _joystick.Horizontal;
        VirtualInputManager.Instance.YAxis = _joystick.Vertical;
    }
}
