using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string AxisHorizontal = "Horizontal";
    private const string AxisJump = "Jump";
    private const string State = "state";

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private Vector3 _groundBoxSize;
    [SerializeField] private float _maxGroundDistance;
    [SerializeField] private LayerMask _groundLayerMask;

    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private AnimatorStates _animatorState
    {
        get { return (AnimatorStates)_animator.GetInteger(State); }
        set { _animator.SetInteger(State, (int) value); }
    }

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CheckIsGround())
        {
            _animatorState = AnimatorStates.Idle;
        }
        else
        {
            _animatorState = AnimatorStates.Jump;
        }

        if (Input.GetButton(AxisHorizontal))
        {
            Run();
        }

        if (CheckIsGround() && Input.GetButtonDown(AxisJump))
        {
            Jump();
        }
    }

    private void Run()
    {
        if (CheckIsGround())
        {
            _animatorState = AnimatorStates.Run;
        }

        Vector3 direction = transform.right * Input.GetAxis(AxisHorizontal);
        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
        _spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        _rigidBody2D.velocity = _jumpForce * Vector3.up;
    }

    private bool CheckIsGround()
    {
        if (Physics2D.BoxCast(transform.position, _groundBoxSize,0, -transform.up, _maxGroundDistance, _groundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum AnimatorStates
{
    Idle,
    Run,
    Jump
}