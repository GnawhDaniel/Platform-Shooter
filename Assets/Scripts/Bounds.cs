using UnityEngine;

public class Bounds : MonoBehaviour
{
    // References
    [SerializeField] Transform playerLoc;

    // Update is called once per frame
    void Update()
    {
        if (playerLoc && playerLoc.position.y < -10)
        {
            playerLoc.position = new Vector3(0, 10, 0);
        }
    }
}
