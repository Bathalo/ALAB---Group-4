using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce = 0.0f;
    [SerializeField] private float jumpForceThreshold = 15.0f;

    //public AudioSource walkAudioSource;      //IGNORE small case COMMENTS FOR NOW, THOSE ARE FOR FUTURE SFX AND PLAYER ANIMATION    <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    //public AudioSource jumpAudioSource;
    //public AudioSource landAudioSource;
    //public AudioSource flatAudioSource;
    //public AudioSource bounceAudioSource;

    public Rigidbody2D rb;
    private GatherInput gI;
    //private Animator anim;
    private Jump jumpScript;

    public bool grounded;
    public bool preJump = false;
    public PhysicsMaterial2D bounceMat, normalMat;
    public ContactFilter2D groundContactFilter;
    public Transform spawnPoint; // PLAYER SPAWN REF
    public float respawnDelay = 1f; // PLAYER RESPAWN DELAY
    private bool isAlive = true; // PLAYER HEALTH
    private bool isRespawning = false; // RESPAWNING CHECK

    private int direction = 1;
    private bool wasGrounded = false;
    private LevelManager levelManager; // LEVEL MANAGER REF

    //KNOCKBACK FORCE
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;

    public bool KnockFromRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gI = GetComponent<GatherInput>();
        //anim = GetComponent<Animator>();
        jumpScript = GetComponent<Jump>();
        levelManager = FindObjectOfType<LevelManager>();

        rb.sharedMaterial = normalMat;
    }

    private void FixedUpdate()
    {

        CheckStatus();
        SetMaterialForGroundedState(grounded); 
        PlayerJump();
        PlayerMove();

        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 20f);
        
        //KNOCKBACK FORCE, REFER TO Ball.cs FOR COLLISION
        if (KBCounter <= 0)
        {
            Flip(); //IF NOT HIT, CAN FLIP DIRECTION
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce,KBForce); //KNOCKBACK TO THE LEFT
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce,KBForce); //KNOCKBACK TO THE RIGHT
            }
            
            KBCounter -= Time.deltaTime;
        }
    }
    private void PlayerMove() // PLAYER MOVE
    {
        // if (jumpForce == 0.0f && grounded && gI.valueX != 0)
        // {
        //     rb.velocity = new Vector2(speed * gI.valueX, rb.velocity.y);

        //     // WALKING SFX LOGIC
        //     //if (Mathf.Abs(gI.valueX) > 0.1f)
        //     {
        //         //if (!walkAudioSource.isPlaying)
        //         {
        //             //walkAudioSource.Play();
        //         }
        //     }
        //     //else
        //     {
        //         //walkAudioSource.Stop();
        //     }
        // }
        // //else // STOPS THE WALK SOUND DURING JUMPS OR WHEN STANDING STILL 
        // {
        //     //walkAudioSource.Stop();
        // }
    }

    private void Flip() // PLAYER DIRECTION FLIP
    {
        // if (gI.valueX < 0 && direction > 0)  // CHECK IF MOVING LEFT BUT FACING RIGHT 
        // {
        //     transform.right = -transform.right;
        //     direction = -1;
        // }
        // else if (gI.valueX > 0 && direction < 0) // CHECK IF MOVING RIGHT BUT FACING LEFT
        // {
        //     transform.right = -transform.right;
        //     direction = 1;
        // }

        //CANNOT MOVE ANYMORE, CAN ONLY FLIP DIRECTION
        if (gI.valueX < 0)
        {
            transform.right = -Vector2.right; // Face left
        }
        else if (gI.valueX > 0)
        {   
            transform.right = Vector2.right; // Face right
        }
    }

    public bool IsGrounded => rb.IsTouching(groundContactFilter);

    private void PlayerJump() // PLAYER JUMP
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

            if (IsGrounded) // APPLY MOVEMENT ONLY WHEN GROUNDED
            {
                tempX = gI.valueX * speed;
            }

            else
            {
                tempX = 0.0f; // RESTRICT MOVEMENT WHILE MID-AIR
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) // TRAP COLLISION CHECK
        {
            isAlive = false; // SET PLAYER'S HP TO ZERO
        }
        else if (collision.gameObject.CompareTag("Goal")) // GOAL COLLISION CHECK
        {
            levelManager.LoadNextLevel(); // CALL LOADNEXTLEVEL METHOD FROM LEVELMANAGER SCRIPT
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Wall") && collision.gameObject.layer == 6)
        {
            //if (!bounceAudioSource.isPlaying) // PREVENT OVERLAPPING SFX
            {
                //bounceAudioSource.Play();
            }
        }
        else if (collision.gameObject.CompareTag("Pendulum"))
        {
            grounded = false;
        }

    }
    void Update()  // IS PLAYER FALLING CHECK
    {
        if (!isAlive && !isRespawning) //CHECK IF PLAYER'S HP IS ZERO AND NOT RESPAWING
        {
            StartCoroutine(Respawn());
        }

        if (rb.velocity.y < -14f && !grounded)
        {
            //anim.SetBool("isFalling", true);
            //if (!flatAudioSource.isPlaying && !landAudioSource.isPlaying)
            {
                //flatAudioSource.Play(); // PLAYS THE "FLAT AUDIO"
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
    private IEnumerator Respawn() // PLAYER RESPAWN
    {
        isRespawning = true;

        yield return new WaitForSeconds(respawnDelay); // PLAYER RESPAWN DELAY

        transform.position = spawnPoint.position; // REPSAWN PLAYER BACK TO SPAWN POINT

        isAlive = true; // RESET PLAYER'S HP BACK TO 1

        isRespawning = false;
    }
    private void SetMaterialForGroundedState(bool isGrounded)
    {
        if (isGrounded)
        {
            rb.sharedMaterial = normalMat; // PLAYER DEFAULT PHYSMAT
        }
        else
        {
            rb.sharedMaterial = bounceMat; // PLAYER BOUNCE PHYSMAT
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