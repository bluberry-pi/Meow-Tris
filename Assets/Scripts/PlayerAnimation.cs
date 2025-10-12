using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D cat;

    private bool isFacingRight = true;
    private float horizontal;

    private float meowTimer = 0f;
    private bool isIdleMeowing = false;
    private bool isMeowing = false;

    [SerializeField] private AudioClip[] meow;

    [Header("Ground Detection")]
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool isJumping = false;

    private bool isGrounded = false;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.F))
        {
            SoundManager.instance.PlayRandomSound(meow, transform, 1f);
            bool isIdle = !Input.GetKey(KeyCode.A) &&
              !Input.GetKey(KeyCode.D) &&
              !Input.GetKey(KeyCode.LeftArrow) &&
              !Input.GetKey(KeyCode.RightArrow) &&
              Mathf.Abs(cat.linearVelocityY) < 0.01f;


            if (isIdle)
            {
                animator.SetFloat("isIdleMeowing", 1);
                isIdleMeowing = true;
            }
            else
            {
                animator.SetFloat("isMeowing", 1);
                isMeowing = true;
            }

            meowTimer = 0.5f;
        }

        if (isIdleMeowing || isMeowing)
        {
            meowTimer -= Time.deltaTime;

            if (meowTimer <= 0f)
            {
                if (isIdleMeowing)
                {
                    animator.SetFloat("isIdleMeowing", 0);
                    isIdleMeowing = false;
                }
                if (isMeowing)
                {
                    animator.SetFloat("isMeowing", 0);
                    isMeowing = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        bool walkKey = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);
        animator.SetBool("isWalking", walkKey);

    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void CheckGrounded()
    {
        // Create a ContactFilter using the LayerMask
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(groundLayer);
        filter.useTriggers = true;

        // Allocate results array
        Collider2D[] results = new Collider2D[5];
        int count = groundCheck.Overlap(filter, results);

        isGrounded = count > 0;
        isJumping = !isGrounded;

        animator.SetBool("isJumping", isJumping);
    }

}
