using UnityEngine;

public class PlayerMovement2D : MonoBehaviour {

    public float speed;
    private Rigidbody2D rb;
    private float moveInput;

    public float jumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float circleRadius;
    public LayerMask whatIsGround;
    public Collider2D col;

    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
    void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if(moveInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private void Update() {
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);
        isGrounded = Physics2D.IsTouchingLayers(col, whatIsGround);
        if(isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true) {
            if(jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }

    }
}
