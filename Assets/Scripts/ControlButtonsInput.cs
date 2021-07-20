using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonsInput : MonoBehaviour
{
    public void ReleaseButton() 
    {
        VirtualInputManager.Instance.YAxis = 0f;
        VirtualInputManager.Instance.XAxis = 0f;
    }

    public void VerticalMove(int value) 
    {
        VirtualInputManager.Instance.YAxis = value;
    }

    public void HorizontalMove(int value) 
    {
        VirtualInputManager.Instance.XAxis = value;
    }
}
