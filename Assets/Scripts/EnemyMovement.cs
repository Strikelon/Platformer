using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private float _speed;
    [SerializeField] private ContactFilter2D _filter2D;
    [SerializeField] private float _viewDistance;

    private float _startPositionX, _endPositionX;
    private bool _isMovingRight = true;
    private float _divider = 2;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[1];
    private Vector2 _castDirectrion;
    private bool _isPlayerDetected;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Renderer gameObjectRenderer = GetComponent<Renderer>();
        Bounds gameObjectBounds = gameObjectRenderer.bounds;

        Renderer platformRenderer = _platform.GetComponent<Renderer>();
        Bounds platformBounds = platformRenderer.bounds;

        _startPositionX = platformBounds.min.x + gameObjectBounds.size.x / _divider;
        _endPositionX = platformBounds.max.x - gameObjectBounds.size.x / _divider;
    }

    private void Update()
    {
        if(_isMovingRight)
        {
            _castDirectrion = transform.right;
        } else
        {
            _castDirectrion = transform.right * -1;
        }

        int collisionCount = _rigidbody2D.Cast(_castDirectrion, _filter2D, _raycastResults, _viewDistance);

        if (collisionCount > 0)
        {
            _isPlayerDetected = true;
        }
        else
        {
            _isPlayerDetected = false;
        }

        if (_isPlayerDetected)
        {
            Collider2D playerCollider = _raycastResults[0].collider;
            Transform playerTransform = playerCollider.transform;

            Vector3 targetDirection = (playerTransform.position - transform.position).normalized;
            float targetPositionX = transform.position.x + targetDirection.x * _speed * Time.deltaTime;

            targetPositionX = Mathf.Clamp(targetPositionX, _startPositionX, _endPositionX);

            transform.position = new Vector3(targetPositionX, transform.position.y, transform.position.z);

            if (targetDirection.x > 0)
            {
                _isMovingRight = true;
            }
            else
            {
                _isMovingRight = false;
            }
        }
        else
        {
            if (_isMovingRight)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (transform.position.x <= _startPositionX)
            {
                _isMovingRight = true;
            }
            else if (transform.position.x >= _endPositionX)
            {
                _isMovingRight = false;
            }
        }

        _spriteRenderer.flipX = !_isMovingRight;
    }
}