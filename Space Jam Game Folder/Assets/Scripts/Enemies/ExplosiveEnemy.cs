using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [SerializeField] private float explosionDelay;
    [SerializeField] private float explosionRadius;
    [SerializeField] private int explosionDamage;
    
    [Space]
    [SerializeField] private bool drawGizmos;

    private bool hasPlayerBeenAffected;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

    private void Start() 
    { 
        enemyState = ENEMY_STATE.CHASING;
    }

    private void Update() {
        CheckEnemyState();
        HandleFlip();

        if (player != null) {
            if (enemyState == ENEMY_STATE.ATTACKING) {
                StartCoroutine(Attack());
            }
        }
    }

    private void FixedUpdate() {
        if (player != null && enemyState == ENEMY_STATE.CHASING) {
            Chase();
        }
    }

    private void Chase() 
    {
        dir = transform.position - player.position;
        dir.Normalize();

        rb.MovePosition((Vector2) transform.position + (dir * -speed * Time.deltaTime));
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(explosionDelay);

        hasPlayerBeenAffected = Physics2D.OverlapCircle(transform.position, explosionRadius, whatIsPlayer);

        if (hasPlayerBeenAffected) {
            Player.I.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (drawGizmos) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, explosionRadius);
        }
    }
}
