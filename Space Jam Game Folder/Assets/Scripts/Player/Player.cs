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

    public GameObject gameOver;
    public GameObject vic;

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

    bool isText1 = false, isText2 = false, isText3 = false;

    private void Update()
    {
        // Pega a direção em que o player esta olhando (em angulo)
        lookAngle = StaticRes.LookDir(transform.position);
        FlipSprite();
        
        // Seta os inputs
        movement.x = InputManager.I.xAxis;
        movement.y = InputManager.I.yAxis;

        if (level == 1 && !isText1) {
            StartCoroutine(DrawText(text1));
            isText1 = true;
        }

        if (level == 3 && !isText2) {
            StartCoroutine(DrawText(text2));
            isText2 = true;
        }

        if (level > 3 && !isText3) {
            StartCoroutine(DrawText(text3));
            isText3 = true;
        }

        // Morre
        if (health <= 0) {
            Destroy(gameObject);

            gameOver.SetActive(true);
        }
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

    private IEnumerator DrawText(GameObject textUI) 
    {
        textUI.SetActive(true);

        yield return new WaitForSeconds(2f);

        textUI.SetActive(false);
    }
}
