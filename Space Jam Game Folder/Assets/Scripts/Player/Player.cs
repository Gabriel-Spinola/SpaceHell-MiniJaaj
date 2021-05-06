using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Resources;

public class Player : MonoBehaviour
{
    public static Player I { get; private set; }

    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;

    [Header("Stats")]

    [SerializeField] private short health;

    private float lookAngle;

    private Vector2 movement;

    private void Awake() 
    {
        I = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lookAngle = StaticRes.LookDir(transform.position);
        FlipSprite();
        
        movement.x = InputManager.I.xAxis;
        movement.y = InputManager.I.yAxis;

        if (health <= 0)
            StartCoroutine(Die());
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void FlipSprite() => transform.localScale = lookAngle < 90 && lookAngle > -90 ?
        new Vector3(1f, 1f, 1f) :
        new Vector3(-1f, 1f, 1f)
    ;

    private IEnumerator Die() {
        Destroy(gameObject);

        // Animação de morte e mais
        yield return null;

        // Restarta a Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
