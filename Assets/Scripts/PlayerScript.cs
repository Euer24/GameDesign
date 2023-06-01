using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool flipLeftAndRight;

    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;

    private bool isGroundedLeft;
    private bool isGroundedRight;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.D))
            Move(movingLeft: false);
        else if (Input.GetKey(KeyCode.A))
            Move(movingLeft: true);

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void Move(bool movingLeft)
    {
        if (movingLeft)
        {
            transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);

            if (flipLeftAndRight)
                sr.flipX = true;
            else
                sr.flipX = false;
        }
        else
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);

            if (flipLeftAndRight)
                sr.flipX = false;
            else
                sr.flipX = true;
        }
    }

    private void Jump()
    {
        if (!isGroundedLeft && !isGroundedRight)
            return;

        rb.velocity = Vector2.up * jumpVelocity;
    }

    private void FixedUpdate()
    {
        isGroundedLeft = Physics2D.Linecast(transform.position, groundCheckLeft.position, 1 << LayerMask.NameToLayer("Ground"));
        isGroundedRight = Physics2D.Linecast(transform.position, groundCheckRight.position, 1 << LayerMask.NameToLayer("Ground"));

        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Goal"))
            GameManager.instance.LoadNextLevel();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            GameManager.instance.Respawn();
    }
}
