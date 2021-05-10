using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Resources;
using UnityEditor;

public class Player : MonoBehaviour
{
    public static Player I { get; private set; }

    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject text3;

    public int enemiesKilled;
    public int level;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Stats")]

    [HideInInspector] public int health;

    private float lookAngle;

    private Vector2 movement;

    private void Awake() 
    {
        I = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    bool isDrawned1 = false;
    bool isDrawned2 = false;
    bool isDrawned3 = false;
    private void Update()
    {
        // Pega a direção em que o player esta olhando (em angulo)
        lookAngle = StaticRes.LookDir(transform.position);
        FlipSprite();
        
        // Seta os inputs
        movement.x = InputManager.I.xAxis;
        movement.y = InputManager.I.yAxis;

        if (level == 1 && !isDrawned1) {
            StartCoroutine(DrawText1());

            
        }

        if (level == 3 && !isDrawned2) {
            StartCoroutine(DrawText2());
        }

        if (level > 3 && !isDrawned3) {
            StartCoroutine(DrawText1());
        }

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

    public void TakeDamage(int damage)
    { 
        health -= damage;
    }

    private IEnumerator DrawText1() 
    {
        text1.SetActive(true);

        yield return new WaitForSeconds(2f);

        text1.SetActive(false);
        isDrawned1 = true;
    }

    private IEnumerator DrawText2() 
    {
        text2.SetActive(true);

        yield return new WaitForSeconds(2f);

        text2.SetActive(false);
        isDrawned2 = true;
    }

    private IEnumerator DrawText3() 
    {
        text3.SetActive(true);

        yield return new WaitForSeconds(2f);

        text3.SetActive(false);
        isDrawned3 = true;
    }
}
