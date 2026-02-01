using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionColor : MonoBehaviour
{
    bool _isOpen = false;
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void SetBoolOptionColor()
    {
        _isOpen =! _isOpen;
        _animator.SetBool("IsOpen", _isOpen);
    }
}
