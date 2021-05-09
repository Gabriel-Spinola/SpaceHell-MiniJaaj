using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy I { get; private set; }

    protected enum ENEMY_STATE {
        CHASING,
        ATTACKING
    }

    [Header("References")]
    public Transform player;
    [SerializeField] protected LayerMask whatIsPlayer;

    [Space]
    [Header("Stats")]   
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] private float attackRadius;

    protected ENEMY_STATE enemyState;
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 dir;
    private Animator animator;

    protected bool isAttacking;

    private void Awake()
    { 
        I = this;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyState = ENEMY_STATE.CHASING;
    }

    private void Update() 
    {
        animator.SetBool("isChasing", !isAttacking);

        CheckEnemyState();

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (player != null) {
            HandleFlip();

            if (enemyState == ENEMY_STATE.ATTACKING) {
                Attack();
            }
        }
        else {
            EnemyWeapon.canShoot = false;

            animator.SetBool("isChasing", false);
        }
    }

    private void FixedUpdate() {
        if (player != null && enemyState == ENEMY_STATE.CHASING) {
            Chase();
        }
    }

    protected void CheckEnemyState() 
    {
        isAttacking = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        enemyState = isAttacking ? ENEMY_STATE.ATTACKING : ENEMY_STATE.CHASING; 
    }

    private void Chase() 
    {
        EnemyWeapon.canShoot = false;

        dir = transform.position - player.position;
        dir.Normalize();

        rb.MovePosition((Vector2) transform.position + (dir * -speed * Time.deltaTime));
    }

    private void Attack() 
    {
        EnemyWeapon.canShoot = true;
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
            }
        }

        if (other.CompareTag("Explosion")) {
            Explosion explosion = other.GetComponent<Explosion>();

            health -= explosion.damage;
        }
    }
}
