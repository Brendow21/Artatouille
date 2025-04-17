using UnityEngine;

public class PaintballProjectile : MonoBehaviour
{
    public Color paintColor = Color.blue;
    public int brushSize = 8;
    public AudioClip splatSound;
    public float volume = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        DrawingCanvas canvas = collision.collider.GetComponent<DrawingCanvas>();

        if (canvas != null)
        {
            // Paint at collision point
            Vector3 hitPoint = collision.contacts[0].point;
            canvas.DrawAtPosition(hitPoint, paintColor, brushSize);
        }

        // Play splat sound at collision point
        if (splatSound != null)
        {
            AudioSource.PlayClipAtPoint(splatSound, transform.position, volume);
        }

        // Destroy the projectile
        Destroy(gameObject);
    }
}
