using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public float pressedY = -0.1f;
    public float pressSpeed = 5f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isPressed = false;
    public GameObject door;
    public float moveSpeed = 5f;
    public float opened = 10.5f;
    public float closed = -6.13f;
    public bool isx = true;
    public bool isneg = true;

    void Start()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    void Update()
    {
        if (isPressed == true)
        {
            Debug.Log("Door Opening");
            doorOpens();
        }
        else
        {
            Debug.Log("Door closes");
            doorCloses();
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * pressSpeed);
    }

    void doorOpens()
    {
        if (isx == true)
        {
            if (isneg == true)
            {
                if (door.transform.position.x > opened)
                {
                    door.transform.position = door.transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
                }
            }
            else if (isneg == false)
            {
                if (door.transform.position.x < opened)
                {
                    door.transform.position = door.transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
                }
            }

        }
        else if (isx == false)
        {
            if (isneg == true)
            {
                if (door.transform.position.y > opened)
                {
                    door.transform.position = door.transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
                }
            }
            else if (isneg == false)
            {
                if (door.transform.position.y < opened)
                {
                    door.transform.position = door.transform.position + (Vector3.up * moveSpeed) * Time.deltaTime;
                }
            }

        }


    }

    void doorCloses()
    {
        if (isx == true)
        {
            if (isneg == true)
            {
                if (door.transform.position.x < closed)
                {
                    door.transform.position = door.transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
                }
            }
            else if (isneg == false)
            {
                if (door.transform.position.x > closed)
                {
                    door.transform.position = door.transform.position + (Vector3.right * moveSpeed) * Time.deltaTime;
                }
            }

        }
        else if (isx == false)
        {
            if (isneg == true)
            {
                if (door.transform.position.y < closed)
                {
                    door.transform.position = door.transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
                }
            }
            else if (isneg == false)
            {
                if (door.transform.position.y > closed)
                {
                    door.transform.position = door.transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
                }
            }

        }


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
