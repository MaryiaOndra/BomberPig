using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PersonInfo
{
    [SerializeField]
    private GameObject _prefab;
    [SerializeField]
    private Vector2Int _coordinates;

    public GameObject Prefab => _prefab;
    public Vector2Int Coordinates => _coordinates;
}
