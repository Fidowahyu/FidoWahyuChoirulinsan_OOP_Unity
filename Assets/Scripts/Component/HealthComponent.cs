using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthComponent : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] private float health;

    // Getter untuk health
    public float Health => health;

    // Subtract damage from health
    private void Start()
    {
        health = maxHealth; // Initialize health to maxHealth at the start
    }
    
    public void Subtract(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject); // Destroy the object when health is 0 or less
        }
    }

}

