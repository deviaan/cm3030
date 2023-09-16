// Adapted from : https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller/blob/main/Scripts/PlayerController.cs
// https://www.youtube.com/watch?v=3sWTzMsmdx8

using System;
using System.Collections.Generic;
using System.Linq;
using CharacterController;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public PlayerFireControls fireControls;
    [SerializeField] public PlayerHealth playerHealth;
    private Vector3 Velocity { get; set; }
    private Vector3 RawMovement { get; set; }

    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed;
    private float _xMovement = 0;
    private JumpButtonState _jumpButtonState = JumpButtonState.Neutral;

    private bool _isFacingRight = true;
    private float _horizontalMovement;
    
    void Update()
    {
        Velocity = (transform.position - _lastPosition) / Time.deltaTime;
        
        RunCollisionChecks();
        CalculateMove();
        CalculateJumpApex();
        CalculateGravity();
        CalculateJump();
        UpdateCharacterPosition();

        if ((!_isFacingRight && _horizontalMovement > 0f) || (_isFacingRight && _horizontalMovement < 0f))
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(_horizontalMovement));

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
    }
    
    #region collision

    [Header("COLLISION")] [SerializeField] private Bounds characterBounds;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float detectorCount = 3;
    [SerializeField] private float detectionRayLength = 0.1f;
    [SerializeField] [Range(0.1f, 0.3f)] private float rayBuffer = 0.1f;

    private RayRange _raysUp, _raysRight, _raysDown, _raysLeft;
    private bool _colUp, _colRight, _colDown, _colLeft;
    private float _timeLeftGrounded;

    private void CalculateRayRanged()
    {
        var bounds = new Bounds(transform.position + characterBounds.center, characterBounds.size);

        _raysUp = new RayRange(bounds, Vector2.up, rayBuffer);
        _raysRight = new RayRange(bounds, Vector2.right, rayBuffer);
        _raysDown = new RayRange(bounds, Vector2.down, rayBuffer);
        _raysLeft = new RayRange(bounds, Vector2.left, rayBuffer);
    }

    private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
    {
        for (var i = 0; i < detectorCount; ++i)
        {
            yield return Vector2.Lerp(range.Start, range.End, i / (detectorCount - 1));
        }
    }
    
    private void RunCollisionChecks()
    {
        // Generate ray ranges
        CalculateRayRanged();
        
        // Check if grounded
        var groundCheck = RunDetection(_raysDown);
        switch (_colDown)
        {
            case true when !groundCheck:
                // Trigger "coyote time" when not touching ground for the first time.
                _timeLeftGrounded = Time.time;
                break;
            case false when groundCheck:
                // Reset "coyote time" when ground is first touched again
                _coyoteUsable = true;
                animator.SetBool("IsJumping", false);
                break;
        }

        _colDown = groundCheck;
        _colUp = RunDetection(_raysUp);
        _colLeft = RunDetection(_raysLeft);
        _colRight = RunDetection(_raysRight);
        return;

        bool RunDetection(RayRange range)
        {
            return EvaluateRayPositions(range)
                .Any(point => Physics2D.Raycast(
                    point, 
                    range.Dir, 
                    detectionRayLength, 
                    groundLayer
                    )
                );
        }
    }
    
    #endregion

    #region movement controls

    [Header("WALKING")] [SerializeField] private float acceleration = 90;
    [SerializeField] private float moveClamp = 13;
    [SerializeField] private float deaAcceleration = 60f;

    public void Move(InputAction.CallbackContext context)
    {
        if (playerHealth.PlayerCanAct())
        {
            _xMovement = context.ReadValue<Vector2>().x;
        }
    }

    private void CalculateMove()
    {
        _horizontalMovement = _xMovement;
        if (_horizontalMovement != 0)
        {
            _currentHorizontalSpeed += _horizontalMovement * acceleration * Time.deltaTime;
            // Clamp speed
            _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -moveClamp, moveClamp);
        }
        else
        {
            // Slow down character
            _currentHorizontalSpeed = Mathf.MoveTowards(
                _currentHorizontalSpeed, 0, deaAcceleration * Time.deltaTime
                );
        }
        
        // Wall check
        if (_currentHorizontalSpeed > 0 && _colRight || _currentHorizontalSpeed < 0 && _colLeft)
        {
            _currentHorizontalSpeed = 0;
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        fireControls.Flip();
    }
    
    #endregion

    #region Gravity

    [Header("GRAVITY")] [SerializeField] private float fallClamp = -40f;
    [SerializeField] private float minFallSpeed = 80f;
    [SerializeField] private float maxFallSpeed = 120f;
    private float _fallSpeed;

    private void CalculateGravity()
    {
        if (_colDown)
        {
            // Prevents clipping through ground
            if (_currentVerticalSpeed < 0) _currentVerticalSpeed = 0;
        }
        else
        {
            var fallSpeed = _endedJumpEarly && _currentVerticalSpeed > 0
                ? _fallSpeed * jumpEndEarlyGravityModifier
                : _fallSpeed;
            
            // Fall & Clamp
            _currentVerticalSpeed -= Math.Max((fallSpeed * Time.deltaTime), fallClamp);
        }
    }
    
    #endregion

    #region jumping

    [Header("JUMPING")] [SerializeField] private float jumpHeight = 30;
    [SerializeField] private float jumpApexThreshold = 10f;
    [SerializeField] private float coyoteTimeThreshold = 0.1f;
    [SerializeField] private float jumpBuffer = 0.1f;
    [SerializeField] private float jumpEndEarlyGravityModifier = 3;
    
    private bool _coyoteUsable;
    private bool _endedJumpEarly = true;
    private float _apexPoint;
    private float _lastJumpPressed;
    private bool CanUseCoyote => _coyoteUsable && !_colDown && _timeLeftGrounded + coyoteTimeThreshold > Time.time;
    private bool HasBufferedJump => _colDown && _lastJumpPressed + jumpBuffer > Time.time;

    private void CalculateJumpApex()
    {
        if (!_colDown)
        {
            _apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Math.Abs(Velocity.y));
            _fallSpeed = Mathf.Lerp(minFallSpeed, maxFallSpeed, _apexPoint);
        }
        else
        {
            _apexPoint = 0;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && _jumpButtonState != JumpButtonState.Pressed && playerHealth.PlayerCanAct())
        {
            _jumpButtonState = JumpButtonState.Pressed;
            _lastJumpPressed = Time.time;
            animator.SetBool("IsJumping", true);
        } else if (context.canceled)
        {
            _jumpButtonState = JumpButtonState.Released;
        }
    }

    private void CalculateJump()
    {
        if (_jumpButtonState == JumpButtonState.Pressed && CanUseCoyote || HasBufferedJump)
        {
            _currentVerticalSpeed = jumpHeight;
            _endedJumpEarly = false;
            _coyoteUsable = false;
            _timeLeftGrounded = float.MinValue;
        }
        
        // End jump early
        if (!_colDown && _jumpButtonState == JumpButtonState.Released && !_endedJumpEarly && Velocity.y > 0)
        {
            _endedJumpEarly = true;
        }
        
        // Reset button state
        if (_jumpButtonState == JumpButtonState.Released) _jumpButtonState = JumpButtonState.Neutral;

        if (_colUp && _currentVerticalSpeed > 0) _currentVerticalSpeed = 0;
    }
    #endregion

    #region update character

    [Header("MOVE")] [SerializeField, Tooltip("Increase for better collision, at performance cost.")]
    private int freeColliderIterations = 10;

    private void UpdateCharacterPosition()
    {
        // Freeze during hitstun
        if (!playerHealth.PlayerCanAct())
        {
            _currentHorizontalSpeed = 0;
        }
        
        var pos = transform.position + characterBounds.center;
        RawMovement = new Vector3(_currentHorizontalSpeed, _currentVerticalSpeed);
        var move = RawMovement * Time.deltaTime;
        var furthestPoint = pos + move;
        
        // If furthest point is clear, move and continue
        var hit = PosOverlaps(furthestPoint);
        if (!hit)
        {
            transform.position += move;
            return;
        }
        
        // Move as close as we can w/o overlapping
        var positionToMoveTo = transform.position;
        for (int i = 1; i < freeColliderIterations; ++i)
        {
            // Don't check furthest point since we already know that overlaps
            var partialDistance = i / freeColliderIterations;
            var posToTry = Vector2.Lerp(pos, furthestPoint, partialDistance);

            if (PosOverlaps(posToTry))
            {
                transform.position = positionToMoveTo;
                
                // Nudge the player if we've just barely hit a box
                if (i == 1)
                {
                    _currentVerticalSpeed = Math.Max(_currentVerticalSpeed, 0);
                    var dir = transform.position - hit.transform.position;
                    transform.position += dir.normalized * move.magnitude;
                }
                
                return;
            }

            positionToMoveTo = posToTry;
        }

        Collider2D PosOverlaps(Vector2 point)
        {
            return Physics2D.OverlapBox(
                point,
                characterBounds.size,
                0,
                groundLayer
            );
        }
    }

    #endregion
}
