using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private LayerMask whatIsEnemy;

    [SerializeField] private float explosionRadius;
    [SerializeField] private float timeToExplode;
    [SerializeField] private float explosionDuration;

    private bool exploded = false;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (!exploded) 
            StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        if (speed - 0.2f > 0) {
            speed -= 0.2f;
        }

        yield return new WaitForSeconds(timeToExplode);

        Explosion explosion_ = Instantiate(explosion, transform.position, transform.rotation).GetComponent<Explosion>();

        explosion_.damage = damage;
        explosion_.radius = explosionRadius;

        Destroy(gameObject);
    }
}
