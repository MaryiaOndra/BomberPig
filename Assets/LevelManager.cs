using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GridCreator _gridCreator;

    [SerializeField]
    private List<GameObject> _enemyPrefabs;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _gridCreator.BuilGrid();
      //  CreateEnemy();

    }

    private void CreateEnemy() 
    {
        foreach (var enemy in _enemyPrefabs) 
        {
            var enemyObject = Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }
}
