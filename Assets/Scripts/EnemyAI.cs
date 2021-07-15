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
    private float _speed = 15f;
    [SerializeField]
    private List<GameObject> _waypoints;

    private int _randomSpot = 1;
    private Vector2 _startPosition;
    private Vector2 _walkPosition;
    private State _currentState;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Vector2 _direction;
    float timeRemaining = 3f;

    private Vector2 _upAlign = new Vector2(Vector2.up.x + GridCreator._xAlign, Vector2.up.y);
    private Vector2 _downAlign = new Vector2(Vector2.down.x - GridCreator._xAlign, Vector2.down.y);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = _waypoints[0].transform.position;
        _animator = GetComponent<Animator>();
        _currentState = State.Walking;
    }

    private void Update()
    {
        switch (_currentState) 
        {
            case State.Walking:
                _animator.SetLayerWeight(1, 0);
                Walking();
                CheckDistance();
                break;

            case State.Angry:
                _animator.SetLayerWeight(1, 1);
                CheckDistance();
                break;

            case State.Dirty:
                _animator.SetLayerWeight(2, 1);

                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else 
                {
                    Walking();
                }
                break;
        }

    }

    private void Walking() 
    {
        _walkPosition = _waypoints[_randomSpot].transform.position;
        _direction = (_walkPosition - (Vector2)transform.localPosition).normalized;
        _animator.SetFloat(X, _direction.x);
        _animator.SetFloat(Y, _direction.y);

        transform.position = Vector2.MoveTowards(transform.position, _walkPosition, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _waypoints[_randomSpot].transform.position) < MIN_DIST)
        {
            if (_randomSpot >= _waypoints.Count - 1)
                _randomSpot = 0;
            else
                _randomSpot++;
        }

        _startPosition = _walkPosition;
    }

    private void CheckDistance() 
    {
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _direction, _targetRange, mask);

        if (raycastHit2D)
        {
            _currentState = State.Angry;
            transform.position = Vector2.MoveTowards(transform.position, raycastHit2D.collider.transform.position, _speed * 1.2f * Time.deltaTime);
        }
        else
            _currentState = State.Walking;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>())
        {
            _currentState = State.Dirty;
        }
    }   
}
