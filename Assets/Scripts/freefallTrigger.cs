using UnityEngine;

public class freefallTrigger : MonoBehaviour
{
    private TetronimoMovement block;
    void OnTriggerEnter2D(Collider2D collision)
    {
        block = collision.GetComponent<TetronimoMovement>();
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (block != null && rb != null)
        {
            block.fall = false;
            rb.gravityScale = 1f;
            rb.constraints = RigidbodyConstraints2D.None;

            Debug.Log("Tetronimo entered freefall zone: physics enabled.");
        }
    }
}
