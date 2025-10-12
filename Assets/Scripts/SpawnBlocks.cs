using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBlocks : MonoBehaviour
{
    public GameObject[] tetrominos;
    public Transform spawnPoint;
    public TetronimoMovement currentTetronimo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentTetronimo == null || currentTetronimo.fall == false)
            {
                spawn();
            }
        }

    }

    void spawn()
    {
        if (tetrominos.Length == 0 || spawnPoint == null)
        {
            Debug.Log("Error!");
            return;
        }
        int randomIndex = Random.Range(0, tetrominos.Length);
        GameObject obj = Instantiate(tetrominos[randomIndex], spawnPoint.position, Quaternion.identity);
        currentTetronimo = obj.GetComponent<TetronimoMovement>();
    }
}
