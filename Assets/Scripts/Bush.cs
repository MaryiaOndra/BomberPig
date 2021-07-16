using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private void DestroyBush()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.GetComponent<Explosion>())
        {
            DestroyBush();
        }        
    }
}
