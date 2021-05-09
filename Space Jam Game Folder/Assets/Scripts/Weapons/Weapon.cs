using System.Collections;
using UnityEngine;
using Resources;

public class Weapon : MonoBehaviour
{
    public static Weapon I { get; private set; }

    [Header("References")]
    public GameObject bullet;
    
    [SerializeField] protected Transform gunTip;

    [Header("Config")]
    [SerializeField] private bool isAuto;

    [Header("Stats")]
    public int damage;

    [SerializeField] protected bool isShotgun;

    [SerializeField] protected int ammo;
    [SerializeField] protected float reloadSpeed;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float spread;

    [SerializeField] protected Vector3 bulletScale;

    protected SpriteRenderer spriteRenderer;

    protected bool isReloading;

    protected float lookAngle;
    protected float nextTimeToFire = 0f;

    protected int currentAmmo;

    private void OnEnable() 
    {
        isReloading = false;    
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentAmmo = ammo;
    }

    private void Update()
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

    protected void SingleShoot() 
    {
        // Instancia a bala e seta o dano
        Bullet currentBullet = Instantiate(bullet, gunTip.position, transform.rotation).GetComponent<Bullet>();
        currentBullet.damage = damage;
        currentBullet.speed = bulletSpeed;
        currentBullet.scale = bulletScale;

        // diminui a munição
        currentAmmo--;
    }

    protected void AutoShoot()
    {
        if (Time.time >= nextTimeToFire) {
            // pega a proxima vez que a arma podera atirar
            nextTimeToFire = Time.time + 1f / fireRate;

            // Instancia a bala e seta o dano
            Bullet currentBullet = Instantiate(bullet, gunTip.position, transform.rotation).GetComponent<Bullet>();
            currentBullet.damage = damage;
            currentBullet.speed = bulletSpeed;
            currentBullet.scale = bulletScale;

            // diminui a munição
            currentAmmo--;
        }
    }

    protected IEnumerator Reload() 
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

        spriteRenderer.flipY = lookAngle < 90 && lookAngle > -90 ? false : true;
    }
}
