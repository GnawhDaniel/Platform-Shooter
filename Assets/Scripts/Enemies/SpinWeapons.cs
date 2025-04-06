using UnityEngine;

public class SpinWeapons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, -15f * Time.fixedDeltaTime);
    }
}
