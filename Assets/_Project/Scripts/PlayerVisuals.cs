using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Player _player;

    private const string IS_RUNNING_ANIM_NAME = "isRunning";
    private const string X_VELOCITY_ANIM_NAME = "xVelocity";
    private const string Y_VELOCITY_ANIM_NAME = "yVelocity";
    private const string IS_GROUNDED_ANIM_NAME = "isGrounded";

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!_player.IsPlayerUnlocked) return;

        HandleAnimations();
    }

    private void HandleAnimations()
    {
        animator.SetBool(IS_GROUNDED_ANIM_NAME, _player.IsGrounded);
        animator.SetFloat(X_VELOCITY_ANIM_NAME, _player.LinearVelocityX);
        animator.SetFloat(Y_VELOCITY_ANIM_NAME, _player.LinearVelocityY);
    }
}