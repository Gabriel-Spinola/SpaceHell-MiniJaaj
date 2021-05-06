using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Rigidbody2D weaponRb;
    [SerializeField] private Transform weaponTransform;

    [SerializeField] private float moveSpeed;

    private Vector2 movement;
    private Vector2 mousePos;

    private float xAxis, yAxis;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

    /**
     * Rotation Bug Constraints         
     * right: { 69, -69 }
     * left:  { 92, -92 }
    */
    private void HandleRotation() {
        float absRotation = Mathf.Abs(weaponRb.rotation);

        print(absRotation);

        if (
            (absRotation >= 69 && absRotation <= 92) ||
            (absRotation <=-69 && absRotation >=-92)
        ) {
            return;
        }

        if (absRotation > 90 || absRotation < -90) {
            transform.localScale = new Vector3(-1, 1, 1);

            weaponTransform.localScale = new Vector3(-1, -1, 1);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);

            weaponTransform.localScale = new Vector3(1, 1, 1); 
        }
    }

    private void GetInput() 
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
    }
}
