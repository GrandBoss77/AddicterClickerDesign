using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    // Singleton instance to ensure only one instance exists
    private static BackgroundMusicManager instance;

    private AudioSource audioSource;

    // Awake is called before Start
    private void Awake()
    {
        // Ensure only one instance of the BackgroundMusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the GameObject when loading a new scene
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate instance
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Play the background music
        PlayBackgroundMusic();
    }

    // Function to play the background music
    private void PlayBackgroundMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // SceneManager.sceneLoaded is called after a scene is loaded
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic(); // Ensure background music keeps playing when a new scene is loaded
    }
}
