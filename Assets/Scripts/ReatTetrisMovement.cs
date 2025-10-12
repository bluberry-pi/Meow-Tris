using UnityEngine;

public class ReatTetrisMovement : MonoBehaviour
{
    public float deadZone = -6.8f;

    public Vector3 rotationPoint;
    public float fallInterval = 1f;
    public float minFallInterval = 0.1f;
    public float maxFallInterval = 2f;
    public float scrollSensitivity = 0.1f;
    public bool fall = true;

    private float fallTimer = 0f;
    private float lastMouseX;

    public LayerMask collisionLayers;  // assign Ground + Block layers in Inspector


    void Start()
    {
        lastMouseX = Input.mousePosition.x;
    }


    void Update()
    {
        if (!fall) return;

        HandleMouseMovement();
        HandleScroll();
        HandleRotation();
        HandleFalling();
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void HandleMouseMovement()
    {
        float currentMouseX = Input.mousePosition.x;
        float deltaX = currentMouseX - lastMouseX;

        if (Mathf.Abs(deltaX) > 10f)
        {
            int direction = deltaX > 0 ? 1 : -1;
            Vector3 move = new Vector3(direction, 0, 0);
            if (IsMovePossible(move))
            {
                transform.position += move;
            }
            lastMouseX = currentMouseX;
        }
    }

    void HandleScroll()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0)
            fallInterval = Mathf.Max(minFallInterval, fallInterval - scrollSensitivity);
        else if (scroll < 0)
            fallInterval = Mathf.Min(maxFallInterval, fallInterval + scrollSensitivity);
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPivot = transform.TransformPoint(rotationPoint);

            Vector3 originalPos = transform.position;
            Quaternion originalRot = transform.rotation;

            transform.RotateAround(worldPivot, Vector3.forward, -90f);
            SnapToGrid();

            if (!IsMovePossible(Vector3.zero))
            {
                transform.position = originalPos;
                transform.rotation = originalRot;
            }
        }
    }



    void HandleFalling()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer >= fallInterval)
        {
            fallTimer = 0f;

            if (HasHitGround())
            {
                fall = false;
                SnapToGrid();
                SetLayerRecursive(transform, LayerMask.NameToLayer("Block"));
            }


            else
            {
                transform.position += Vector3.down;
            }
        }
    }

    void SetLayerRecursive(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        foreach (Transform child in obj)
        {
            SetLayerRecursive(child, layer);
        }
    }

    bool HasHitGround()
    {
        foreach (Transform child in transform)
        {
            Vector2 pos = child.position;
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 1f, collisionLayers);
            if (hit.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    bool IsMovePossible(Vector3 offset)
    {
        foreach (Transform child in transform)
        {
            Vector2 targetPos = (Vector2)(child.position + offset);
            Collider2D hit = Physics2D.OverlapBox(targetPos, Vector2.one * 0.9f, 0, collisionLayers);
            if (hit != null)
            {
                return false;
            }
        }
        return true;
    }

    void SnapToGrid()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = child.position;
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            child.position = pos;
        }
    }



}
