using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;
    [SerializeField]
    private GameObject _bombSprite;

    [SerializeField]
    private float _timeToEplode;


    private SpriteRenderer _sprite;

    private void Awake()
    {
        StartCoroutine(SetTimer(_timeToEplode));
    }

    public IEnumerator SetTimer(float time) 
    {
        yield return new WaitForSeconds(time);
        _bombSprite.SetActive(false);
        _explosion.SetActive(true);
        DestroyBomb();
    }

    public void DestroyBomb() 
    {
        Destroy(gameObject, 1f);
    }
}
