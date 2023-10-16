using UnityEngine;

public class PlayerFollower: MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _playerPosition;
    private float _zCoordinateCorrection = -10f;
    private float _yCoordinateCorrection = 1f;

    private void Avake()
    {
        if (_player != true)
        {
            _player = FindAnyObjectByType<Player>().transform;
        }
    }

    private void Update()
    {
        _playerPosition = _player.position;
        _playerPosition.z = _zCoordinateCorrection;
        _playerPosition.y += _yCoordinateCorrection;
        transform.position = Vector3.Lerp(transform.position, _playerPosition, Time.deltaTime);
    }
}
