using UnityEngine;

public class rotatingObstacle : MonoBehaviour
{
    public GameObject obs;
    private float timer = 0f;
    public float rotatingSpeed = 0.5f;
    public float rotationDegree = 25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotate();
    }

    void rotate()
    {
        timer = timer + Time.deltaTime;
        if (timer >= rotatingSpeed)
        {
            obs.transform.Rotate(0f, 0f, rotationDegree);
            timer = 0f;
        }
    }
}
