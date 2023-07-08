using UnityEngine;

namespace Other
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private string damageTag;
        [SerializeField] private int startHealth;

        private int _currentHealth;
        public int HealthAmount => _currentHealth;

        private void Awake()
        {
            _currentHealth = startHealth;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(damageTag))
            {
                _currentHealth -= 1;
                if (_currentHealth <= 0)
                    Destroy(gameObject);
            }
        }
    }
}
