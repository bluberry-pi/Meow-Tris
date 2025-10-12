using UnityEngine;

public class DynamicTetronimoMovement : MonoBehaviour
{
    [Header("Fall Settings")]
    public float fallSpeed = 1f;
    public float minFallSpeed = 0.1f;
    public float maxFallSpeed = 5f;
    public float scrollSensitivity = 0.5f;

    [Header("Mouse Control")]
    public float mouseFollowSpeed = 10f;
    public float rotationAmount = 90f;
    public float deadZone = -6.8f;


    public bool fall = true; // <- PUBLIC for SpawnBlocks to read

    private Rigidbody2D rb;
    private Camera cam;
    private bool isColliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (!fall) return;

        // Adjust fall speed via scroll
        float scroll = Input.mouseScrollDelta.y;
        fallSpeed -= scroll * scrollSensitivity;
        fallSpeed = Mathf.Clamp(fallSpeed, minFallSpeed, maxFallSpeed);

        // Move only in X axis based on mouse position
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float targetX = mousePos.x;
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, 0), Time.deltaTime * mouseFollowSpeed);

        // Rotate on left click
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(0, 0, -rotationAmount);
        }

        // Stop falling when right clicked while colliding
        if (isColliding && Input.GetMouseButtonDown(1))
        {
            fall = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
        
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (fall)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            Destroy(gameObject);
        }
    }
}