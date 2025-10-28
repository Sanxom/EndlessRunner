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
    [SerializeField] private Animator animator;

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

    [Header("Run Fields")]
    private const string IS_RUNNING_ANIM_NAME = "isRunning";
    private const string X_VELOCITY_ANIM_NAME = "xVelocity";
    private const string Y_VELOCITY_ANIM_NAME = "yVelocity";
    private const string IS_GROUNDED_ANIM_NAME = "isGrounded";
    private bool _isPlayerUnlocked = false;
    #endregion

    #region Public Properties
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

    private void Update()
    {
        if (!_isPlayerUnlocked) return;
        HandleAnimations();
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
    }

    private void HandleAnimations()
    {
        animator.SetBool(IS_GROUNDED_ANIM_NAME, IsGrounded);
        animator.SetFloat(X_VELOCITY_ANIM_NAME, rb.linearVelocityX);
        animator.SetFloat(Y_VELOCITY_ANIM_NAME, rb.linearVelocityY);
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (!_isPlayerUnlocked) return;
        if (!IsGrounded) return;

        rb.linearVelocityY = jumpForce;
    }

    private void OnBeginGamePerformed(InputAction.CallbackContext obj)
    {
        _isPlayerUnlocked = true;
        _beginGameAction.Disable();
    }
    #endregion
}