using UnityEngine;

public class DynamicSpawnPoint : MonoBehaviour
{
    public GameObject[] tetrominos;
    public Transform spawnPoint;
    public float isOut = 1.5f;

    private GameObject current;

    void Update()
    {
        if (current == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SpawnTetromino();
            }
        }
        else
        {
            if (current.transform.position.y < isOut)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SpawnTetromino();
                }
            }
        }
    }

    void SpawnTetromino()
    {
        if (tetrominos.Length == 0 || spawnPoint == null)
        {
            Debug.LogError("Spawn error: No tetrominos or spawn point assigned.");
            return;
        }

        int randomIndex = Random.Range(0, tetrominos.Length);
        current = Instantiate(tetrominos[randomIndex], spawnPoint.position, Quaternion.identity);
    }
}
