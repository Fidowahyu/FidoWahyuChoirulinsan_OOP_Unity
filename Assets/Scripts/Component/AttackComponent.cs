using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;  // Bullet used for attack
    public int damage; 

    private void Start()
    {
        // Pastikan Collider2D diatur sebagai trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (!collider.isTrigger)
        {
            collider.isTrigger = true;
            Debug.LogWarning("Collider2D pada " + gameObject.name + " diatur sebagai trigger.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek apakah objek yang bertabrakan memiliki tag yang sama
        if (collision.CompareTag(gameObject.tag))
        {
            return; // Abaikan jika tag sama
        }

        // Cek apakah objek yang bertabrakan memiliki HitboxComponent
        HitboxComponent hitbox = collision.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Jika ada Bullet yang terhubung, gunakan damage dari Bullet
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else
            {
                // Jika tidak ada Bullet, gunakan damage default
                hitbox.Damage(damage);
            }
        }
    }
}
