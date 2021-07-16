using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GridCreator _gridCreator;

    [SerializeField]
    private List<GameObject> _enemyPrefabs;

    [SerializeField]
    private GameObject _gameOverPanel;

    private int _dirtyEnemyCount;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _dirtyEnemyCount = FindObjectsOfType<EnemyAI>(true).Length;
    }

    private void Start()
    {
        Time.timeScale = 1;
        _gridCreator.BuilGrid();
    }

    public void RestartPanel() 
    {
        Time.timeScale = 0;
        _gameOverPanel.SetActive(true);
    }

    public void Restart() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void CheckLevelComplete() 
    {
        _dirtyEnemyCount--;

        if (_dirtyEnemyCount == 0)
        {
            RestartPanel();
        }
    }
}