using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemy : Enemy
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float explosionDelay;
    [SerializeField] private float explosionRadius;
    [SerializeField] private int explosionDamage;
    
    [Space]
    [SerializeField] private bool drawGizmos;

    private bool hasPlayerBeenAffected;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    private void Start() 
    { 
        enemyState = ENEMY_STATE.CHASING;
    }

    private void Update() {
        CheckEnemyState();

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
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(explosionDelay);

        hasPlayerBeenAffected = Physics2D.OverlapCircle(transform.position, explosionRadius, whatIsPlayer);

        Explosion explosion_ = Instantiate(explosion, transform.position, transform.rotation).GetComponent<Explosion>();

        explosion_.damage = explosionDamage;
        explosion_.radius = explosionRadius;

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
