using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip start;
    public void PlayGame()
    {
        //SoundManager.instance.PlaySound(start, transform, 1f);
        SceneManager.LoadSceneAsync(1);
    }
}
