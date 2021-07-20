using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private bool _isMoving;
    private Vector2 _origPos, _targetPos;
    private float _timeTOMove = 0.2f;


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !_isMoving)
        {
            CheckDirectionObstacles(LevelCreator.UpAlign);
             //StartCoroutine(MovePlayer(LevelCreator.UpAlign));
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !_isMoving)
        {
            CheckDirectionObstacles(LevelCreator.DownAlign);
            //StartCoroutine(MovePlayer(LevelCreator.DownAlign));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isMoving)
        {
            CheckDirectionObstacles(Vector2.left);
            // StartCoroutine(MovePlayer(Vector2.left));
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !_isMoving) 
        {
            CheckDirectionObstacles(Vector2.right);
            //StartCoroutine(MovePlayer(Vector2.right));
        }
    }

    private void CheckDirectionObstacles(Vector2 direction)
    {
        _origPos = transform.position;
        _targetPos = _origPos + direction;

        LayerMask obstaclesMask = LayerMask.GetMask("Obstacles"); 
        LayerMask gridMask = LayerMask.GetMask("Grid");
        RaycastHit2D obstaclePoint = Physics2D.Linecast(transform.position, _targetPos, obstaclesMask);
        RaycastHit2D[] gridHits = Physics2D.LinecastAll(transform.position, _targetPos, gridMask);
        RaycastHit2D gridHit = gridHits[gridHits.Length - 1];

        if (!obstaclePoint && gridHit)
        {
            StartCoroutine(MovePlayer(gridHit.transform.position));
        }
        else {Debug.Log("Is obstacle"); }
    }

    private IEnumerator MovePlayer(Vector2 targetPos) 
    {
        _isMoving = true;
        float elapsedTime = 0;
        _origPos = transform.position;

        while (elapsedTime < _timeTOMove) 
        {
            transform.position = Vector2.Lerp(_origPos, targetPos, (elapsedTime / _timeTOMove));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPos;

        _isMoving = false;
    }
}
