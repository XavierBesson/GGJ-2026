using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexController : MonoBehaviour
{

    private bool _isVertexOnSnapPoint = false;

    private event Action<Collider2D> _onAnyCollision;

    #region Properties
    public bool IsVertexOnSnapPoint { get => _isVertexOnSnapPoint; set => _isVertexOnSnapPoint = value; }

    #endregion

    #region Event

    public event Action<Collider2D> OnAnyCollision
    {
        add => _onAnyCollision += value;
        remove => _onAnyCollision -= value;
    }

    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision détectée sur " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Cercle"))
        {
            Debug.Log("Tag Cercle OK, event envoyé");
            IsVertexOnSnapPoint = true;
            _onAnyCollision?.Invoke(collision);
        }
    }

}
