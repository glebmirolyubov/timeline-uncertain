/*
 * Script written by Gleb Mirolyubov, student ID: 150330867
 * 
 * ECS657U Multi-Platform Game Development Assignment
 * 
 */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 0.8f;
    [SerializeField] private AudioClip jumpSound; 
    [SerializeField] private AudioClip landSound;

    private Rigidbody playerRigidbody;
    private AudioSource audioSource;
    private Vector3 playerInput = Vector3.zero;
    private Vector3 playerVelocity = Vector3.zero;
    private Vector3 playerRotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    private bool isGrounded;
    private bool isCrouched;
    private bool isSprinting;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        isCrouched = false;
        isSprinting = false;
        Application.targetFrameRate = 300;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

        Vector3 horizontalMovement = transform.right * playerInput.x;
        Vector3 verticalMovement = transform.forward * playerInput.y;

        playerVelocity = (horizontalMovement + verticalMovement) * speed;

        float verticalRotation = Input.GetAxisRaw("Mouse X");

        playerRotation = new Vector3(0f, verticalRotation, 0f) * lookSensitivity;

        float horizontalRotation = Input.GetAxisRaw("Mouse Y");

        cameraRotationX = horizontalRotation * lookSensitivity;

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched)
        {
            PerformJump();
        }

        if (Input.GetButtonDown("Jump") && isCrouched)
        {
            UnCrouch();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouched)
            {
                Crouch();
            } else if (isCrouched)
            {
                UnCrouch();
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Sprint();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprint();
        }

        PerformRotation();
    }

    void FixedUpdate()
    {
        PerformMovement();
    }

    void PerformMovement()
    {
        if (playerVelocity != Vector3.zero)
        {
            playerAnimator.SetBool("Run", true);  
            playerRigidbody.MovePosition(playerRigidbody.position + playerVelocity * Time.fixedDeltaTime);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
        }
    }

    void PerformRotation()
    {
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(playerRotation));

        if (playerCamera != null)
        {
            // Set our rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -85f, 70f);

            //Apply our rotation to the transform of our camera
            playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

    void PerformJump()
    {
        if (playerVelocity == Vector3.zero)
        {
            playerAnimator.SetTrigger("Jump");
        }
        Vector3 upVelocity = playerRigidbody.velocity;
        upVelocity.y = 6f;
        playerRigidbody.velocity = upVelocity;
        PlayJumpSound();
        isGrounded = false;
    }

    void Crouch()
    {
        playerAnimator.SetBool("Crouch", true);
        isCrouched = true;
        playerCollider.height = 1.3f;
        playerCollider.center = new Vector3(0f,-0.2f,0.3f);
        speed = 2f;
    }

    void UnCrouch()
    {
        playerAnimator.SetBool("Crouch", false);
        isCrouched = false;
        playerCollider.height = 1.7f;
        playerCollider.center = new Vector3(0f, 0f, 0.1f);
        speed = 5f;
    }

    void Sprint()
    {
        if (isCrouched)
            UnCrouch();

        isSprinting = true;
        speed = 8f;
        playerCollider.height = 1.7f;
        playerCollider.center = new Vector3(0f, 0f, 0.3f);
        playerAnimator.SetBool("Sprint", true);
    }

    void StopSprint()
    {
        isSprinting = false;
        speed = 5f;
        playerCollider.height = 1.7f;
        playerCollider.center = new Vector3(0f, 0f, 0.1f);
        playerAnimator.SetBool("Sprint", false);
    }

    private void PlayJumpSound()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    private void PlayLandingSound()
    {
        audioSource.clip = landSound;
        audioSource.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
            //PlayLandingSound();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.tag == "Platform")
        //{
         //   isGrounded = true;
        //}
    }
}