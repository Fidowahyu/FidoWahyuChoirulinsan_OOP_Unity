using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 40;  // Kecepatan peluru
    public int damage = 10;         // Kerusakan yang diberikan oleh peluru
    private Rigidbody2D rb;         // Rigidbody2D komponen untuk mengontrol fisika
    private IObjectPool<Bullet> pool;  // Pool objek untuk manajemen pemulihan peluru

    // Fungsi Start dipanggil saat objek pertama kali diinisialisasi
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Mendapatkan komponen Rigidbody2D
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;  // Menetapkan kecepatan peluru berdasarkan arah transform dan kecepatan
        }
    }

    // Update tidak digunakan, dibiarkan kosong
    void Update()
    {
    }

    // Fungsi untuk menetapkan pool yang digunakan untuk melepaskan peluru
    public void SetPool(IObjectPool<Bullet> pool)
    {
        this.pool = pool;
    }

    // Fungsi untuk menembakkan peluru dengan mengatur kecepatan geraknya
    public void Shoot()
    {
        if (pool != null && rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;  // Menetapkan kembali kecepatan peluru
        }
    }

    // Fungsi ini dipanggil saat peluru bertabrakan dengan objek lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && pool != null)
        {
            pool.Release(this);  // Melepaskan peluru kembali ke pool
        }
    }

    // Fungsi ini dipanggil ketika peluru keluar dari layar (tidak terlihat)
    private void OnBecameInvisible()
    {
        if (pool != null)
        {
            pool.Release(this);  // Melepaskan peluru kembali ke pool
        }
    }
}
