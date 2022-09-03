using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Animator animator;
    public float speed = 5f;
    bool isGrounded;
    bool jumped;
    float jumpPower = 12f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        CheckIfGrounded(); 
        PlayerJump();
    }
    void FixedUpdate()
    {
        PlayerWalk();
    }
    void PlayerWalk()
    {
        float playerInput = Input.GetAxisRaw("Horizontal");
        if(playerInput > 0)
        {
            playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
            ChangeDirection(2);
        }
        else if(playerInput < 0)
        {
            playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
            ChangeDirection(-2);
        }
        else if(playerInput == 0)
        {
            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        }
        animator.SetInteger("Speed", Mathf.Abs( (int)playerRigidbody.velocity.x ) );
    }
    void ChangeDirection(int a)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = a;
        transform.localScale = tempScale;
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if(isGrounded)
        {
            if(jumped)
            {
                jumped = false;
                animator.SetBool("Jump", false);
            }
        }
    }
    void PlayerJump()
    {
        if(isGrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpPower);

                animator.SetBool("Jump", true);
            }
        }
    }
}
