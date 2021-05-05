using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Rigidbody2D weaponRb;
    [SerializeField] private Transform weaponTransform;

    [SerializeField] private float moveSpeed;

    private SpriteRenderer sprite;

    private Vector2 movement;
    private Vector2 mousePos;

    private float xAxis, yAxis;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetInput();

        movement.x = xAxis;
        movement.y = yAxis;
    }

    void FixedUpdate() 
    {
        Movement();
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        HandleRotation();
    }

    private void HandleRotation() {
        if (weaponRb.rotation > 65 || weaponRb.rotation < -65) {
            transform.localScale = new Vector3(-1, 1, 1);

            weaponTransform.transform.localScale = new Vector3(-1, -1, 1);
        }
        else if (Mathf.Abs(weaponRb.rotation) == -90 || Mathf.Abs(weaponRb.rotation) == 90) {
            return;
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);

            weaponTransform.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void GetInput() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }
}
