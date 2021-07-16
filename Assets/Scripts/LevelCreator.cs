using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public static readonly float _xSize = 1.1f;
    public static readonly float _ySize = 1f;
    public static readonly float _xAlign = 1f / 9f;
    public static Vector2 UpAlign = new Vector2(Vector2.up.x + LevelCreator._xAlign, Vector2.up.y);
    public static Vector2 DownAlign = new Vector2(Vector2.down.x - LevelCreator._xAlign, Vector2.down.y);

    [SerializeField]
    private LevelInfo _levelInfo;

    private Vector3 _startPointPos;
    private List<GridPoint> _gridPoints = new List<GridPoint>();

    public void CreateLevel()
    {
        SetGrid();
        SetStones();
        SetBushes();
        SetCharacters();
    }

    private void SetGrid() 
    {
        _startPointPos = transform.position;

        for (int i = 0; i < _levelInfo.GridSize.y; i++)
        {
            _startPointPos.x = transform.position.x + _xAlign * i;

            for (int j = 0; j < _levelInfo.GridSize.x; j++)
            {
                var point = GameObject.Instantiate(_levelInfo.PointPrefab, transform);
                point.transform.position = _startPointPos;
                _startPointPos.x += _xSize;

                var gridPoint = point.GetComponent<GridPoint>();
                gridPoint.X = j;
                gridPoint.Y = i;

                _gridPoints.Add(gridPoint);
            }

            _startPointPos.y += _ySize;
        }
    }

    private void SetStones() 
    {
        for (int i = 0; i < _gridPoints.Count; i++)
        {
            if ((_gridPoints[i].Y % 2) != 0)
            {
                if ((_gridPoints[i].X % 2) != 0)
                {
                    var stone = Instantiate(_levelInfo.StonePrefab, transform);
                    stone.transform.position = _gridPoints[i].transform.position;
                    _gridPoints.Remove(_gridPoints[i]);
                }
            }
        }
    }

    private void SetBushes() 
    {
        for (int i = 0; i < _levelInfo.BushesInfo.Coordinates.Count; i++)
        {
            var _serchPoint = _levelInfo.BushesInfo.Coordinates[i];
            GridPoint bushPoint = _gridPoints.Find(_p => _p.X == _serchPoint.x && _p.Y == _serchPoint.y);
            if (bushPoint != null)
            {
                var bush = Instantiate(_levelInfo.BushesInfo.BushPrefab, transform);
                bush.transform.position = bushPoint.transform.position;
            }
        }
    }

    private void SetCharacters() 
    {
        for (int i = 0; i < _levelInfo.CharactersInfo.Count; i++)
        {
            Vector2Int coordinates = _levelInfo.CharactersInfo[i].Coordinates;

            GridPoint characterPoint = _gridPoints.Find(_p => _p.X == coordinates.x && _p.Y == coordinates.y);
            var character = Instantiate(_levelInfo.CharactersInfo[i].Prefab, transform);
            character.transform.position = characterPoint.transform.position;
        }
    }
}
