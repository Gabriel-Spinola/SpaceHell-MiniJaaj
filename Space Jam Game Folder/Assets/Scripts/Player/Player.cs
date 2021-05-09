using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Resources;

public class Player : MonoBehaviour
{
    public static Player I { get; private set; }

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Stats")]

    [SerializeField] private int health;

    private float lookAngle;

    private Vector2 movement;

    private void Awake() 
    {
        I = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Pega a direção em que o player esta olhando (em angulo)
        lookAngle = StaticRes.LookDir(transform.position);
        FlipSprite();
        
        // Seta os inputs
        movement.x = InputManager.I.xAxis;
        movement.y = InputManager.I.yAxis;

        // Morre
        if (health <= 0)
            StartCoroutine(Die());
    }

    private void FixedUpdate()
    {
        animator.SetBool("isMoving", InputManager.I.xAxis != 0 || InputManager.I.yAxis != 0);

        // Move o player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    // Flip o sprite baseado na direção que ele esta olhando
    private void FlipSprite() => spriteRenderer.flipX = lookAngle < 90 && lookAngle > -90 ? false : true;

    private IEnumerator Die() {
        Destroy(gameObject);

        // Animação de morte e mais
        yield return new WaitForSeconds(1f);

        // Restarta a Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TakeDamage(int damage) => health -= damage;
}
