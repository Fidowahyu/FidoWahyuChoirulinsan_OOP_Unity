using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Attributes")]
    public float speed = 20;
    public int damageAmount = 10;
    private Rigidbody2D rigidbodyComponent;
    public IObjectPool<Bullet> Pool { get; set; }

    void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rigidbodyComponent.velocity = transform.up * speed;
    }

    public void AssignPool(IObjectPool<Bullet> pool)
    {
        this.Pool = pool;
    }

    void OnBecameInvisible()
    {
        Pool.Release(this);
    }
}
