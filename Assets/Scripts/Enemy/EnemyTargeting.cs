using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;
    private Transform player;
    private Vector3 screenBounds;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        SetRandomSpawnPoint();
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    /// <summary>
    /// Mengatur posisi spawn acak berdasarkan batas layar
    /// </summary>
    private void SetRandomSpawnPoint()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float randomX = Random.Range(-screenBounds.x, screenBounds.x);
        float randomY = Random.Range(-screenBounds.y, screenBounds.y);
        transform.position = new Vector3(randomX, randomY, 0);
    }

    /// <summary>
    /// Menggerakkan musuh menuju pemain
    /// </summary>
    private void MoveTowardsPlayer()
    {
        if (player == null) return;

        // Hitung arah menuju pemain dan perbarui posisi musuh
        Vector3 targetDirection = (player.position - transform.position).normalized;
        transform.position += targetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hancurkan peluru jika menabrak
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet
        }
        // Hancurkan musuh jika bertabrakan dengan pemain
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
