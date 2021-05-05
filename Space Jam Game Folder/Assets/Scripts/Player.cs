using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Camera camera;

    [SerializeField] private float moveSpeed;

    private Vector2 movement;
    private Vector2 mousePos;

    private float xAxis, yAxis;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start() 
    {
        
    }

    void Update()
    {
        GetInput();

        movement.x = xAxis;
        movement.y = yAxis;

        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate() 
    {
        Movement();
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }

    private void GetInput() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }
}
