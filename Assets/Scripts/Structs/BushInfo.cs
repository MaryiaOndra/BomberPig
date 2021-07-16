using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BushInfo
{
    [SerializeField]
    private GameObject _bushPrefab;
    [SerializeField]
    private List<Vector2Int> _coordinates;

    public GameObject BushPrefab => _bushPrefab;
    public List<Vector2Int> Coordinates => _coordinates;
}
