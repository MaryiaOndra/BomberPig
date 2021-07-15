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
    int bombPower = 2;

    private Vector2 _upAlign = new Vector2(Vector2.up.x + GridCreator._xAlign, Vector2.up.y);
    private Vector2 _downAlign = new Vector2(Vector2.down.x - GridCreator._xAlign, Vector2.down.y);   

    private void Awake()
    {
        transform.position = AlignToGrid();
        StartCoroutine(SetBombTimer(_timeToEplode));
    }

    private Vector2 AlignToGrid() 
    {
        Vector2 position = Vector2.zero;
        LayerMask gridMask = LayerMask.GetMask("Grid");
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.localPosition, Vector2.right, MIN_DIST, gridMask);

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

        StartCoroutine(CreateExplosion(Vector2.zero, GridCreator._ySize));
        StartCoroutine(CreateExplosion(_upAlign, GridCreator._ySize));
        StartCoroutine(CreateExplosion(_downAlign, GridCreator._ySize));
        StartCoroutine(CreateExplosion(Vector2.right, GridCreator._xSize));
        StartCoroutine(CreateExplosion(Vector2.left, GridCreator._xSize));
    }

    private IEnumerator CreateExplosion(Vector2 direction, float distance)
    {
        LayerMask gridMask = LayerMask.GetMask("Obstacles");

        for (int i = 1; i < bombPower; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, direction, i * distance, gridMask);
            GameObject explosion;

            if (!hit.collider)
            {
                explosion = Instantiate(_explosionPrefab, (Vector2)transform.localPosition + (i * direction), _explosionPrefab.transform.rotation);
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