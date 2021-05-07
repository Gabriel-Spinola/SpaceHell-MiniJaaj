using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isEnemy;

    public int damage;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        StartCoroutine(DestroyBulletOnTimer(3f));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isEnemy) {
            if (!other.CompareTag("Player") && !other.CompareTag("Bullets")) {
                Destroy(gameObject);
            }

            if (other.CompareTag("Enemy")) {
                Enemy.I.TakeDamage(damage);
            }
        }
        else {
            if (!other.CompareTag("Enemy") && !other.CompareTag("Enemy Bullets")) {
                Destroy(gameObject);
            }

            if (other.CompareTag("Player")) {
                Player.I.TakeDamage(damage);
            }
        }
    }

    private IEnumerator DestroyBulletOnTimer(float timer) {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }
}
