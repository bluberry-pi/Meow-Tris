using UnityEngine;

public class L8doorButton : MonoBehaviour
{
    public float pressedY = -0.1f;
    public float pressSpeed = 5f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    public bool isPressed = false;

    void Start()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    void Update()
    {

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * pressSpeed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        isPressed = true;
        targetPosition = originalPosition + new Vector3(0, pressedY, 0);

    }

    void OnTriggerExit2D(Collider2D other)
    {

        isPressed = false;
        targetPosition = originalPosition;

    }
}
