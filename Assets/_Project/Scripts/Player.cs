using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Event Fields
    #endregion

    #region Public Fields
    #endregion

    #region Serialized Private Fields
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Run Fields")]
    [SerializeField] private float moveSpeed;

    [Header("Jump Fields")]
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float _groundCheckDistance;
    #endregion

    #region Private Fields
    [Header("Input Fields")]
    private GameInput _gameInput;
    private InputAction _jumpAction;
    private InputAction _beginGameAction;
    #endregion

    #region Public Properties
    public float LinearVelocityX { get; private set; }
    public float LinearVelocityY { get; private set; }
    public bool IsPlayerUnlocked { get; private set; } = false;
    public bool IsGrounded => Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _gameInput = new();
        _jumpAction = _gameInput.Player.Jump;
        _beginGameAction = _gameInput.Player.BeginGame;
    }

    private void OnEnable()
    {
        _gameInput.Enable();
        _jumpAction.performed += OnJumpPerformed;
        _beginGameAction.performed += OnBeginGamePerformed;
        _beginGameAction.Enable();
    }

    private void OnDisable()
    {
        _jumpAction.performed -= OnJumpPerformed;
        _beginGameAction.performed -= OnBeginGamePerformed;
    }

    private void FixedUpdate()
    {
        if (!IsPlayerUnlocked) return;
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new(transform.position.x, transform.position.y - _groundCheckDistance));
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocityY);
        SetLinearVelocityProperties();
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (!IsPlayerUnlocked) return;
        if (!IsGrounded) return;

        rb.linearVelocityY = jumpForce;
    }

    private void OnBeginGamePerformed(InputAction.CallbackContext obj)
    {
        IsPlayerUnlocked = true;
        _beginGameAction.Disable();
    }

    private void SetLinearVelocityProperties()
    {
        LinearVelocityX = rb.linearVelocityX;
        LinearVelocityY = rb.linearVelocityY;
    }
    #endregion
}