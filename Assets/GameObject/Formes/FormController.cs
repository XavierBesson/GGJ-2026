using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FormController : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private VertexController[] _vertexController;
    [SerializeField] private EColor _actualColor = EColor.VOID;
    [SerializeField] private float _speedFollowMouse = 20.0f; 

    private Animator _formsAnimator; 

    public EColor ActualColor { get => _actualColor; set => _actualColor = value; }

    private enum EGeometricsState
    {
        Idle,
        Dragging,
        Snapped
    }

    private EGeometricsState _state = EGeometricsState.Idle;

    private bool _canBeSnapped = false;
    private int _checkNumberOfVertex = 0;
    private Vector3 _firstPosition;

    #region Build-In Unity Methods

    private void Start()
    {
        _firstPosition = transform.position;

        _formsAnimator = GetComponentInChildren<Animator>();      
    }

    private void Update()
    {
        if (_state == EGeometricsState.Dragging)
        {
            FollowMouse();
        }

        HandleDragRelease();
    }

    private void OnEnable()
    {
        if (_vertexController == null) return;

        for (int i = 0;  i < _vertexController.Length; i++)
        {
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
        if (_state == EGeometricsState.Snapped)
        {
            UnsnapGeometrics();
        }

        _state = EGeometricsState.Dragging;
    }

    private void OnMouseOver()
    {
        if (_formsAnimator == null) return; 

        if (_state != EGeometricsState.Dragging)
        {
            _formsAnimator.SetBool("CanHover", true);
        }
    }

    private void OnMouseExit()
    {
        if (_formsAnimator == null) return;

        _formsAnimator.SetBool("CanHover", false);
    }

    #endregion

    #region Drag

    private void FollowMouse()
    {
        Vector3 mousePosition = InputManager.Instance.MousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, worldPosition, 1f - Mathf.Exp(-_speedFollowMouse * Time.deltaTime));
    }

    private void HandleDragRelease()
    {
        if (_state != EGeometricsState.Dragging) return;

        if (InputManager.Instance.WasDragButtonRelased)
        {
            if (_canBeSnapped)
            {
                SnapGeometrics();
            }
            else
            {
                ResetGeometricsPosition();
            }
        }
    }

    #endregion

    #region Snap

    private void HandleVertexCollisionEnter(Collider2D collision)
    {
        _checkNumberOfVertex = 0;

        
        for (int i = 0; i < _vertexController.Length; i++)
        {
            if (_vertexController[i].IsVertexOnSnapPoint)
            {
                _checkNumberOfVertex++;
            }
        }
        _canBeSnapped = _checkNumberOfVertex == _vertexController.Length;

        Debug.Log($"Vertex validés {_checkNumberOfVertex} / {_vertexController.Length}");
    }

    private void SnapGeometrics()
    {
        Vector3 center = Vector3.zero;
        int validVertexCount = 0;

        
        for (int i = 0;  i < _vertexController.Length; i++)
        {
            if (_vertexController[i].CollisionTransform != null)
            {
                center += _vertexController[i].CollisionTransform.position;
                validVertexCount++;
            }
        }
        

        if (validVertexCount > 0)
        {
            center /= validVertexCount;
            transform.position = center;
        }

        _state = EGeometricsState.Snapped;
        _canBeSnapped = false;

        Debug.Log(GameManager.Instance.ActualFormGrid.VerifieFormIsCompleted());
    }

    private void UnsnapGeometrics()
    {
        _state = EGeometricsState.Idle;
        _canBeSnapped = false;
        _checkNumberOfVertex = 0;

        for (int i = 0; i < _vertexController.Length;i++)
        {
            _vertexController[i].ResetSnap();
        }
    }

    private void ResetGeometricsPosition()
    {
        transform.position = _firstPosition;
        _state = EGeometricsState.Idle;
        _canBeSnapped = false;
    }

    #endregion

}


