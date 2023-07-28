using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;

    public int Damage => damage;

    public void SetVelocity(Vector2 velocity)
    {
        coll.enabled = true;
        if (transform.parent != null)
            transform.SetParent(null);
        rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) => Destroy(gameObject);
}
