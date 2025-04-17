using UnityEngine;

/// <summary>
/// PaintballGun: Fires paintballs that paint on a canvas.
/// </summary>
public class PaintballGun : MonoBehaviour
{
    [Header("Paintball Settings")]
    public GameObject paintballTemplate;   // Assign a prefab with Rigidbody, Collider, Renderer
    public Transform spawnPoint;           // Point from which paintballs fire
    public float shootForce = 10f;         // Force applied to paintball
    public Color paintColor = Color.blue;  // Paint color applied to canvas
    public int brushSize = 8;              // Brush radius for paint on contact

    public void ShootPaintball()
    {
        if (paintballTemplate == null || spawnPoint == null)
        {
            Debug.LogWarning("Missing paintball template or spawn point.");
            return;
        }

        // Instantiate paintball at spawn location and rotation
        GameObject paintball = Instantiate(paintballTemplate, spawnPoint.position, spawnPoint.rotation);

        // Prevent self-collision
        Collider gunCollider = GetComponent<Collider>();
        Collider paintballCollider = paintball.GetComponent<Collider>();
        if (gunCollider != null && paintballCollider != null)
        {
            Physics.IgnoreCollision(paintballCollider, gunCollider);
        }

        // Assign paint color and brush size to the projectile
        PaintballProjectile projectile = paintball.GetComponent<PaintballProjectile>();
        if (projectile != null)
        {
            projectile.paintColor = paintColor;
            projectile.brushSize = brushSize;
        }

        // Apply force with continuous collision detection
        Rigidbody rb = paintball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.linearVelocity = spawnPoint.forward * shootForce;
        }

        // Set the paintball's visible color
        Renderer rend = paintball.GetComponent<Renderer>();
        if (rend != null)
        {
            Material mat = new Material(rend.material); // Avoid shared material changes
            mat.color = paintColor;
            rend.material = mat;
        }

        // Cleanup: Destroy paintball after 5 seconds
        Destroy(paintball, 5f);
    }
}
