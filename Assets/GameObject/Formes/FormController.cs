using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormController : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;

    private bool _hasClickedOnPlateform = false;
    private bool _isOnPlateform = false;

    [SerializeField] private EColor _actualColor = EColor.VOID;
    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }

    private void Update()
    {
        GetInfo();
    }

    private void GetInfo()
    {
        InputManager input = InputManager.Instance; 

        if (input.WasDragButtonPressed && _isOnPlateform)
        {
            _hasClickedOnPlateform = true;
        }

        if (_hasClickedOnPlateform)
        {
            FollowMouse();
        }
    }

    private void FollowMouse()
    {
        Vector3 mousePosition = InputManager.Instance.MousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);

        float speed = 10f;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, worldPosition, 1f - Mathf.Exp(-speed * Time.deltaTime));
    }

    private void OnMouseDown()
    {
        _hasClickedOnPlateform = true;  
    }

    private void OnMouseOver()
    {
        _isOnPlateform = true; 
    }
}


