using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public float radius;
    [HideInInspector] public int damage;

    private CircleCollider2D cl;

    private void Awake() => cl = GetComponent<CircleCollider2D>();

    private void Update()
    { 
        cl.radius = radius;

        StartCoroutine(DestroyOnTimer(6f));
    }

    public IEnumerator DestroyOnTimer(float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(gameObject);
    }
}
