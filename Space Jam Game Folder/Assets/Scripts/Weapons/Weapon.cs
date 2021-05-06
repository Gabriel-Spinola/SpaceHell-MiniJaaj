using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    public GameObject bullet;
    
    [SerializeField] private Transform gunTip;

    [Header("Config")]
    [SerializeField] private bool isAuto;

    [Header("Stats")]
    [SerializeField] private int ammo;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private float fireRate;

    private Rigidbody2D rb;

    private float lookAngle;
    private float nextTimeToFire = 0f;

    private int currentAmmo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentAmmo = ammo;
    }

    void Update()
    {
        HandleRotation();

        if (isAuto) {
            if (InputManager.I.isShootingKeyPressed && currentAmmo >= 1) {
                AutoShoot();
            }
        }
        else {
            if (InputManager.I.isShootingKeyDown && currentAmmo >= 1) {
                SingleShoot();
            }
        }

        if (InputManager.I.isReloadingKey) {
            StartCoroutine(Reload());
        }
    }

    private void SingleShoot() 
    {
        Instantiate(bullet, gunTip.position, transform.rotation);

        currentAmmo--;
    }

    private void AutoShoot()
    {
        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / fireRate;

            Instantiate(bullet, gunTip.position, transform.rotation);
            currentAmmo--;
        }
    }

    private IEnumerator Reload() 
    {
        if (currentAmmo == ammo) {
            yield return null;
        }
        
        // Animation
        yield return new WaitForSeconds(reloadSpeed);

        currentAmmo = ammo;
    }

    private void HandleRotation() 
    {
        lookAngle = StaticRes.LookDir(transform.position);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));

        transform.localScale = lookAngle < 90 && lookAngle > -90 ?
            new Vector3(1f, 1f, 1f) :
            new Vector3(-1f, -1f, 1f)
        ;
    }
}
