using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Health & Damage")]
    private int maxHealth = 100;
    public int currentHealth;

    [Header("Player Movement & Gravity")]
    public float movementSpeed = 5f;
    public float jumpForce = 2f;

    private CharacterController controller;
    public float gravity = -9.81f;

    public Transform groundcheck;
    public LayerMask groundMask;

    public float groundDistance = 0.4f;
    private bool isGrounded;
    private Vector3 velocity;

    [Header("Player Movement & Gravity")]
    public AudioSource leftFootAudioSource;
    public AudioSource rightFootAudioSource;
    public AudioClip[] footstepSounds;

    public float footstepInterval = 0.5f;
    private float nextFootstepTime;
    private bool isLeftFootstep = true;

    void Start()
    {
        currentHealth = maxHealth;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0 )
        {
            velocity.y = -2f;
        }
        HandleMovement();
        HandelGravity();

        if(isGrounded && controller.velocity.magnitude > 0.1f && Time.time >= nextFootstepTime )
        {
            PlayerFootstepSound();
            nextFootstepTime = Time.time + footstepInterval;

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
        movement.y = 0;

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }

    void HandelGravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    void PlayerFootstepSound()
    {
        AudioClip footstepClip = footstepSounds[Random.Range(0, footstepSounds.Length)];

        if(isLeftFootstep)
        {
            leftFootAudioSource.PlayOneShot(footstepClip);
        }
        else
        {
            rightFootAudioSource.PlayOneShot(footstepClip);
        }

        isLeftFootstep = !isLeftFootstep;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
    }
}
