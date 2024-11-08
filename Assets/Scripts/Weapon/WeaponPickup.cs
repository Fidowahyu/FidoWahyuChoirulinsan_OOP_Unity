using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;
    private Weapon activeWeapon;
    private bool isCooldownActive = false;
    private float pickupCooldown = 0.5f; // Time delay before the weapon can be picked up again

    void Start() {
        Debug.Log("WeaponPickup initialized on " + gameObject.name);
        if (weaponPrefab == null) {
            Debug.LogError("Weapon prefab is missing in " + gameObject.name);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (isCooldownActive) return; // Prevent multiple pickups in a short time

        if (other.CompareTag("Player")) {
            HandleWeaponPickup(other);
        }
    }

    // Handles the process of picking up and replacing the weapon
    private void HandleWeaponPickup(Collider2D playerCollider) {
        Player player = playerCollider.GetComponent<Player>(); // Assume Player class exists
        if (player != null) {
            Weapon currentWeapon = player.GetComponentInChildren<Weapon>();

            // Swap or replace the current weapon if the player has one
            if (currentWeapon != null) {
                Debug.Log("Player already has a weapon, replacing it.");
                StoreWeapon(currentWeapon); // Store or deactivate the old weapon
            }

            EquipNewWeapon(player);

            // Start the cooldown to prevent instant re-pickup
            StartCoroutine(PickupCooldown());
        }
    }

    // Equip a new weapon to the player
    private void EquipNewWeapon(Player player) {
        // Instantiate a new weapon and attach it to the player
        activeWeapon = Instantiate(weaponPrefab, player.transform.position, Quaternion.identity);
        activeWeapon.transform.SetParent(player.transform);
        activeWeapon.parentTransform = player.transform;

        // Disable the pickup object and make the weapon visible
        SetWeaponVisibility(false);
        SetWeaponVisibility(true, activeWeapon);
    }

    // Toggle visibility of the weapon
    private void SetWeaponVisibility(bool isVisible) {
        if (weaponPrefab != null) {
            foreach (var renderer in weaponPrefab.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = isVisible;
            }
        }
    }

    // Overloaded method to control visibility for specific weapon
    private void SetWeaponVisibility(bool isVisible, Weapon weapon) {
        if (weapon != null) {
            foreach (var renderer in weapon.GetComponentsInChildren<Renderer>()) {
                renderer.enabled = isVisible;
            }
        }
    }

    // Store the weapon (could deactivate, pool, or destroy it)
    private void StoreWeapon(Weapon weapon) {
        Debug.Log("Storing the old weapon for future use.");
        weapon.gameObject.SetActive(false); // Disable it instead of destroying, can be reused later
    }

    // Add a cooldown to the weapon pickup to avoid multiple pickups in a short time
    private IEnumerator PickupCooldown() {
        isCooldownActive = true;
        yield return new WaitForSeconds(pickupCooldown); // Wait for the cooldown period
        isCooldownActive = false;
    }
}
