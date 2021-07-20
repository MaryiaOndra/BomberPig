using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly int HORIZ_INT = Animator.StringToHash("Horizontal");
    private readonly int VERT_INT = Animator.StringToHash("Vertical");

    [SerializeField]
    private float _timeTOMove = 0.2f;

    private bool _isMoving;
    private Vector2 _origPos, _targetPos;
    private Animator _animator;
    Vector2 _moveDirection;
    public static PlayerMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _animator = GetComponent<Animator>();
        VirtualInputManager.OnDropBomb = ThrowBomb;
        
    }

    private void Update()
    {
        _moveDirection = new Vector2(VirtualInputManager.Instance.XAxis, VirtualInputManager.Instance.YAxis);

        //if (_moveDirection.magnitude > 0)
        //{
        //    CheckDirectionObstacles(_moveDirection);
        //}

        if (Input.GetKey(KeyCode.UpArrow) && !_isMoving)
        {
            CheckDirectionObstacles(LevelCreator.UpAlign);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !_isMoving)
        {
            CheckDirectionObstacles(LevelCreator.DownAlign);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !_isMoving)
        {
            CheckDirectionObstacles(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !_isMoving)
        {
            CheckDirectionObstacles(Vector2.right);
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
            _animator.SetFloat(HORIZ_INT, direction.x);
            _animator.SetFloat(VERT_INT, direction.y);
        }
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

    private void ThrowBomb()
    {
        GameObject bomb = LevelManager.Instance.LevelInfo.BobmPrefab;
        
        Instantiate(bomb, transform.position, Quaternion.identity);
    }
}
