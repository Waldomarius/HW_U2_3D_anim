using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody _rb;
    
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    [Header("Moving options")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 5f;

    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        
        _moveAction = _playerInput.actions["Moved"];
        _jumpAction = _playerInput.actions["Jump"];
        
         // _moveAction = InputSystem.actions.FindAction("Player/Move");
         // _jumpAction = InputSystem.actions.FindAction("Player/Jump");

    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
            
        _moveAction.performed += OnMovePerformed;
        _moveAction.canceled += OnMoveCanceled;
        _jumpAction.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
            
        _moveAction.performed -= OnMovePerformed;
        _moveAction.canceled -= OnMoveCanceled;
        _jumpAction.performed -= OnJumpPerformed;
    }


    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }


    private void FixedUpdate()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y) * (_moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(transform.position + move);
    }
}
