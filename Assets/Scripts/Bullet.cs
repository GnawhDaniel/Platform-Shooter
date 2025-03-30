using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float maxDistance = 8f;
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
        }

        // If bullet hits a box collider, destroy the bullet
        if (collision.gameObject.GetComponent<BoxCollider>())
        {
            Destroy(gameObject);
        }
    }
}
