using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}
