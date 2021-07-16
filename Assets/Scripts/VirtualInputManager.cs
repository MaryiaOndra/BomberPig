using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualInputManager : MonoBehaviour
{
    [SerializeField]
    private Button _bombBtn;

    public float YAxis { get; set; }
    public float XAxis { get; set; }

    public static VirtualInputManager Instance { get; private set; }
    public static Action OnDropBomb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _bombBtn.onClick.AddListener(DropBomb);
    }

    private void OnDisable()
    {
        _bombBtn.onClick.RemoveListener(DropBomb);
    }

    public void DropBomb() 
    {
        OnDropBomb.Invoke();
    }
}
