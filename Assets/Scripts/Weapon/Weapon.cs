using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 0.01f;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    void Start()
    {
        if (parentTransform == null)
        {
            parentTransform = transform; 
        }
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
        timer = shootIntervalInSeconds;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (bullet != null && bulletSpawnPoint != null)
            {
                Shoot();
            }
            timer = shootIntervalInSeconds;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, transform);
        newBullet.SetPool(objectPool);
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;
        }
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    private void Shoot()
    {
        Bullet spawnedBullet = objectPool.Get();
        if (spawnedBullet != null)
        {
            spawnedBullet.Shoot();
        }
    }
}