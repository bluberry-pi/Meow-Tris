using UnityEngine;

public class TetronimoMovement : MonoBehaviour
{
    public float speed = 10f;
    public float fallInterval = 1f;
    public float minFallInterval = 0.1f;
    public float maxFallInterval = 2f;
    public float fallStep = 0.5f;
    public float scrollSensitivity = 0.1f;

    private Camera cam;
    private float fallTimer = 0f;
    private Rigidbody2D rb;
    public bool fall = true;
    public float deadZone = -6.8f;
    public bool enableMouseHorizontal = true;
    [SerializeField] private AudioClip tetfall;
    [SerializeField] private AudioClip tetplace;
    [SerializeField] private AudioClip tetded;
    private bool hasLanded = false;

    // Static flag to make sure timer starts only once
    private static bool isFirstPlacement = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;
        if (fall == true)
        {
            if (enableMouseHorizontal == true)
            {
                HandleHorizontalMovement();
            }
            HandleFall();
            HandleScrollWheel();
            HandleRotation();
        }

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void HandleHorizontalMovement()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = new Vector3(mouseWorldPos.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    void HandleFall()
    {
        fallTimer = fallTimer + Time.deltaTime;
        if (fallTimer >= fallInterval)
        {
            transform.position = transform.position + Vector3.down * fallStep;
            SoundManager.instance.PlaySound(tetfall, transform, 1f);
            fallTimer = 0f;
        }
    }

    void HandleScrollWheel()
    {
        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0)
        {
            fallInterval = fallInterval + scroll * scrollSensitivity;
            fallInterval = Mathf.Clamp(fallInterval, minFallInterval, maxFallInterval);
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.Rotate(0, 0, 90f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLanded) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Tetronimoes"))
        {
            hasLanded = true;
            fall = false;
            SoundManager.instance.PlaySound(tetplace, transform, 1f);
            rb.bodyType = RigidbodyType2D.Static;

            // Enqueue only once
            Timer.fallenTetronimos.Enqueue(this);

            if (isFirstPlacement)
            {
                Timer.StartTimer();
                isFirstPlacement = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            Destroy(gameObject);
            SoundManager.instance.PlaySound(tetded, transform, 1f);
        }
    }

    public static void ResetFirstPlacement()
{
    isFirstPlacement = true;
}

}
