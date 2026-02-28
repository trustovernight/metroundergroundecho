using MetroUndergroundEcho.Core;
using MetroUndergroundEcho.Core.Sound;
using UnityEngine;

namespace MetroUndergroundEcho.Gameplay
{
    [RequireComponent(typeof(PlayerAudio))]
    public class Player : MonoBehaviour, ISoundProducer
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] [Min(0f)] private float stepVolume = 6f;
        [SerializeField] [Min(0f)] private float landingVolume = 10f;

        private PlayerAudio playerAudio;
        private Rigidbody rb;
        private float xRotation = 0f;
        private bool isGrounded;
        private float defaultMoveSpeed;
        private Vector3 defaultScale;
        private bool canProduceSound = true;
        private bool isInAir = false;

        public Transform cameraTransform;
        public GameObject player;

        private void Start()
        {
            rb = player.GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            defaultMoveSpeed = moveSpeed;
            defaultScale = player.transform.localScale;

            playerAudio = GetComponent<PlayerAudio>();
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            Vector3 velocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
            rb.linearVelocity = velocity;

            if (canProduceSound && moveX != 0 || moveZ != 0)
            {
                ProduceSound();
                playerAudio.PlayFootstep();
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if(!isInAir)
            {
                isInAir = false;
                ProduceLandingSound();
                playerAudio.PlayLanding();
            }

            isGrounded = true;
        }

        private void OnCollisionExit(Collision collision)
        {
            isGrounded = false;
            isInAir = true;
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
        
        private void ReactToSpace()
        {
            if(isGrounded){
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void ReactToLeftShift()
        {
            if(isGrounded){
                Vector3 scale = defaultScale;
                scale.y /= 2f;
                player.transform.localScale = scale;

                moveSpeed = defaultMoveSpeed / 2f;
                canProduceSound = false;
            }
        }

        private void ReactToLeftControl()
        {
            if(isGrounded){
                Vector3 scale = defaultScale;
                scale.y /= 3f;
                player.transform.localScale = scale;

                moveSpeed = defaultMoveSpeed / 3f;
                canProduceSound = false;
            }
        }

        private void ReactToLeftShiftReleased() 
        {
            player.transform.localScale = defaultScale;

            moveSpeed = defaultMoveSpeed;

            canProduceSound = true;
        }

        private void ReactToLeftControlReleased() 
        {
            player.transform.localScale = defaultScale;

            moveSpeed = defaultMoveSpeed;
            
            canProduceSound = true;
        }

        public void ProduceSound()
        {
            ProduceSound(stepVolume);
        }

        private void ProduceSound(float volume)
        {
            SoundManager.OnSoundProduced(new SoundProducedEvent(transform.position, volume));
        }

        private void ProduceLandingSound()
        {
            ProduceSound(landingVolume);
        }
    }
}