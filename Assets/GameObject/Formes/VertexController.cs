using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{
    private event Action<Collision2D> _onAnyCollision;

    public event Action<Collision2D> OnAnyCollision
    {
        add => _onAnyCollision += value;
        remove => _onAnyCollision -= value;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision détectée sur " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Cercle"))
        {
            Debug.Log("Tag Cercle OK, event envoyé");
            _onAnyCollision?.Invoke(collision);
        }
    }
}
