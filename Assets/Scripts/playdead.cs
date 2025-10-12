using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AudioClip catded;
    public float reloadDelay = 1f;

    private bool triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.gameObject.layer == LayerMask.NameToLayer("Danger"))
        {
            triggered = true;

            if (SoundManager.instance != null && catded != null)
                SoundManager.instance.PlaySound(catded, transform, 1f);

            if (animator != null)
                animator.enabled = false;

            Time.timeScale = 0f;
            StartCoroutine(ReloadAfterDelay(reloadDelay));
        }
    }

    private System.Collections.IEnumerator ReloadAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
