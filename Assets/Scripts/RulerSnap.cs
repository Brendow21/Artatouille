using UnityEngine;

public class RulerSnap : MonoBehaviour
{
    [Header("Ruler Settings")]
    public LayerMask canvasLayer;
    public float snapDistance = 0.05f;  // Distance within which ruler snaps to the canvas

    private bool isSnapped = false;
    private Vector3 snapPosition;

    private void Update()
    {
        if (!isSnapped)
        {
            DetectCanvasAndSnap();
        }
    }

    private void DetectCanvasAndSnap()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, snapDistance, canvasLayer);
        if (colliders.Length > 0)
        {
            // Snap to the first detected canvas
            snapPosition = colliders[0].ClosestPoint(transform.position);
            transform.position = snapPosition;
            isSnapped = true;

            // Optional: Freeze movement
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;  // Prevents physics movement
            }
        }
    }

    public void ReleaseRuler()
    {
        isSnapped = false;

        // Allow movement again
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;  
        }
    }

    /// <summary>
    /// Checks if the brush is close enough to the ruler to constrain movement.
    /// </summary>
    public bool IsBrushOnRuler(Vector3 brushPosition)
    {
        float distance = Vector3.Distance(brushPosition, transform.position);
        Debug.Log($"Brush Distance to Ruler: {distance}"); // Debugging line

        return distance < .8f;
    }
}