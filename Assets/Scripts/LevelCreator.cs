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
        Vector2 gridSize = LevelManager.Instance.LevelInfo.GridSize;
        GameObject pointPrefab = LevelManager.Instance.LevelInfo.PointPrefab;

        for (int i = 0; i < gridSize.y; i++)
        {
            _startPointPos.x = transform.position.x + _xAlign * i;

            for (int j = 0; j < gridSize.x; j++)
            {
                var point = Instantiate(pointPrefab, transform);
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
        GameObject stonePrefab = LevelManager.Instance.LevelInfo.StonePrefab;

        for (int i = 0; i < _gridPoints.Count; i++)
        {
            if ((_gridPoints[i].Y % 2) != 0)
            {
                if ((_gridPoints[i].X % 2) != 0)
                {
                    var stone = Instantiate(stonePrefab, transform);
                    stone.transform.position = _gridPoints[i].transform.position;
                    _gridPoints.Remove(_gridPoints[i]);
                }
            }
        }
    }

    private void SetBushes() 
    {
        GameObject bushPrefab = LevelManager.Instance.LevelInfo.BushesInfo.BushPrefab;
        List<Vector2Int> bushesCoordinates = LevelManager.Instance.LevelInfo.BushesInfo.Coordinates;

        for (int i = 0; i < bushesCoordinates.Count; i++)
        {
            var _serchPoint = bushesCoordinates[i];
            GridPoint bushPoint = _gridPoints.Find(_p => _p.X == _serchPoint.x && _p.Y == _serchPoint.y);
            if (bushPoint != null)
            {
                var bush = Instantiate(bushPrefab, transform);
                bush.transform.position = bushPoint.transform.position;
            }
        }
    }

    private void SetCharacters() 
    {
        List<PersonInfo> characters = LevelManager.Instance.LevelInfo.CharactersInfo;

        for (int i = 0; i < characters.Count; i++)
        {
            Vector2Int coordinates = characters[i].Coordinates;

            GridPoint characterPoint = _gridPoints.Find(_p => _p.X == coordinates.x && _p.Y == coordinates.y);
            var character = Instantiate(characters[i].Prefab, transform);
            character.transform.position = characterPoint.transform.position;
        }
    }
}
