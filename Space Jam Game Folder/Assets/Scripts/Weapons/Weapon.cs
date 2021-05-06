using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources;
using UnityEngine.PlayerLoop;

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
    [SerializeField] private int damage;

    private Rigidbody2D rb;

    private bool isReloading;

    private float lookAngle;
    private float nextTimeToFire = 0f;

    private int currentAmmo;

    private void OnEnable() 
    {
        isReloading = false;    
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentAmmo = ammo;
    }

    void Update()
    {
        HandleRotation();
        HandleWeapon();
    }

    private void HandleWeapon() 
    {
        if (isReloading) {
            return;
        }

        // Se a munição acabar inicia o reload automaticamente
        if (currentAmmo <= 0) {
            StartCoroutine(Reload());

            return;
        }

        if (InputManager.I.isReloadingKey && currentAmmo < ammo) {
            StartCoroutine(Reload());
        }

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
    }

    private void SingleShoot() 
    {
        Bullet currentBullet = Instantiate(bullet, gunTip.position, transform.rotation).GetComponent<Bullet>();
        currentBullet.damage = damage;

        currentAmmo--;
    }

    private void AutoShoot()
    {
        if (Time.time >= nextTimeToFire) {
            nextTimeToFire = Time.time + 1f / fireRate;

            Bullet currentBullet = Instantiate(bullet, gunTip.position, transform.rotation).GetComponent<Bullet>();
            currentBullet.damage = damage;

            currentAmmo--;
        }
    }

    private IEnumerator Reload() 
    {     
        isReloading = true;

        // Animation
        yield return new WaitForSeconds(reloadSpeed);

        isReloading = false;

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
