using UnityEngine;

public class EnemyLifeHolder : MonoBehaviour
{
    [SerializeField] private float _baseHealth = 30;
    [SerializeField] private float _damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log($"Attack player");
            player.GetDamage(_damage);

            _baseHealth -= player.GiveDamage();
            if (_baseHealth <= 0)
            {
                Destroy(gameObject);
            }
            Debug.Log($"Enemy health {_baseHealth}");
        }
    }
}