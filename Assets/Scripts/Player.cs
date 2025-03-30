using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    // Public Variables
    public float sprintSpeed = 7;
    public float walkSpeed = 4;
    public Transform targetTransform;
    public LayerMask mouseAimMask;

    // Internal Var
    private bool jumpKeyWasPressed = false;
    private int jumpCounter = 2;
    private bool sprintKeyHold = false;
    private float horizontalInput;

  
    private Camera mainCamera;
    private Rigidbody rigidbodyComponent;
    private Transform hand;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        hand = transform.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {

        // Aim
        mainCamera = Camera.main;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
            hand.LookAt(hit.point);
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            jumpKeyWasPressed = true;
            --jumpCounter;
        }

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            sprintKeyHold=true;
        }

        horizontalInput = Input.GetAxis("Horizontal");

    }

    // Fixed update is called once every physic update
    private void FixedUpdate()
    {
        // Horizontal Movement (sprint + walk)
        if (sprintKeyHold)
        {
            rigidbodyComponent.linearVelocity = new Vector3(horizontalInput * sprintSpeed, rigidbodyComponent.linearVelocity.y, 0);
            sprintKeyHold = false;
        }
        else
        {
            rigidbodyComponent.linearVelocity = new Vector3(horizontalInput * walkSpeed, rigidbodyComponent.linearVelocity.y, 0);
        }

        // Facing Rotation
        float mag = Mathf.Sign(targetTransform.position.x - transform.position.x);
        float y;
        if (mag >= 1) { y = 180; }
        else { y = 0; }
        rigidbodyComponent.MoveRotation(Quaternion.Euler(new Vector3(0, y, 0)));

        // Limit Jumps
        if (jumpCounter <= 0 && Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) // Expect collision with itself
        {
            return;
        }

        // Floor Collision
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length >= 1)
        {
            jumpCounter = 2;
        }

        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }
}
