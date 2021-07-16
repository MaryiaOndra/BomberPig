using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LevelInfo
{
    [SerializeField]
    private LevelOrder _level;
    [SerializeField]
    private Vector2 _gridSize;
    [SerializeField]
    private GameObject _pointPrefab;
    [SerializeField]
    private GameObject _stonePrefab;
    [Header("Info")]
    [SerializeField]
    private List<PersonInfo> _charactersInfo;
    [SerializeField]
    private BushInfo _bushesInfo;

    public Vector2 GridSize => _gridSize;
    public LevelOrder Level => _level;
    public GameObject PointPrefab => _pointPrefab;
    public GameObject StonePrefab => _stonePrefab;
    public List<PersonInfo> CharactersInfo => _charactersInfo;
    public BushInfo BushesInfo => _bushesInfo;
}
 public enum LevelOrder{ Level01, Level02, Level03  }
