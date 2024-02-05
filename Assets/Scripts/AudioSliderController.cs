using UnityEngine;
using UnityEngine.UI;

public class AudioSliderController : MonoBehaviour
{
    public Slider audioSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Add a listener to the slider's value change event
        audioSlider.onValueChanged.AddListener(UpdateAudioVolume);
    }

    void UpdateAudioVolume(float volume)
    {
        // Update the audio source volume based on the slider value
        audioSource.volume = volume;
    }
}