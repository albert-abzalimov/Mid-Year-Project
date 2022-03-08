using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(playerStats))]
public class playerMovement : MonoBehaviour
{
    public static playerMovement instance;
    #region Variables
    playerStats stats;
    Rigidbody2D rb;
    int horizontalInput;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkRadius;
    public float checkRadiusWidth;
    bool grounded;
    private Animator anim;
    public float lenKB, ForceKB;
    private float CountKB;
    public float bouncy;
    private bool flipped;
    // Dash thingys
    private bool dashing;

    // Fall Detector
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public powerUpSlot slot;

    float _jumpDelay;
    bool jumped = false;

    private Coroutine dashCopy;
    private void Awake()
    {
        instance = this;
    }

    #endregion


    void Start(){
        stats = GetComponent<playerStats>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        stats.canDash = true;
        stats.canJump = true;
        stats.canMove = true;
        _jumpDelay = stats.jumpDelay;
    }
    void Update(){
        // reload the scene when R is pressed
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        // if we have jumped in any way start the timer
        if (jumped){
            _jumpDelay -= Time.deltaTime;
        }
        // once the timer ends, then we can jump again
        if (_jumpDelay <= 0f){
            jumped = false;
            _jumpDelay = stats.jumpDelay;
        }
        // Flip player model
            if(rb.velocity.x < -0.1f && !flipped)
            {
                flipped = true;
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            } else if (rb.velocity.x > 0.1f && flipped)
            {
                flipped = false;
                Vector3 scale = transform.localScale;
                scale.x *= -1f;
                transform.localScale = scale;
            }

        if (CountKB <= 0)
        {
                    // get the input from A D which returns either -1 , 0 , or 1
            horizontalInput = (int)Input.GetAxisRaw("Horizontal");
            // check for ground
            grounded = Physics2D.OverlapBox(groundCheck.position , new Vector2(checkRadiusWidth, checkRadius) , 0, groundLayer);

            // if jump input "Spacebar" and is grounded (touching the ground) then jump using jumpForce

            if (Input.GetButtonDown("Jump") && grounded && stats.canJump && !jumped){
                rb.velocity += Vector2.up * stats.jumpForce;
                jumped = true;
            }
            else if (Input.GetButtonDown("Jump") && stats.canJump && stats.doulbleJumps > 0 && !jumped){
                rb.velocity += Vector2.up * stats.doubleJumpForce;
                stats.doulbleJumps--;
                jumped = true;
        }
            if (rb.velocity.y < 0){
                rb.velocity += Vector2.up * Physics2D.gravity.y * (stats.fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump")){
                rb.velocity += Vector2.up * Physics2D.gravity.y * (stats.lowJumpMultiplier - 1) * Time.deltaTime;
            }
            
            // dashing
            if (Input.GetKeyDown(KeyCode.LeftShift) && stats.canDash){
                if (horizontalInput != 0){
                    dashCopy = StartCoroutine(Dash());
                }
            }
        } 
        else
        {
            CountKB -= Time.deltaTime;
            if(rb.velocity.x > 0.1f)
            {
                rb.velocity = new Vector2(ForceKB, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(-ForceKB, rb.velocity.y);
            }
        }





        anim.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("dashing", dashing);
    }

    public void KB()
    {
        CountKB = lenKB;
        rb.velocity = new Vector2(0f, ForceKB);
    }

    public void Bounce()
    {
        rb.velocity = new Vector2(rb.velocity.x, bouncy);
    }

    private IEnumerator Dash(){
        dashing = true;
        stats.canJump = false;
        stats.canMove = false;
        stats.canDash = false;
        jumped = false;
        _jumpDelay = stats.jumpDelay;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        rb.velocity = new Vector2(horizontalInput * stats.dashSpeed * 10, rb.velocity.y);
        // TODO: Some effect
        // - cam shake
        // - particles
        // - sound
        yield return new WaitForSeconds(stats.dashTime);
        StartCoroutine(FinishedDashing());
    }
    private IEnumerator FinishedDashing(){
        stats.canJump = true;
        stats.canMove = true;
        dashing = false;
        jumped = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(stats.dashDelay);
        stats.canDash = true;
    }

    private void FixedUpdate()
    {        
        // set the rigidbody's velocity according to the input and speed while keeping y axis same
        // the * 10 is to make thigns easier cuz its way too slow
        if (stats.canMove){
            rb.velocity = new Vector2(horizontalInput * stats.speed * Time.deltaTime * 10, rb.velocity.y);   
        }

        // Moving the fall detector
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    // Checks if player collides with anything
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if player collided with the fall detector
        if(collision.tag == "FallDetector")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag != "Player" && dashing){
            StopCoroutine(dashCopy);
            StartCoroutine(FinishedDashing());
        }
    }
    

    // draw the groundCheck sphere in the editor to help visualize
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(checkRadiusWidth, checkRadius));
    }
}
