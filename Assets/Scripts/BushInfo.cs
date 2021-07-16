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


    //public static List<Vector2Int> _Coordinates = new List<Vector2Int>()
    //{
    //    new Vector2Int(0, 2),
    //    new Vector2Int(2, 6),
    //    new Vector2Int(4, 0),
    //    new Vector2Int(4, 4),
    //    new Vector2Int(5, 4),
    //    new Vector2Int(6, 8),
    //    new Vector2Int(8, 8),
    //    new Vector2Int(8, 1),
    //    new Vector2Int(8, 2),
    //    new Vector2Int(8, 3),
    //    new Vector2Int(10, 2),
    //    new Vector2Int(10, 4),
    //    new Vector2Int(11, 0),
    //    new Vector2Int(11, 6),
    //    new Vector2Int(13, 0),
    //    new Vector2Int(12, 0),
    //    new Vector2Int(12, 6),
    //    new Vector2Int(14, 2),
    //    new Vector2Int(14, 8),
    //    new Vector2Int(16, 6),
    //};
}
