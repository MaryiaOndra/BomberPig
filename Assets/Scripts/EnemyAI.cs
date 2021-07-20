using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Walking, Angry, Dirty}

    //private const float MIN_DIST = 0.1f;
    private const float CATCH_DIST = 0.5f;
    private readonly int X = Animator.StringToHash("x");
    private readonly int Y = Animator.StringToHash("y");

    [SerializeField]
    private float _targetRange = 5f;
    [SerializeField]
    private float _obstacleRange = 2f;
    [SerializeField]
    private float _speed = 15f;   
    [SerializeField]
    private float _angryAcceleration = 1.2f;

    private Vector2 _walkPosition;
    private State _currentState;
    private Vector2 _direction = Vector2.up;
    private Animator _animator;
    private float _timeRemaining = 3f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentState = State.Walking;
        _direction = GetRandomDirection(_direction);
    }

    private void Update()
    {
        switch (_currentState) 
        {
            case State.Walking:
                _animator.SetLayerWeight(1, 0);
                Walking();
                CheckAngryDistance();
                break;

            case State.Angry:
                _animator.SetLayerWeight(1, 1);
                Walking();
                CheckAngryDistance();
                break;

            case State.Dirty:
                _animator.SetLayerWeight(2, 1);

                if (_timeRemaining > 0)
                    _timeRemaining -= Time.deltaTime;
                else 
                    Walking();
                break;
        }

        CheckDistanceToCatch();

        _animator.SetFloat(X, _direction.x);
        _animator.SetFloat(Y, _direction.y);
    }

    private void Walking() 
    {
        LayerMask obstaclesMask = LayerMask.GetMask("Obstacles");
        RaycastHit2D obstaclePoint = Physics2D.Raycast(transform.position, _direction, _obstacleRange, obstaclesMask);

        if ( !obstaclePoint && CheckWalkPosition())
        {
            _walkPosition = CheckWalkPosition().collider.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, _walkPosition, _speed * Time.deltaTime);
        }
        else
        {
            _direction = GetRandomDirection(_direction);
        }
    }

    private RaycastHit2D CheckWalkPosition() 
    {
        Vector2 walkPos = (Vector2)transform.position + _direction;

        LayerMask gridMask = LayerMask.GetMask("Grid");
        RaycastHit2D[] gridHits = Physics2D.LinecastAll(transform.position, walkPos, gridMask);
        RaycastHit2D gridHit = gridHits[gridHits.Length - 1];
        return gridHit;
    }

    private Vector2 GetRandomDirection(Vector2 oldDirection) 
    {
        Vector2[] directions = new[] { LevelCreator.UpAlign, LevelCreator.DownAlign, Vector2.right, Vector2.left};
        int randIndex = UnityEngine.Random.Range(0, directions.Length);

        while (directions[randIndex] == oldDirection)
        {
            randIndex = UnityEngine.Random.Range(0, directions.Length);
        }

        var newDirection = directions[randIndex];

        return newDirection;    
    }

    private void CheckAngryDistance() 
    {
        LayerMask playerMask = LayerMask.GetMask("Player");

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, _direction, _targetRange, playerMask);

        if (hitPlayer)
        {
            _currentState = State.Angry;
            transform.position = Vector2.MoveTowards(transform.position, hitPlayer.collider.transform.position, _speed * _angryAcceleration * Time.deltaTime);
        }

        else
            _currentState = State.Walking;
    }

    private void CheckDistanceToCatch() 
    {
        if (Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position) < CATCH_DIST)
        {
            LevelManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_currentState != State.Dirty)
        {
            if (collision.GetComponent<Explosion>())
            {
                _currentState = State.Dirty;
                LevelManager.Instance.CheckLevelComplete();
            }
        }
    }
}
