using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float startingTime = 5f;
    private float remainingTime;
    [SerializeField] private AudioClip ded;

    private static bool hasStarted = false;

    // Queue to store fallen tetronimos in order
    public static Queue<TetronimoMovement> fallenTetronimos = new Queue<TetronimoMovement>();

    void Start()
    {
        remainingTime = startingTime;
        UpdateTimerUI(); // Display 00:05 initially

        // Reset static state when the scene starts
        hasStarted = false;
        TetronimoMovement.ResetFirstPlacement();
        fallenTetronimos.Clear();
    }


    void Update()
    {
        if (!hasStarted) return;

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            if (fallenTetronimos.Count > 0)
            {
                TetronimoMovement tetToDestroy = fallenTetronimos.Dequeue();
                if (tetToDestroy != null && tetToDestroy.gameObject != null)
                {
                    SoundManager.instance.PlaySound(ded, transform, 1f);
                    Destroy(tetToDestroy.gameObject);
                }
            }


            remainingTime = startingTime;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Call this when the first tetronimo is placed
    public static void StartTimer()
    {
        hasStarted = true;
    }
}
