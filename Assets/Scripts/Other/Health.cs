using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private string damageTag;
    [SerializeField] private int health;
    public int HealthAmount => health;

}
