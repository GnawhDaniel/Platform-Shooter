using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] public float damage = 10f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // If the bullet has traveled the max distance, destroy the bullet
        if (Vector3.Distance(startPos, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet hits an object with a rigidbody, destroy the bullet
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            Destroy(gameObject);
            // If gameobject is Player
            if (collision.gameObject.name == "Player")
            {
                // Add damage to player
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamge(damage);
                }
            }
            // If gameobject is Enemy
            else if (collision.gameObject.tag == "Enemy")
            {
                // Add damage to enemy
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        // If bullet hits a box collider, destroy the bullet
        if (collision.gameObject.GetComponent<BoxCollider>())
        {
            Destroy(gameObject);
        }
    }
}
