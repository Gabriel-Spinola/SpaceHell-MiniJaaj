using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D playerPos;

    private Rigidbody2D rb;

    private Vector2 mousePos;
    private Vector2 lookDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        lookDir = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.position = (rb.rotation > 90 || rb.rotation < -90) ?
            new Vector2(playerPos.position.x + -0.5f, playerPos.position.y) :
            new Vector2(playerPos.position.x + 0.5f, playerPos.position.y);
    }

    private void FixedUpdate() {
        
    }
}
