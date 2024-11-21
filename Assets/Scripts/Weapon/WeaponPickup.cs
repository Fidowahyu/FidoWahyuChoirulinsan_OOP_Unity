using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] public Weapon weaponHolder;

    private Weapon weapon;

    private void Awake()
    {
        // Initialize the weapon with the weaponHolder
        weapon = Instantiate(weaponHolder);
    }

    private void Start()
    {
        // If weapon is not null, disable its visuals
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if(playerWeapon != null)
            {
                playerWeapon.transform.SetParent(transform, false);
                playerWeapon.transform.localPosition = Vector3.zero;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                TurnVisual(false, playerWeapon);
            }
            
            weapon.parentTransform = other.transform;
            weapon.transform.SetParent(Player.Instance.transform);
            weapon.transform.localPosition = new Vector3(0, -0.05f, 2); 
            weapon.transform.localScale = Vector3.one; 
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            // Enable the weapon's visuals
            TurnVisual(true, weapon);
        }
        else {
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }
    }

    // TurnVisual method without parameters for toggling the visual state of the weapon
    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    // Overloaded TurnVisual method with weapon parameter for polymorphic behavior
    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
