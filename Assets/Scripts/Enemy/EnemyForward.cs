using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 2f;
    private float spawnRangeX = 8f;  // Rentang spawn di X
    private float spawnY = 10f;      // Posisi Y spawn di atas layar

    private void Start()
    {
        // Posisikan musuh secara acak di bagian atas layar
        RespawnAtTop();
    }

    private void Update()
    {
        // Gerakkan musuh ke bawah
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Jika musuh keluar dari layar di bagian bawah, respawn di bagian atas layar
        if (transform.position.y < -spawnY)
        {
            RespawnAtTop();
        }
    }

    // Method untuk memposisikan musuh secara acak di bagian atas layar
    private void RespawnAtTop()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);  // Posisi spawn acak di kiri atau kanan
        transform.position = new Vector2(randomX, spawnY);

        // Pastikan rotasi tetap pada keadaan awal (menghadap ke bawah secara natural)
        transform.rotation = Quaternion.identity;
    }

    
}