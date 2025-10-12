using UnityEngine;

public class KysObject : MonoBehaviour
{
    public float upperYLimit = 2.638f;
    public float lowerYLimit = -2.638f;
    public float moveSpeed = 2f;

    private bool movingDown = true;
    private float zRotationStep = 25f;
    private float rotationTimer = 0f;
    public float rotationInterval = 0.5f;

    void Update()
    {
        Vector3 pos = transform.position;

        float direction = movingDown ? -1f : 1f;
        pos.y += direction * moveSpeed * Time.deltaTime;
        transform.position = pos;

        if (pos.y <= lowerYLimit)
        {
            movingDown = false;
        }
        else if (pos.y >= upperYLimit)
        {
            movingDown = true;
        }

        rotationTimer += Time.deltaTime;
        if (rotationTimer >= rotationInterval)
        {
            transform.Rotate(0f, 0f, zRotationStep);
            rotationTimer = 0f;
        }
    }
}
