using UnityEngine;

public class Enemy : ProjectileGun
{
    private Transform hand; 
    private double health = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform player = GameObject.Find("Player").transform;

        hand = transform.Find("Hand");
        readyToShoot = true;
        bulletsLeft = magazineSize;
        reloading = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAt();
        RaycastHit hit;
        Vector3 direction = (attackPoint.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            if (hit.transform.name == "Player")
            {
                shooting = true;
            }
            else
            {
                shooting = false;
            }
        }
        EnemyInput();
    }

    private void EnemyInput()
    {
        // Reloading
        if (readyToShoot && !reloading && bulletsLeft <= 0) Reload();

        // Shooting 
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    } 

    private void LookAt()
    {
        Vector3 target = attackPoint.position;
        if (target != null)
        {
            hand.LookAt(target);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTargetPlayer()
    {
        Transform player = GameObject.Find("Player").transform;
        attackPoint = player;
    }
}
