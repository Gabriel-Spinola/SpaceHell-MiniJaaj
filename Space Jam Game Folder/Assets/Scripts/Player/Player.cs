using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class Player : MonoBehaviour
{
    public static Player I { get; private set; }

    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0f;

    private float lookAngle;

    private Vector2 movement;
    private Vector2 mousePos;

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
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void FlipSprite() => transform.localScale = lookAngle < 90 && lookAngle > -90 ? new Vector3(1f, 1f, 1f) : new Vector3(-1f, 1f, 1f);
}
