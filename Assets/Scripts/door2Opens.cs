using UnityEngine;

public class door2Opens : MonoBehaviour
{
    private L8doorButton pressed; // Switched from L8 to L9
    private Rigidbody2D rb;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pressed = Object.FindAnyObjectByType<L8doorButton>(); // Updated to L9
    }

    void Update()
    {
        if (pressed != null && pressed.isPressed)
        {
            if (rb.transform.position.y < 8f)
            {
                rb.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
        }
        else if (rb.transform.position.y > 1.9683)
        {
            // NOTE: You're moving right here — is this intentional? You're checking `.y` but applying `Vector3.right`
            rb.transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
    }
}
