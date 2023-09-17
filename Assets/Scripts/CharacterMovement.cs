using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterMovement: MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public Animator animator;
    public PlayerFireControls fireControls;
    public PlayerHealth playerHealth;
    [SerializeField] public float speed = 8f;
    [SerializeField] public float jumpPower = 16;
    [SerializeField] public Vector2 boxSize;
    [SerializeField] public float castDistance;
    [SerializeField] public float jumpCooldown = 1f;

    private float _horizontal;
    private bool _isFacingRight = true;
    private bool _isJumping = false;
    private float _animationClear;
    private float _animationDelay = 0.2f;

    void Update()
    {
        rb.velocity = new Vector2(_horizontal * speed, rb.velocity.y);
        if (!_isFacingRight && _horizontal > 0f || _isFacingRight && _horizontal < 0f)
        {
            Flip(); 
        }
        animator.SetFloat("Speed", Mathf.Abs(_horizontal));

        if (fireControls.isShooting && Time.time > fireControls.nextShot)
        {
            fireControls.ToggleFiring();
            animator.SetBool("IsShooting", false);
        }
        
        if (playerHealth.wasHit && playerHealth.PlayerCanAct()) 
        {
            playerHealth.ClearHitStun();
            animator.SetBool("IsHit", false);
        }

        if (IsGrounded() && _isJumping && Time.time > _animationClear)
        {
            animator.SetBool("IsJumping", false);
            _isJumping = false;
        }
    }

    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!playerHealth.PlayerCanAct()) return;

        if (context.started && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.SetBool("IsJumping", true);
            _isJumping = true;
            _animationClear = Time.time + _animationDelay;
        }

        // if (context.canceled)
        // {
        //     rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.5f);
        // }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        fireControls.Flip();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!playerHealth.PlayerCanAct()) return;
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
