using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly int HORIZ_INT = Animator.StringToHash("Horizontal");
    private readonly int VERT_INT = Animator.StringToHash("Vertical");

    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _bombPrefab;

    private Rigidbody2D _rB;
    private Vector2 _movement;
    private Animator _animator;

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        _rB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
       _movement.y = Input.GetAxisRaw("Vertical");
       _movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            ThrowBomb(transform.position);


        _animator.SetFloat(HORIZ_INT, _movement.x);
        _animator.SetFloat(VERT_INT, _movement.y);
    }

    private void FixedUpdate()
    {
        _rB.MovePosition(_rB.position + _movement * _speed * Time.fixedDeltaTime);
    }


    private void ThrowBomb(Vector2 position) 
    {
        Instantiate(_bombPrefab, transform.position , Quaternion.identity);
    }  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bomb>())
        {
            Debug.Log("LOST  HEALTH");
        }
    }
}
