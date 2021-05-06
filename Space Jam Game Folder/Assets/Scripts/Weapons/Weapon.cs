using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;

    [SerializeField] private Transform gunTip;
    [SerializeField] private Transform playerPos;

    private Rigidbody2D rb;

    private float lookAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lookAngle = StaticRes.LookDir(transform.position);

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
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));

        transform.localScale = lookAngle < 90 && lookAngle > -90 ?
            new Vector3(1f, 1f, 1f) :
            new Vector3(-1f, -1f, 1f)
        ;
    }
}
