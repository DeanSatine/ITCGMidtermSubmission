using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private float sensitivity;

    [SerializeField]
    private float speed, walk, run, jumpHeight;

    private Vector3 playerVelocity;
    private bool isGrounded;

    public bool isMoving, isRunning;

    private float xRotation, yRotation;

    private void Start()
    {
        speed = walk;
        cc = GetComponent<CharacterController>();
        cc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = cc.isGrounded;

        // Reset the downward velocity when the player is on the ground
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        // Handle player rotation
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0.0f);

        // Get player movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;

        // Move
        cc.SimpleMove((forward + right) * speed);

        // Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run;
            isRunning = true;
        }
        else
        {
            speed = walk;
            isRunning = false;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        // Apply gravity 
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);

        // Check if player is moving
        isMoving = cc.velocity.sqrMagnitude > 0.0f;
    }
}

    