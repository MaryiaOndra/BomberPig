using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static readonly float _xSize = 1.1f;
    public static readonly float _ySize = 1f;
    public static readonly float _xAlign = 1f / 9f;

    [SerializeField]
    private GameObject _pointPrefab;

    [SerializeField]
    private Vector2 _gridSize;

    private Vector3 _startPointPos;
    private List<GameObject> _gridPoints = new List<GameObject>();

    public List<GameObject> GridPoints => _gridPoints;

    public void BuilGrid()
    {
        _startPointPos = transform.position;
        //_xAlign = 1f / (float)_gridSize.y;

        for (int i = 0; i < _gridSize.y; i++)
        {
            _startPointPos.x = transform.position.x + _xAlign * i;

            for (int j = 0; j < _gridSize.x; j++)
            {
                var point = GameObject.Instantiate(_pointPrefab, transform);
                point.transform.position = _startPointPos;
                _startPointPos.x += _xSize;

                _gridPoints.Add(point);
            }

            _startPointPos.y += _ySize; 
        }

        RemoveStones();
    }

    void RemoveStones() 
    {
        LayerMask gridMask = LayerMask.GetMask("Obstacles");

        for (int i = 0; i < _gridPoints.Count; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(_gridPoints[i].transform.position, Vector2.up, 0.1f, gridMask);

            if (hit.collider)
            {
                Destroy(_gridPoints[i]);
                _gridPoints.Remove(_gridPoints[i]);
            }
        }
    }
}
