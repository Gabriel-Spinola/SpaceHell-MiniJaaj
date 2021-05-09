using Resources;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    public static EnemyWeapon I { get; private set; }

    public bool canShoot;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        currentAmmo = ammo;
    }

    // Update is called once per frame
    private void Update()
    {
        // Se a munição acabar inicia o reload automaticamente
        if (currentAmmo <= 0) {
            StartCoroutine(Reload());

            return;
        }

        if (canShoot && currentAmmo >= 1) {
            HandleRotation();

            AutoShoot();
        }
    }

    private void HandleRotation()
    {
        if (Enemy.I.player != null) { 
            lookAngle = StaticRes.TargetLookDir2D(
                self: transform.position,
                target: Enemy.I.player.transform.position
            );

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, lookAngle));  
        }

        spriteRenderer.flipY = lookAngle < 90 && lookAngle > -90 ? false : true;
    }
}
