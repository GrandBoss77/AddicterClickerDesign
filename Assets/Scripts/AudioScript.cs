using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip audioClip;

    private void Start()
    {
        if (audioClip == null)
        {
            Debug.LogError("AudioClip is not assigned. Please assign an audio clip in the Unity Editor.");
            enabled = false;
        }

        // Attach a method to the button's click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayAudioOnClick);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }
    }

    // Method to play audio when the button is clicked
    private void PlayAudioOnClick()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject.");
            return;
        }

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
