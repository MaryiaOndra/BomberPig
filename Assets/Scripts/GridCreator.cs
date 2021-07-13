using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointPrefab;

    [SerializeField]
    private int _rows;
    [SerializeField]
    private int _columns;
    [SerializeField]
    private Vector3 _gap;

    private Vector3[,] _grid;
    private Vector3 _startPointPos;

    private void Awake()
    {
        _startPointPos = transform.position;
        //_grid = new Vector3[_columns, _rows];

        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                Vector3 pos = _startPointPos + _gap;
                //_grid[i, j] = pos;
                var point = GameObject.Instantiate(_pointPrefab, transform);
                point.transform.position = pos;
                Debug.Log(pos);
                _startPointPos = pos;
            }

            _gap.y = 0;
        }
    }
}
