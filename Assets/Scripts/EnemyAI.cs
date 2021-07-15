using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State { Roaming, ChaseTarget}
    
    private const float MIN_DIST = 0.1f;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private List<Transform> _waypoints;
    [SerializeField]
    private float _targetRange = 5f;

    private Vector2 startPosition;
    private int _randomSpot = 1;
    private Vector2 _roamPosition;
    private State _currentState;

    public static Action Hitted;

    private void Awake()
    {
        Hitted = GetHit;
        transform.position = _waypoints[0].position;
        _currentState = State.Roaming;
    }


    private void Update()
    {
        switch (_currentState) 
        {
            case State.Roaming:
                _roamPosition = _waypoints[_randomSpot].position;

                transform.position = Vector2.MoveTowards(transform.position, _roamPosition, _speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, _waypoints[_randomSpot].transform.position) < MIN_DIST)
                {
                    //Go to the next point

                    if (_randomSpot >= _waypoints.Count - 1)
                    {
                        _randomSpot = 0;
                    }
                    else
                        _randomSpot++;
                }

                CheckDistance();

                break;
            case State.ChaseTarget:
                transform.position = Vector2.MoveTowards(transform.position, PlayerController.Instance.transform.position, _speed * Time.deltaTime);
                CheckDistance();

                break;
        
        }


    }


    private void CheckDistance() 
    {
        float distance = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distance < _targetRange)
        {
            _currentState = State.ChaseTarget;
        }
        else if (distance > _targetRange)
        {
            _currentState = State.Roaming;
        }
    }

    void GetHit() 
    {
        Debug.Log(gameObject.name +  "  get hitted!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>())
        {
            Debug.Log(gameObject.name + "  get hitted!");
        }
    }
}
