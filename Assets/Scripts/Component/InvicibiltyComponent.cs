using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvicibiltyComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private HitboxComponent hitboxComponent;
    private Material originalMaterial;
    public bool isInvincible = false;
    // Start is called before the first frame update
    void Awake()
    {
        hitboxComponent = GetComponent<HitboxComponent>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }
    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < blinkingCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            Debug.Log("Invincible");
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            Debug.Log("Vulnerable");
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false; // Setelah selesai, set invincible menjadi false
    }
    public void StartInvincibility()
    {
        if (!isInvincible) // Pastikan entity belum invincible
        {
            StartCoroutine(FlashRoutine());
            Debug.Log("Started invincibility."); // Mulai efek blinking
        }
    }
    void Update()
    {

    }
}
