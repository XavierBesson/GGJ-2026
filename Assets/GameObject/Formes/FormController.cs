using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormController : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private VertexController _vertexController;

    private bool _hasClickedOnPlateform = false;
    private bool _isOnPlateform = false;

    [SerializeField] private EColor _actualColor = EColor.VOID;
    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }

    private void Update()
    {
        GetInfo();
    }

    private void OnEnable()
    {
        if (_vertexController == null)
        {
            Debug.LogError(_vertexController.name + " non assigné");
            return;
        }

        Debug.Log(gameObject.name + " écoute " + _vertexController.name);
        _vertexController.OnAnyCollision += HandleVertexCollision;
    }

    private void OnDisable()
    {
        if (_vertexController == null) return;

        _vertexController.OnAnyCollision -= HandleVertexCollision;
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

    private void HandleVertexCollision(Collision2D collision)
    {
        Debug.Log("Collision with " + collision);
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("frighruigqre");
    //}
}


