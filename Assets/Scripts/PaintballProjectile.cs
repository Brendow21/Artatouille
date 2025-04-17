using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PaintballProjectile : MonoBehaviour
{
    public Color paintColor = Color.blue;
    public int brushSize = 8;
    public AudioClip splatSound;
    public float volume = 1f;

    private void Start()
    {
        // Prevent missing collisions for fast projectiles
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;

        // Try to paint on a canvas
        DrawingCanvas canvas = collision.collider.GetComponent<DrawingCanvas>();
        if (canvas != null)
        {
            canvas.DrawAtPosition(hitPoint, paintColor, brushSize);
        }

        // Play splat sound
        if (splatSound != null)
        {
            AudioSource.PlayClipAtPoint(splatSound, hitPoint, volume);
        }

        Destroy(gameObject);
    }
}
