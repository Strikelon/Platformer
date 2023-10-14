using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _resourcePrefab;
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private int _resourceCount;
    [SerializeField] private LayerMask _restrictedLayer;
    [SerializeField] private float _restrictedRadius;

    void Start()
    {
        SpawnResources();
    }

    void SpawnResources()
    {
        for (int i = 0; i < _resourceCount; i++)
        {
            Vector2 randomPosition = GetRandomPositionInsideArea();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPosition, _restrictedRadius);

            bool isCollidingWithRestrictedArea = false;

            foreach (Collider2D collider in colliders)
            {
                if (_restrictedLayer == (_restrictedLayer | (1 << collider.gameObject.layer)))
                {
                    isCollidingWithRestrictedArea = true;
                    break;
                }
            }

            if (!isCollidingWithRestrictedArea)
            {
                Instantiate(_resourcePrefab, randomPosition, Quaternion.identity);
            }
            else
            {
                i--;
            }
        }
    }

    Vector2 GetRandomPositionInsideArea()
    {
        Bounds rectangleBounds = _spawnArea.GetComponent<Renderer>().bounds;
        Vector2 randomPoint = new Vector2(
            Random.Range(rectangleBounds.min.x, rectangleBounds.max.x),
            Random.Range(rectangleBounds.min.y, rectangleBounds.max.y)
        );
        return randomPoint;
    }
}
