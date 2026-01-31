using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{
    private event Action<Collider2D> _onAnyCollision;

    public event Action<Collider2D> OnAnyCollision
    {
        add => _onAnyCollision += value;
        remove => _onAnyCollision -= value;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision détectée sur " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Cercle"))
        {
            Debug.Log("Tag Cercle OK, event envoyé");
            _onAnyCollision?.Invoke(collision);
        }
    }
}
