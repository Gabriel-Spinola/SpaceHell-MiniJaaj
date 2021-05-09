using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class BounceBullet : Bullet
{
    Vector2 direction;

    [SerializeField] private LayerMask whatIsWall = 0;

    [SerializeField] private int bounceTimes;

    private float randDir;

    private int bounced;

    private void Start()
    {
        direction = Vector2.right;
        direction.Normalize();

        transform.localScale = scale;
    }

    private void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
        
        if (bounced >= 2) {
            Destroy(gameObject);
        }

        StartCoroutine(DestroyBulletOnTimer(lifeTime));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Left Wall" || other.gameObject.tag == "Right Wall") {
            direction = Vector2.Reflect(direction, other.contacts[0].normal);

            bounced++;
        }
        
        if (other.gameObject.tag == "Up Wall" || other.gameObject.tag == "Down Wall") {
            randDir = Random.Range(-2f, 2f);

            direction.y = -direction.y;
            speed = -speed;

            Vector2 dir = (Vector2) transform.position - direction * randDir;
            dir.Normalize();

            transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
            transform.Rotate(Vector3.right * randDir);

            direction = Vector2.Reflect(direction, other.contacts[0].normal);

            bounced++;
        }
    }
}
