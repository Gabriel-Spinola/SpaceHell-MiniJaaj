using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : Bullet
{
    Vector2 direction;

    [SerializeField] private LayerMask whatIsWall = 0;

    [SerializeField] private int bounceTimes;

    private int bounced;

    private void Start()
    {
        direction = Vector2.right;
        direction.Normalize();
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        
        if (
            Physics2D.Raycast(transform.position, Vector2.right, 0.6f, whatIsWall) ||
            Physics2D.Raycast(transform.position, Vector2.left, 0.6f, whatIsWall)
        ) {
            direction.x = -direction.x + Random.Range(-2f, 2f);

            bounced++;
        }

        if (
            Physics2D.Raycast(transform.position, Vector2.up, 0.6f, whatIsWall) ||
            Physics2D.Raycast(transform.position, Vector2.down, 0.6f, whatIsWall)
        ) {
            direction.y = -direction.y + Random.Range(-2f, 2f);

            bounced++;
        }

        if (bounced >= bounceTimes) {
            Destroy(gameObject);
        }

        StartCoroutine(DestroyBulletOnTimer(lifeTime));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
