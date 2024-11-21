using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    private InvicibiltyComponent invincibilityComponent; 

    private void Start()
    {
        // Fetch the HealthComponent attached to the same GameObject or parent
        healthComponent = GetComponent<HealthComponent>(); 
        invincibilityComponent = GetComponent<InvicibiltyComponent>();
        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on " + gameObject.name);
        }
    }

    // Method to damage using Bullet
    public void Damage(Bullet bullet)
    {
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible) // Cek apakah invincible
        {
            if (healthComponent != null)
            {
                healthComponent.Subtract(bullet.damage); // Kurangi health berdasarkan damage dari bullet
                invincibilityComponent.StartInvincibility();
            }
        }
    }

    // Method to damage using an integer value
    public void Damage(int damage)
    {
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible) // Cek apakah invincible
        {
            healthComponent.Subtract(damage); // Kurangi health berdasarkan damage integer
            invincibilityComponent.StartInvincibility();
        }
    }
}
