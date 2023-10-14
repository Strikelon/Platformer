using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    [SerializeField] private float _speed;
    
    private float _startPositionX, _endPositionX;
    private bool _isMovingRight = true;
    private float _divider = 2;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Renderer gameObjectRenderer = GetComponent<Renderer>();
        Bounds gameObjectBounds = gameObjectRenderer.bounds;

        Renderer platformRenderer = _platform.GetComponent<Renderer>();
        Bounds platformBounds = platformRenderer.bounds;

        _startPositionX = platformBounds.min.x + gameObjectBounds.size.x / _divider;
        _endPositionX = platformBounds.max.x - gameObjectBounds.size.x / _divider;
    }

    private void Update()
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

        _spriteRenderer.flipX = !_isMovingRight;
    }
}