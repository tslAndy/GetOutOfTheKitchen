using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    public void ResetHealth() => _currentHealth = startHealth;
}
