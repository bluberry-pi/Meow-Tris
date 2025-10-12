using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundObject;
    [SerializeField] private AudioSource bgmSource;

    private bool bgmStarted = false;

    private void Awake()
    {
        // Destroy if it's MainMenu scene
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject);
            return;
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Only start music once
        if (!bgmStarted && bgmSource.clip != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
            bgmStarted = true;
        }
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransformation, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, spawnTransformation.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlayRandomSound(AudioClip[] audioClip, Transform spawnTransformation, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundObject, spawnTransformation.position, Quaternion.identity);
        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void SetBackgroundMusic(AudioClip musicClip, float volume = 1f)
    {
        if (bgmStarted) return;

        bgmSource.clip = musicClip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
        bgmStarted = true;
    }
}
