using UnityEngine;

public class door1Opens : MonoBehaviour
{
    private L8doorButton pressed; // changed from L8doorButton
    private Rigidbody2D rb;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pressed = Object.FindAnyObjectByType<L8doorButton>(); // changed to find L9doorButton
    }

    void Update()
    {
        if (pressed != null && pressed.isPressed)
        {
            if (rb.transform.position.x > -11)
            {
                rb.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
        else if (rb.transform.position.x < -6.4561)
        {
            rb.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }
}
