using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;
    private Vector2 moveDirection;

    private float spawnRangeX = 8f;  // Rentang spawn di X
    private float spawnYRange = 4f;  // Rentang spawn di Y

    private void Start()
    {
        RespawnAtSide();
    }

    private void Update()
    {
        // Gerakkan musuh secara horizontal
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Jika musuh keluar dari layar di bagian kiri atau kanan, respawn di sisi berlawanan
        if (transform.position.x < -spawnRangeX || transform.position.x > spawnRangeX)
        {
            RespawnAtSide();
        }
    }

    // Method untuk memposisikan musuh secara acak di sisi kiri atau kanan layar
    private void RespawnAtSide()
    {
        // Tentukan sisi spawn secara acak (kiri atau kanan)
        float spawnX = Random.Range(0, 2) == 0 ? -spawnRangeX : spawnRangeX;
        float spawnY = Random.Range(-spawnYRange, spawnYRange);

        // Set posisi musuh di sisi kiri atau kanan dengan posisi Y acak
        transform.position = new Vector2(spawnX, spawnY);

        // Tentukan arah pergerakan horizontal berdasarkan sisi spawn
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        // Pastikan rotasi tetap pada keadaan awal (menghadap arah horizontal)
        transform.rotation = Quaternion.identity;
    }
}