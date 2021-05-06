using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        StartCoroutine(DestroyBulletOnTimer(3f));
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBulletOnTimer(float timer) {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }
}
