using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb2d;

    public Animator playerAnim;
    [SerializeField] private LayerMask groundMask;
    [HideInInspector] public bool isPlayerStopped;

    private float moveInput;

    public float moveSpeed;

    public float jumpForce;

    private int moreJumps;

    [SerializeField] private bool onGround;

    private bool wasOnGround;

    private bool isJump;

    //ground circle collider
    private Collider2D[] colliders_1, colliders_2;

    //check feet touch 
    private float groundCheckRadius = 0.036f;

    //object position collider with Ground
    public Transform[] groundCheck; 

    //slope system
    public PhysicsMaterial2D noFriction, friction;
    public float slopeCheckDistance;
    private float slopeAngle;
    private bool onSlope;

    private bool isLandGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        InputSystem();
        checkGround();
        Animations();
        Slopes();
        Slide();
    }

    //slide
    [Header("Slide System ---------")]
    public Transform wallCheck;

    private bool isColliderWall;

    public float wallCheckDistance;

    [HideInInspector]public bool isSliding;
    [Header("velocity slide wall")]

    public float wallSlideSpeed;
    [Header("Wall slide speed")]

    public float wallJumpForce;

    private bool onSliding;

    private void FixedUpdate(){
        if(!onSliding)
        Move();
    }

    private void Move(){

        if(onSlope && !isJump){
            rb2d.gravityScale = 20f;
            if(rb2d.velocity.y < -2f){
                rb2d.velocity = new Vector2(moveInput * moveSpeed, -9f);
            }else{
                rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
            }
        }else{
            rb2d.gravityScale = 3f;
            rb2d.velocity = new Vector2(moveInput * moveSpeed, rb2d.velocity.y);
        }
    }

    IEnumerator jumpSlide(){
        SFXController.Instance.SFX("PlayerJump", 0.3f);
        transform.localScale = new Vector3(-moveInput,1f,1f);
        yield return new WaitForSeconds(0.3f);
        onSliding = false;
    }

    private void InputSystem(){

        if(isPlayerStopped){
            moveInput = 0f;
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");

        if(moveInput != 0f && !onSliding){
            transform.localScale = new Vector3(moveInput, 1f, 1f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && (onGround || (moreJumps < 1 && rb2d.velocity.y > 0))){
            moreJumps++;
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.Space) && isSliding){
            moreJumps = 1000;
            rb2d.velocity = Vector2.zero;
            rb2d.velocity = new Vector2(wallJumpForce * -moveInput, wallJumpForce);
            onSliding = true;
            StartCoroutine(jumpSlide());
        }
    }

    void checkGround(){
        colliders_1 = Physics2D.OverlapCircleAll(groundCheck[0].position, groundCheckRadius, groundMask);
        colliders_2 = Physics2D.OverlapCircleAll(groundCheck[1].position, groundCheckRadius, groundMask);

        if(onGround && !wasOnGround){
            isJump = false;
        };
        
        wasOnGround = onGround;

        if(colliders_1.Length>0 || colliders_2.Length>0){
            onGround = true;
            moreJumps = 0;
        }else{
            onGround = false;
        }
    }

    private void Jump(){
        isJump = true;
        rb2d.gravityScale = 3f;
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        SFXController.Instance.SFX("PlayerJump", 0.3f);
    }

    private void Slopes(){
        RaycastHit2D hitSlope = Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, groundMask);

        //Debug.DrawRay(transform.position, Vector2.down * slopeCheckDistance, Color.red);

        if(hitSlope && !isJump){
            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);

            print(slopeAngle);

            onSlope = slopeAngle != 0;

            if(onSlope && moveInput == 0){
                rb2d.sharedMaterial = friction;
            }else{
                rb2d.sharedMaterial = noFriction;
            }

            if(!isLandGround){
                SFXController.Instance.SFX("LandGround", 0.1f);
                isLandGround = true;
            }
        }else{
            rb2d.sharedMaterial = noFriction;
            isLandGround = false;
        }
    }

    private void Slide(){
        isColliderWall = Physics2D.Raycast(wallCheck.position, wallCheck.TransformDirection(Vector2.right), wallCheckDistance, groundMask);

        if(isColliderWall && !onGround && rb2d.velocity.y < 0 && moveInput != 0){
            isSliding = true;
        }else{
            isSliding = false;
        }

        if(isSliding && rb2d.velocity.y < -wallSlideSpeed){
            rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
        }

    }

    private void Animations(){
        playerAnim.SetFloat("SpeedX", Mathf.Abs(moveInput));
        playerAnim.SetFloat("SpeedY", rb2d.velocity.y);
        playerAnim.SetBool("onGround", onGround);
        playerAnim.SetBool("isSliding", isSliding);
    }

    // private void OnCollisionEnter2D(Collider2D other){
    //     if(other.gameObject.layer == 8){
    //         GameController.instance.RestartGame();
    //     }
    // }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 8){
            SceneManager.LoadScene("Game_Over");
            SFXController.Instance.SFX("Lose", 0.5f);
            // GameController.instance.RestartGame();
        }else if(other.gameObject.layer == 9){
            SceneManager.LoadScene("Win");
            SFXController.Instance.SFX("Win", 0.5f);
        }else if(other.gameObject.layer == 12){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.layer == 8){
    //         GameController.instance.RestartGame();
    //         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //     }
    // }
}
