using UnityEngine;

public class BrushSound : MonoBehaviour
{
    public AudioSource brushAudio;         // Assign an AudioSource with your looped painting sound
    public LayerMask canvasLayer;          // Assign the Canvas layer
    public float detectionRadius = 0.01f;  // Detection size (tiny is good)

    private bool isTouching = false;

    private void Update()
    {
        bool touchingNow = Physics.CheckSphere(transform.position, detectionRadius, canvasLayer);

        if (touchingNow && !isTouching)
        {
            // Just started touching
            if (brushAudio != null && !brushAudio.isPlaying)
            {
                brushAudio.Play();
            }
        }
        else if (!touchingNow && isTouching)
        {
            // Just stopped touching
            if (brushAudio != null && brushAudio.isPlaying)
            {
                brushAudio.Stop();
            }
        }

        isTouching = touchingNow;
    }
}
