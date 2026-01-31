using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormController : MonoBehaviour
{

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private VertexController[] _vertexController;

    private bool _hasClickedOnPlateform = false;
    private bool _isOnPlateform = false;
    private int _checkNumberOfVertex = 0;
    private bool _canBeSnapped = false; 
    private bool _hasSnapped;

    [SerializeField] private EColor _actualColor = EColor.VOID;
    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }


    #region Unity Build-In Methods
    private void Update()
    {
        GetInfo();
        SnapGeometrics();

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

    private void OnMouseDown()
    {
        if (_hasSnapped)
        {
            UnsnapGeometrics();
        }

        _hasClickedOnPlateform = true;
    }

    private void OnMouseOver()
    {
        _isOnPlateform = true;
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
        if (_vertexController == null) return;

        _checkNumberOfVertex = 0;

        for (int i = 0; i < _vertexController.Length; i++)
        {
            if (_vertexController[i].IsVertexOnSnapPoint)
            {
                _checkNumberOfVertex++;
            }
        }

        Debug.Log("Vertex validés " + _checkNumberOfVertex + " / " + _vertexController.Length);

        if (_checkNumberOfVertex == _vertexController.Length)
        {
            _canBeSnapped = true;
        }

        else { _canBeSnapped = false; }
    }

    private void SnapGeometrics()
    {
        
        if (_vertexController != null)
        {
            if (!_canBeSnapped && _hasClickedOnPlateform && _hasSnapped) return;

            else if (_canBeSnapped && InputManager.Instance.WasDragButtonPressed)
            {
                Vector3 center = Vector3.zero;

                for (int i = 0; i < _vertexController.Length; i++)
                {
                    if (_vertexController[i].CollisionTransform != null)
                    {
                        center += _vertexController[i].CollisionTransform.position;
                    }
                }

                center /= _vertexController.Length;

                transform.position = center;
                _hasSnapped = true;
                _hasClickedOnPlateform = false;
                print(GameManager.Instance.ActualFormGrid.VerifieFormIsCompleted());

            }
        }
       
    }

    private void UnsnapGeometrics()
    {
        _hasSnapped = false;
        _canBeSnapped = false;
        _checkNumberOfVertex = 0;

        for (int i = 0; i < _vertexController.Length; i++)
        {
            _vertexController[i].ResetSnap();
        }

    }

    private void ResetGeometricsPosition()
    {

    }

    #endregion

}


