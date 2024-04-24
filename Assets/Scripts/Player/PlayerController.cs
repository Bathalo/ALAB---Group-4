using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce = 0.0f;
    [SerializeField] private float jumpForceThreshold = 15.0f;
    //[SerializeField] private Camera cam;

    //public AudioSource walkAudioSource;
    //public AudioSource jumpAudioSource;
    //public AudioSource landAudioSource;
    //public AudioSource flatAudioSource;
    //public AudioSource bounceAudioSource;

    private Rigidbody2D rb;
    private GatherInput gI;
    //private Animator anim;
    private Jump jumpScript;

    public bool grounded;
    public bool preJump = false;
    public PhysicsMaterial2D bounceMat, normalMat;
    public ContactFilter2D groundContactFilter;

    private int direction = 1;
    private bool wasGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gI = GetComponent<GatherInput>();
        //anim = GetComponent<Animator>();
        jumpScript = GetComponent<Jump>();

        rb.sharedMaterial = normalMat;
    }

    private void FixedUpdate()
    {
        Flip();
        CheckStatus();
        SetMaterialForGroundedState(grounded); 
        PlayerJump();
        PlayerMove();

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 20f);

        //if (cam != null)
        {
            //cam.transform.position = new Vector3(cam.transform.position.x, Mathf.Lerp
                //(cam.transform.position.y, transform.position.y + 3, 0.01f), -10);
        }
    }

    private void PlayerMove()
    {
        if (jumpForce == 0.0f && grounded && gI.valueX != 0)
        {
            rb.velocity = new Vector2(speed * gI.valueX, rb.velocity.y);

            // Walking Sound Logic
            if (Mathf.Abs(gI.valueX) > 0.1f)
            {
                //if (!walkAudioSource.isPlaying)
                {
                    //walkAudioSource.Play();
                }
            }
            else
            {
                //walkAudioSource.Stop();
            }
        }
        else // Stop the walk sound during jumps or when standing still
        {
            //walkAudioSource.Stop();
        }
    }

    private void Flip() // Player Flip 
    {
        if (gI.valueX < 0 && direction > 0)  // Check if moving left but facing right
        {
            transform.right = -transform.right;
            direction = -1;
        }
        else if (gI.valueX > 0 && direction < 0) // Check if moving right but facing left
        {
            transform.right = -transform.right;
            direction = 1;
        }
    }

    public bool IsGrounded => rb.IsTouching(groundContactFilter);

    private void PlayerJump() // Player Jump
    {
        if (gI.jumpInput && IsGrounded)
        {
            jumpForce += 0.5f;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
            preJump = true;

            //anim.SetBool("isJumpCharging", true);
            //anim.SetBool("isJumping", false);
        }

        else
        {
            preJump = false;
            //anim.SetBool("isJumpCharging", false);
        }

        if (gI.jumpInput && IsGrounded && jumpForce >= jumpForceThreshold || gI.jumpInput == false && jumpForce >= 0.1f)
        {
            float tempX;

            if (IsGrounded) // Apply sideways force only if grounded
            {
                tempX = gI.valueX * speed;
            }

            else
            {
                tempX = 0.0f; // No sideways force in the air
            }

            float tempY = jumpForce;
            rb.velocity = new Vector2(tempX, tempY);
            Invoke("ResetJump", 0.025f);
            //anim.SetBool("isJumping", true);
            //anim.SetBool("isJumpCharging", false);
            //jumpAudioSource.Play();
        }

        if (rb.velocity.y <= -1)
        {
            rb.sharedMaterial = normalMat;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && collision.gameObject.layer == 6)
        {
            //if (!bounceAudioSource.isPlaying) // Prevent overlapping sounds
            {
                //bounceAudioSource.Play();
            }
        }
    }
    void Update()  // Add for checking falling  
    {
        if (rb.velocity.y < -14f && !grounded)
        {
            //anim.SetBool("isFalling", true);
            //if (!flatAudioSource.isPlaying && !landAudioSource.isPlaying)
            {
                //flatAudioSource.Play(); // Play the "flat" audio
            }
        }
        else
        {
            //anim.SetBool("isFalling", false);

            if (IsGrounded)
            {
                //anim.SetBool("isWalking", Mathf.Abs(gI.valueX) > 0.1f);

                //if (!wasGrounded && !landAudioSource.isPlaying && !flatAudioSource.isPlaying)
                {
                    //landAudioSource.Play();
                }

                wasGrounded = true;
            }
            else
            {

                //anim.SetBool("isWalking", false);
                wasGrounded = false;
            }
        }
    }
    private void SetMaterialForGroundedState(bool isGrounded)
    {
        if (isGrounded)
        {
            rb.sharedMaterial = normalMat; // Default Physics Material
        }
        else
        {
            rb.sharedMaterial = bounceMat; // Bounce Physics Material
        }
    }

    private void CheckStatus()
    {
        grounded = rb.IsTouching(groundContactFilter);
        SetMaterialForGroundedState(grounded);
    }
    private void ResetJump()
    {
        jumpForce = 0.0f;
    }
}