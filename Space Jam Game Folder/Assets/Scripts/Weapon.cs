using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private Transform gunTip;

    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D playerPos;

    private Rigidbody2D rb;

    private Vector2 mousePos;
    private Vector2 lookDir;

    private float angle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleRotation();

        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot() 
    {
        Instantiate(bullet, gunTip.position, transform.rotation);
    }

    private void HandleRotation() 
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;

        float absRotation = Mathf.Abs(angle);

        if (
            (absRotation >= 69 && absRotation <= 92) ||
            (absRotation <=-69 && absRotation >=-92)
        ) { 
            rb.position = playerPos.position;
        }
        else {
            rb.position = (absRotation > 90 || absRotation < -90) ?
                new Vector2(playerPos.position.x + -0.5f, playerPos.position.y) :
                new Vector2(playerPos.position.x + 0.5f, playerPos.position.y);
        }
    }
}
