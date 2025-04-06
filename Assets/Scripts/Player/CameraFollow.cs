using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5f;
    public Transform target;


    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y + 3.5f, -7.5f);
            transform.position = newPos;
        }
    }
}
