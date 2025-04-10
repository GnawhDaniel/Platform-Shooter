using System;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    // Bullet
    [SerializeField] private GameObject bullet;

    // Bullet Force
    [SerializeField] private float shootForce, upwardForce;

    // Stats
    [SerializeField] private float timeBetweenShooting, spread;

    [SerializeField] private Slider healthUI;
    private float health = 1000f;

    // Reference
    [SerializeField] private Transform[] weapons;
    [SerializeField] private Enemy underling;


    // Graphics
    private GameObject muzzleFlash;

    private bool readyToShoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sourcePoint = transform;
        readyToShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (readyToShoot)
        {
            foreach (Transform weapon in weapons)
            {
                BulletWheelAttack(weapon);
            }
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Update the health bar UI
        if (healthUI != null)
        {
            healthUI.value = health / 1000f;
        }
    }

    private void FixedUpdate()
    {
        // Randomly spawn underlings
        if (UnityEngine.Random.Range(0, 1000) < 2)
        {
            SpawnUnderling();
        }
    }

    void SpawnUnderling()
    {
        // Choose random position in game world
        Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(1, 5), 0);

        // Spawn
        Enemy currentUnderling = Instantiate(underling, spawnPosition, Quaternion.identity);
        currentUnderling.SetTargetPlayer();
    }

    void BulletWheelAttack(Transform srcPt)
    {
        readyToShoot = false;

        // Create a bullet
        GameObject currentBullet = Instantiate(bullet, srcPt.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(srcPt.forward * shootForce, ForceMode.Impulse);

        Invoke("ResetShot", timeBetweenShooting);
    }
    public void ResetShot()
    {
        readyToShoot = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet hits an object with a rigidbody, destroy the bullet
        if (collision.gameObject.tag == "Projectile")
        {
            Debug.Log("hit boss");

            // If gameobject is Bullet
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Add damage to boss
                health -= bullet.damage;
                Debug.Log("Boss Health: " + health);
                if (health <= 0)
                {
                    UpdateHealthBar();
                    Destroy(gameObject);
                }
            }

            Destroy(collision.gameObject);

        }
    }
}
