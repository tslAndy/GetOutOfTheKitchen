using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private string damageTag;
        [SerializeField] private int startHealth;
        [SerializeField] private int calories;
        [SerializeField] private Slider healthSlider;

        [HideInInspector] public int Calories => calories;

        private int _currentHealth;
        public int HealthAmount => _currentHealth;

        private void Awake()
        {
            _currentHealth = startHealth;
            healthSlider.value = 1;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(damageTag))
            {
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
                int damageAmount = projectile == null ? 1 : projectile.Damage;
                _currentHealth -= damageAmount;
                healthSlider.value = _currentHealth / (float)startHealth;
                if (_currentHealth <= 0)
                    Die();
            }
        }

        private void Die()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);

            CaloriesCounterSingleton.Instance.AddCalories(calories);

            Destroy(gameObject);
        }
    }
}
