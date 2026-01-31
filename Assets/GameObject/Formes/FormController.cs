using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormController : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private VertexController[] _vertexController;

    private bool _hasClickedOnPlateform = false;
    private bool _isOnPlateform = false;
    private int _check = 0;
    private bool _hasSnapped; 

    [SerializeField] private EColor _actualColor = EColor.VOID;
    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }


    #region Build In Methods
    private void Update()
    {
        GetInfo();
    }

    private void OnEnable()
    {

        if (_vertexController == null) return;

        for (int i = 0; i < _vertexController.Length; i++)
        {
            /*Debug.Log(gameObject.name + " écoute " + _vertexController[i].name);*/    
            _vertexController[i].OnAnyCollision += HandleVertexCollisionEnter;
        }

    }

    private void OnDisable()
    {

        if (_vertexController == null) return;

        for (int i = 0; i < _vertexController.Length; i++)
        {
            _vertexController[i].OnAnyCollision -= HandleVertexCollisionEnter;
        }
    }

    #endregion

    #region Drag
    private void GetInfo()
    {
        InputManager input = InputManager.Instance; 

        if (input.WasDragButtonPressed && _isOnPlateform)
        {
            _hasClickedOnPlateform = true;
        }

        if (!_hasClickedOnPlateform) return;
        else if (_hasClickedOnPlateform) { FollowMouse(); }
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

    #endregion

    #region Snap
    private void HandleVertexCollisionEnter(Collider2D collision)
    {
        if (_vertexController == null || _hasSnapped) return;

        _check = 0;

        for (int i = 0; i < _vertexController.Length; i++)
        {
            if (_vertexController[i].IsVertexOnSnapPoint)
            {
                _check++;
            }
        }

        Debug.Log("Vertex validés " + _check + " / " + _vertexController.Length);

        if (_check == _vertexController.Length)
        {
            SnapGeometrics();
            Debug.Log("CanBeSnapped");
        }
    }

    private void SnapGeometrics()
    {

    }

    #endregion

    private void OnMouseDown()
    {
        _hasClickedOnPlateform = true;  
    }

    private void OnMouseOver()
    {
        _isOnPlateform = true; 
    }

}


