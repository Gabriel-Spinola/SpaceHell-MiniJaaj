using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy I { get; private set; }

    protected enum ENEMY_STATE {
        NEUTRAL,
        CHASING,
        ATTACKING
    }

    [Header("References")]
    public Transform player;

    [SerializeField] protected GameObject deadBody;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected LayerMask whatIsWall;

    [Space]
    [Header("Stats")]   
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] private float attackRadius;
    [SerializeField] private float chasingRadius;

    EnemyWeapon enemyWeapon;
    protected ENEMY_STATE enemyState;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 dir;
    private Animator animator;

    public bool isAttacking;
    protected bool isChasing;

    private void Awake()
    { 
        I = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Transform enemyWeapon_ = transform.Find("Rifle");

        if (enemyWeapon_ != null) {
            enemyWeapon = enemyWeapon_.GetComponent<EnemyWeapon>();
        }
    } 

    private void Start()
    {
        enemyState = ENEMY_STATE.NEUTRAL;
    }

    private void Update() 
    {
        animator.SetBool("isChasing", isChasing);

        if (health <= 0) {
            Instantiate(deadBody, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        CheckEnemyState();

        if (player != null) {
            HandleFlip();

            if (enemyState == ENEMY_STATE.ATTACKING) {
                Attack();
            }
        }
        else {
            if (enemyWeapon != null) {
                enemyWeapon.canShoot = false;
            }

            animator.SetBool("isChasing", false);
        }
    }

    private void FixedUpdate() 
    {
        if (player != null && enemyState == ENEMY_STATE.CHASING && enemyState != ENEMY_STATE.NEUTRAL) {
            Chase();
        }
    }

    private void OnDestroy() 
    {
        Player.I.enemiesKilled++;
    }

    protected void CheckEnemyState() 
    {
        isAttacking = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);
        isChasing = Physics2D.OverlapCircle(transform.position, chasingRadius, whatIsPlayer);

        if(isAttacking) {
            isAttacking = true;
            isChasing = false;

            enemyState = ENEMY_STATE.ATTACKING;
        }
        else if (isChasing){
            isChasing = true;
            isAttacking = false;

            enemyState = ENEMY_STATE.CHASING;
            
        }
        else {
            isChasing = false;
            isAttacking = false;

            enemyState = ENEMY_STATE.NEUTRAL;
        }
    }

    private void Chase() 
    {
        if (enemyWeapon != null) {
            enemyWeapon.canShoot = false;
        }

        dir = transform.position - player.position;
        dir.Normalize();

        rb.MovePosition((Vector2) transform.position + (dir * -speed * Time.deltaTime));
    }

    private void Attack() 
    {
        if (enemyWeapon != null) {
            enemyWeapon.canShoot = true;
        }
    }

    protected void HandleFlip() 
    {
        Vector2 dir_ = transform.position - player.position;
        float lookAngle = Mathf.Atan2(dir_.y, dir_.x) * Mathf.Rad2Deg;

        spriteRenderer.flipX = lookAngle < 90 && lookAngle > -90 ? true : false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.CompareTag("Bullets") && !other.CompareTag("Ex Bullets")) {
            Bullet bullet = other.GetComponent<Bullet>();

            if(!bullet.isEnemy) {
                health -= bullet.damage;

                StartCoroutine(Dan());
            }
        }

        if (other.CompareTag("Explosion")) {
            Explosion explosion = other.GetComponent<Explosion>();

            health -= explosion.damage;

            StartCoroutine(Dan());
        }
    }

    private IEnumerator Dan()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = Color.white;
    }
}
