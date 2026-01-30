using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput = null;


    private InputAction _dragInputAction;
    private InputAction _rotateInputAction;
    private InputAction _mouseInputAction; 
    
    private bool _wasDragButtonPressed;
    private bool _wasDragButtonRelased;
    private bool _isDragButtonPressed;
    private bool _wasRotateButtonPressed;
    private bool _wasRotateButtonRelased;
    private bool _isRotateButtonPressed;
    private Vector2 _mousePosition; 


    private static InputManager _instance;

    #region Properties
    public static InputManager Instance { get => _instance; set => _instance = value; }
    public bool WasDragButtonPressed { get => _wasDragButtonPressed; set => _wasDragButtonPressed = value; }
    public bool WasDragButtonRelased { get => _wasDragButtonRelased; set => _wasDragButtonRelased = value; }
    public bool IsDragButtonPressed { get => _isDragButtonPressed; set => _isDragButtonPressed = value; }
    public bool WasRotateButtonPressed { get => _wasRotateButtonPressed; set => _wasRotateButtonPressed = value; }
    public bool WasRotateButtonRelased { get => _wasRotateButtonRelased; set => _wasRotateButtonRelased = value; }
    public bool IsRotateButtonPressed { get => _isRotateButtonPressed; set => _isRotateButtonPressed = value; }
    public Vector2 MousePosition { get => _mousePosition; set => _mousePosition = value; }

    #endregion

    public void Init()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        _dragInputAction = _playerInput.actions["Drag"];
        _rotateInputAction = _playerInput.actions["Rotate"];
        _mouseInputAction = _playerInput.actions["Mouse Position"];
    }

    private void Update()
    {
        MousePosition = _mouseInputAction.ReadValue<Vector2>();
        
        WasDragButtonPressed = _dragInputAction.WasPressedThisFrame(); 
        WasDragButtonRelased = _dragInputAction.WasReleasedThisFrame();
        IsDragButtonPressed = _dragInputAction.IsPressed();

        WasRotateButtonPressed = _rotateInputAction.WasPressedThisFrame();
        WasRotateButtonRelased = _rotateInputAction.WasReleasedThisFrame();
        IsRotateButtonPressed = _dragInputAction.IsPressed();   
    }
}
