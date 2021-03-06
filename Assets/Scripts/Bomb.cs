using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private const float MIN_DIST = 0.1f;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _bombSprite;
    [SerializeField]
    private float _timeToEplode;
    [SerializeField]
    private int _bombPower;

    public static int BombPower;

    private void Awake()
    {
        if(BombPower == 0)
            BombPower = _bombPower;

        transform.position = GetGridPointPosition(transform.localPosition);
        StartCoroutine(SetBombTimer(_timeToEplode));
    }

    public static Vector2 GetGridPointPosition(Vector2 startPosition) 
    {
        Vector2 position = Vector2.zero;
        LayerMask gridMask = LayerMask.GetMask("Grid");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(startPosition, Vector2.right, MIN_DIST, gridMask);

        if (raycastHit2D) 
        {
            position = raycastHit2D.transform.position;
        }

        return position;
    }

    public IEnumerator SetBombTimer(float time) 
    {
        yield return new WaitForSeconds(time);
        Explode();
        DestroyBomb(1f);
    }

    private void Explode() 
    {
        _bombSprite.SetActive(false);

        StartCoroutine(CreateExplosion(Vector2.zero, LevelCreator._ySize));
        StartCoroutine(CreateExplosion(LevelCreator.UpAlign, LevelCreator._ySize));
        StartCoroutine(CreateExplosion(LevelCreator.DownAlign, LevelCreator._ySize));
        StartCoroutine(CreateExplosion(Vector2.right, LevelCreator._xSize));
        StartCoroutine(CreateExplosion(Vector2.left, LevelCreator._xSize));
    }

    private IEnumerator CreateExplosion(Vector2 direction, float distance)
    {
        LayerMask gridMask = LayerMask.GetMask("Obstacles");

        for (int i = 1; i < _bombPower; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, i * distance, gridMask);

            if (!hit.collider || hit.collider.GetComponent<Bush>())
            {
                var explosion = Instantiate(_explosionPrefab, (Vector2)transform.localPosition + (i * direction), _explosionPrefab.transform.rotation);
                explosion.transform.SetParent(gameObject.transform);
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DestroyBomb(float secToWait) 
    {
        Destroy(gameObject, secToWait);
    }
}
