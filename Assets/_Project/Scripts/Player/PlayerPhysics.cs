using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Audios")]
    [SerializeField] private AudioClip[] footstepSounds = null;
    [SerializeField] private AudioClip jumpSound = null;
    [SerializeField] private AudioClip landSound = null;
    [SerializeField] private AudioClip breathingSound;

    [SerializeField] private SoundController soundController;

    private float timer = 0.5f;
    private float count;

    private Vector3 velocity;

    private bool previouslyGrounded;

    public CharacterController controller;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float speed = 3f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 1f;
    public float timerBreath = 7f;

    public bool isGrounded;

    private void Initialization()
    {
        count = timer;
    }

    private void Start()
    {
        Initialization();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        timer -= Time.deltaTime;

        timerBreath -= Time.deltaTime;

        if (move.magnitude == 0 && controller.isGrounded && timerBreath <= 0)
        {
            soundController.PlaySound(breathingSound);
            timerBreath = 7f;
        }

        if (timer <= 0 && move.magnitude != 0)
        {
            PlayFootStepAudio();
            timer = count;
        }
    }

    public void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!previouslyGrounded && controller.isGrounded)
        {
            soundController.PlaySound(landSound);
        }

        previouslyGrounded = controller.isGrounded;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            soundController.PlaySound(jumpSound);
        }
    }

    private void PlayFootStepAudio()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        int n = Random.Range(1, footstepSounds.Length);
        soundController.audioSource.clip = footstepSounds[n];
        soundController.audioSource.PlayOneShot(soundController.audioSource.clip);
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = soundController.audioSource.clip;
    }
}
