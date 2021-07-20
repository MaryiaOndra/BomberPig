using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Obsolete]
public class Player : MonoBehaviour
{
    private readonly int HORIZ_INT = Animator.StringToHash("Horizontal");
    private readonly int VERT_INT = Animator.StringToHash("Vertical");

    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _bombPrefab;

    private Rigidbody2D _rB;
    private Vector2 _movement;
    private Vector2 _direction;
    private Animator _animator;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }

        _rB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        VirtualInputManager.OnDropBomb = ThrowBomb;
    }

    void Update()
    {
#if UNITY_STANDALONE
        _movement.y = Input.GetAxisRaw("Vertical");
       _movement.x = Input.GetAxisRaw("Horizontal");      
        if (Input.GetKeyDown(KeyCode.Space))
            ThrowBomb(transform.position);

#elif UNITY_ANDROID
        _movement.y = VirtualInputManager.Instance.YAxis * LevelCreator._xSize;
        _movement.x = VirtualInputManager.Instance.XAxis;

#endif
        _animator.SetFloat(HORIZ_INT, _movement.x);
        _animator.SetFloat(VERT_INT, _movement.y);
    }

    private void FixedUpdate()
    {
         _rB.MovePosition(_rB.position + _movement * _speed * Time.fixedDeltaTime);
    }


    private void ThrowBomb() 
    {
        Instantiate(_bombPrefab, transform.position , Quaternion.identity);
    }  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Explosion>())
        {
            LevelManager.Instance.GameOver();
        }
    }
}
