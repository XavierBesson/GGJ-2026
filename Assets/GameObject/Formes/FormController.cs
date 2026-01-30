using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormController : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;

    private bool _hasClickedOnPlateform = false;

    [SerializeField] private EColor _actualColor = EColor.VOID;
    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }

    private void Update()
    {
        if (_hasClickedOnPlateform)
        {
            FollowMouse();
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = InputManager.Instance.MousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = worldPosition;
    }

    private void OnMouseDown()
    {
        _hasClickedOnPlateform = true;  
    }
}


