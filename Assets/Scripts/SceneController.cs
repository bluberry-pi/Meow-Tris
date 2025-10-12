using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;

    private bool isLoading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        if (isLoading) return;
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        isLoading = true;

        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadSceneAsync(nextIndex);
        }
        else
        {
            SceneManager.LoadSceneAsync("Level1"); // or main menu
        }

        yield return new WaitForSeconds(0.5f); // Give time for scene to load
        transitionAnim.SetTrigger("Start");

        isLoading = false;
    }
}
