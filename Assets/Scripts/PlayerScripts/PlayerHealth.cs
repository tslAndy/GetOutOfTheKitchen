using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startHealth;
    [SerializeField] private Slider healthSlider;
    
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = startHealth;
        healthSlider.value = 1;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            _currentHealth -= 1;
            healthSlider.value = _currentHealth / (float) startHealth;
            if (_currentHealth <= 0)
                GameManager.Instance.SetPlayerIsDead();
        }
    }
}
