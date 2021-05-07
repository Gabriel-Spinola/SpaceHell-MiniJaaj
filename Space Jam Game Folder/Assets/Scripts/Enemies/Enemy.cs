using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class Enemy : MonoBehaviour
{
    public static Enemy I { get; private set; }

    private enum ENEMY_STATE {
        CHASING,
        ATTACKING
    }

    private ENEMY_STATE enemyState;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsPlayer;

    [Header("Stats")]   
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius;

    private Rigidbody2D rb;

    private Vector2 dir;
    private Vector2 movement;

    private bool isAttacking;

    private void Awake()
    { 
        I = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyState = ENEMY_STATE.CHASING;
    }

    private void Update() 
    {
        CheckEnemyState();

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (player != null && enemyState == ENEMY_STATE.CHASING) {
            Chase();
        }
    }

    private void FixedUpdate() {
        if (player != null && enemyState == ENEMY_STATE.CHASING) {
            Attack();
        }
    }

    private void CheckEnemyState() 
    {
        isAttacking = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        enemyState = !isAttacking ? ENEMY_STATE.CHASING : ENEMY_STATE.ATTACKING; 
    }

    private void Chase() 
    {
        dir = transform.position - player.position;
        dir.Normalize();

        rb.MovePosition((Vector2) transform.position + (dir * -speed * Time.deltaTime));
    }

    private void Attack() 
    {
        
    }

    public void TakeDamage(int damage) => health -= damage;
}
