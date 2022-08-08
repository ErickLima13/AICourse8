using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    private Vector3 velocity;

    public bool isGrounded;

    [SerializeField] private AudioClip[] m_FootstepSounds = null;
    [SerializeField] private AudioClip jumpSound = null;
    [SerializeField] private AudioClip landSound = null;

    private AudioSource audioSource;

    public bool useFootsteps = true;

    bool m_PreviouslyGrounded;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

       

        if(move.magnitude != 0 )
        {
            //PlayFootStepAudio();
        }

    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!m_PreviouslyGrounded && controller.isGrounded)
        {
            audioSource.clip = landSound;
            audioSource.Play();
        }
    
        m_PreviouslyGrounded = controller.isGrounded;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }

    private void PlayFootStepAudio()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, m_FootstepSounds.Length);
        audioSource.clip = m_FootstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = audioSource.clip;
    }

}
