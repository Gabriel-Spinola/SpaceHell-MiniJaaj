﻿using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet I { get; private set; }

    [SerializeField] private bool isEnemy;

    [HideInInspector] public int damage;

    [HideInInspector] public float speed;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        StartCoroutine(DestroyBulletOnTimer(6f));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Bullets")) {
            if (!isEnemy) {
                if (!other.CompareTag("Player")) {
                    Destroy(gameObject);
                }

                if (other.CompareTag("Enemy")) {
                    Enemy.TakeDamage(damage);
                }
            }
            else {
                if (!other.CompareTag("Enemy")) {
                    Destroy(gameObject);
                }

                if (other.CompareTag("Player")) {
                    Player.I.TakeDamage(damage);
                }
            }
        }
    }

    private IEnumerator DestroyBulletOnTimer(float timer) {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }
}
