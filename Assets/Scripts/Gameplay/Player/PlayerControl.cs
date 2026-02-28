using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;

    public Transform cameraTransform;
    public GameObject player;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;
    private float defaultMoveSpeed;
    private Vector3 defaultScale;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        defaultMoveSpeed = moveSpeed;
        defaultScale = player.transform.localScale;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
        rb.linearVelocity = velocity;
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnEnable()
    {
        InputManager.OnSpacePressedSpace += ReactToSpace;
        InputManager.OnSpacePressedLeftShift += ReactToLeftShift;
        InputManager.OnSpacePressedLeftControl += ReactToLeftControl;

        InputManager.OnLeftControlReleased += ReactToLeftShiftReleased;
        InputManager.OnLeftShiftReleased += ReactToLeftControlReleased;
    }

    private void OnDisable()
    {
        InputManager.OnSpacePressedSpace -= ReactToSpace;
        InputManager.OnSpacePressedLeftShift -= ReactToLeftShift;
        InputManager.OnSpacePressedLeftControl -= ReactToLeftControl;

        InputManager.OnLeftControlReleased -= ReactToLeftShiftReleased;
        InputManager.OnLeftShiftReleased -= ReactToLeftControlReleased;
    }

    void ReactToSpace()
    {
        if(isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ReactToLeftShift()
    {
        if(isGrounded){
            Vector3 scale = defaultScale;
            scale.y /= 2f;
            player.transform.localScale = scale;

            moveSpeed = defaultMoveSpeed / 2f;
        }
    }

    void ReactToLeftControl()
    {
        if(isGrounded){
            Vector3 scale = defaultScale;
            scale.y /= 3f;
            player.transform.localScale = scale;

            moveSpeed = defaultMoveSpeed / 3f;
        }
    }

    void ReactToLeftShiftReleased() 
    {
        player.transform.localScale = defaultScale;

        moveSpeed = defaultMoveSpeed;
    }

    void ReactToLeftControlReleased() 
    {
        player.transform.localScale = defaultScale;

        moveSpeed = defaultMoveSpeed;
    }
}
        


