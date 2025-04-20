using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource radioAudio;  // Reference to the radio's AudioSource

    // This will be called by the OnClick() event of the Button
    public void ToggleAudio()
    {
        if (radioAudio == null) return;

        // Toggle the audio
        if (radioAudio.isPlaying)
            radioAudio.Pause();
        else
            radioAudio.Play();
    }
}
