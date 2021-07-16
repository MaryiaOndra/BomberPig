using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Walking, Angry, Dirty}

    private const float MIN_DIST = 0.1f;
    private readonly int X = Animator.StringToHash("x");
    private readonly int Y = Animator.StringToHash("y");

    [SerializeField]
    private float _targetRange = 5f;
    [SerializeField]
    private float _obstacleRange = 2f;
    [SerializeField]
    private float _speed = 15f;
    [SerializeField]
    private List<GameObject> _waypoints;

    private int _randomSpot = 1;
    private Vector2 _walkPosition;
    private State _currentState;
    private Animator _animator;
    private Vector2 _direction = Vector2.zero;
    float timeRemaining = 3f;

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
                CheckAngryDistance();
                break;

            case State.Dirty:
                _animator.SetLayerWeight(2, 1);

                if (timeRemaining > 0)
                    timeRemaining -= Time.deltaTime;
                else 
                    Walking();

                CheckDistanceToCatch();
                break;
        }

    }

    private void Walking() 
    {
        _animator.SetFloat(X, _direction.x);
        _animator.SetFloat(Y, _direction.y);
        _walkPosition = (Vector2)transform.position + _direction;
        transform.position = Vector2.MoveTowards(transform.position, _walkPosition, _speed * Time.deltaTime);

        LayerMask gridMask = LayerMask.GetMask("Obstacles");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _direction, _obstacleRange, gridMask);

        if (raycastHit2D)
        {
            _direction = GetRandomDirection(_direction);
        }
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
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _direction, _targetRange, mask);

        if (raycastHit2D)
        {
            _currentState = State.Angry;
            transform.position = Vector2.MoveTowards(transform.position, raycastHit2D.collider.transform.position, _speed * 1.2f * Time.deltaTime);

            CheckDistanceToCatch();
        }
        else
            _currentState = State.Walking;
    }

    private void CheckDistanceToCatch() 
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < MIN_DIST * 10f)
        {
            LevelManager.Instance.RestartPanel();
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
