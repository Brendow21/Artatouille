using UnityEngine;

public class PaintballGun : MonoBehaviour
{
    public GameObject paintballTemplate;  // Assign a prefab (e.g., sphere with Rigidbody & collider)
    public Transform spawnPoint;          // Where the paintball spawns
    public float shootForce = 10f;        // How fast it's shot
    public Color paintColor = Color.blue; // Paint color
    public int brushSize = 8;


    public void ShootPaintball()
    {
        if (paintballTemplate == null || spawnPoint == null) return;

        // Instantiate paintball
        GameObject paintball = Instantiate(paintballTemplate, spawnPoint.position, spawnPoint.rotation);

        Collider gunCollider = GetComponent<Collider>();
        Collider paintballCollider = paintball.GetComponent<Collider>();
        if (gunCollider != null && paintballCollider != null)
        {
            Physics.IgnoreCollision(paintballCollider, gunCollider);
        }

        PaintballProjectile projectile = paintball.GetComponent<PaintballProjectile>();
        if (projectile != null)
        {
            projectile.paintColor = paintColor;
            projectile.brushSize = brushSize;
        }

        // Add force
        Rigidbody rb = paintball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = spawnPoint.forward * shootForce;
        }

        // Set paintball color
        Renderer rend = paintball.GetComponent<Renderer>();
        if (rend != null)
        {
            Material mat = new Material(rend.material); // avoid modifying shared material
            mat.color = paintColor;
            rend.material = mat;
        }

        // Destroy after time to avoid buildup
        Destroy(paintball, 5f);
    }
}
