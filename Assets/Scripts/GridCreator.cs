using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointPrefab;

    [SerializeField]
    private Vector2 _gridSize;
    [SerializeField]
    GameObject stonePrefab;

    private Vector3 _startPointPos;



    private void Awake()
    {
        _startPointPos = transform.position;
        //_grid = new Vector3[_columns, _rows];

        for (int i = 0; i < _gridSize.y; i++)
        {
            _startPointPos.x = transform.position.x + (1f / 9f) * i;

            for (int j = 0; j < _gridSize.x; j++)
            {
                var point = GameObject.Instantiate(_pointPrefab, transform);
                point.transform.position = _startPointPos;
                _startPointPos.x += 1.1f;
            }

            _startPointPos.y += 1; 
        }
    }
}
