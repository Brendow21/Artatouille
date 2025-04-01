using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Basic brush implementation for VR drawing.
/// </summary>
public class Brush : MonoBehaviour
{
    [Header("Brush Properties")]
    public int brushSize = 4;
    public Color brushColor = Color.black;
    
    [Header("Brush Setup")]
    public Transform brushTip;
    public XRGrabInteractable grabInteractable;
    public LayerMask canvasLayer;
    public float drawDistance = 0.02f; // Max distance from canvas to draw
    
    // State tracking
    protected bool isGrabbed = false;
    protected Vector3 lastContactPoint;
    protected DrawingCanvas lastCanvas;
    protected bool isDrawing = false;
    
    protected virtual void Start()
    {
        // Setup grab interactable
        if (grabInteractable == null)
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
        }
        
        if (grabInteractable != null)
        {
            // Track when brush is grabbed/released
            grabInteractable.selectEntered.AddListener((args) => {
                isGrabbed = true;
                Debug.Log("Brush grabbed");
            });
            
            grabInteractable.selectExited.AddListener((args) => {
                isGrabbed = false;
                lastCanvas = null;
                isDrawing = false;
                Debug.Log("Brush released");
            });
        }
        
        // Make sure brush tip exists
        if (brushTip == null)
        {
            brushTip = new GameObject("BrushTip").transform;
            brushTip.SetParent(transform);
            brushTip.localPosition = new Vector3(0, 0, 0.1f);
        }
        
        // Add a visual indicator for the brush tip
        GameObject visualTip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        visualTip.transform.SetParent(brushTip);
        visualTip.transform.localPosition = Vector3.zero;
        visualTip.transform.localScale = Vector3.one * 0.02f;
        Destroy(visualTip.GetComponent<Collider>());
        
        // Use a shared material to avoid shader errors
        Renderer renderer = visualTip.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Try to set the material color without creating a new material
            if (renderer.material != null)
            {
                renderer.material.color = brushColor;
            }
        }
        
        Debug.Log("Brush initialized");
    }
    
    protected virtual void Update()
    {
        if (!isGrabbed) return;
        
        // Simple sphere overlap check for any canvas colliders
        Collider[] hitColliders = Physics.OverlapSphere(brushTip.position, 0.015f, canvasLayer);
        
        bool foundCanvas = false;
        foreach (var hitCollider in hitColliders)
        {
            DrawingCanvas canvas = hitCollider.GetComponent<DrawingCanvas>();
            if (canvas != null)
            {
                // Find the closest point on the collider surface
                Vector3 contactPoint = hitCollider.ClosestPoint(brushTip.position);
                
                // Visual debugging
                Debug.DrawLine(brushTip.position, contactPoint, Color.green, Time.deltaTime);
                
                if (!isDrawing || lastCanvas != canvas)
                {
                    // First contact with this canvas
                    lastCanvas = canvas;
                    lastContactPoint = contactPoint;
                    isDrawing = true;
                    Debug.Log("First contact with canvas at " + contactPoint);
                    
                    // Draw first point
                    canvas.DrawAtPosition(contactPoint, brushColor, brushSize);
                }
                else
                {
                    // Only draw if moved enough
                    if (Vector3.Distance(lastContactPoint, contactPoint) > 0.002f)
                    {
                        // Draw line between points
                        canvas.DrawLine(lastContactPoint, contactPoint, brushColor, brushSize);
                        lastContactPoint = contactPoint;
                    }
                }
                
                foundCanvas = true;
                break; // Only use the first canvas found
            }
        }
        
        // If no canvas was found this frame, but we were drawing
        if (!foundCanvas && isDrawing)
        {
            isDrawing = false;
            Debug.Log("Lost contact with canvas");
        }
        
        // Alternative approach: Raycast in multiple directions around the brush tip
        if (!foundCanvas)
        {
            RaycastMultipleDirections();
        }
    }
    
    /// <summary>
    /// Perform raycasts in multiple directions to try to hit a canvas
    /// </summary>
    private void RaycastMultipleDirections()
    {
        // Define multiple directions to try
        Vector3[] directions = new Vector3[] {
            brushTip.forward,
            -brushTip.forward,
            brushTip.up,
            -brushTip.up,
            brushTip.right,
            -brushTip.right
        };
        
        // Try each direction
        foreach (Vector3 dir in directions)
        {
            RaycastHit hit;
            if (Physics.Raycast(brushTip.position, dir, out hit, drawDistance, canvasLayer))
            {
                DrawingCanvas canvas = hit.collider.GetComponent<DrawingCanvas>();
                if (canvas != null)
                {
                    // Visual debugging
                    Debug.DrawLine(brushTip.position, hit.point, Color.green, Time.deltaTime);
                    Debug.DrawRay(hit.point, hit.normal * 0.03f, Color.blue, Time.deltaTime);
                    
                    if (!isDrawing || lastCanvas != canvas)
                    {
                        // First contact with this canvas
                        lastCanvas = canvas;
                        lastContactPoint = hit.point;
                        isDrawing = true;
                        Debug.Log($"First contact with canvas using raycast in direction {dir}");
                        
                        // Draw first point
                        canvas.DrawAtPosition(hit.point, brushColor, brushSize);
                    }
                    else
                    {
                        // Only draw if moved enough
                        if (Vector3.Distance(lastContactPoint, hit.point) > 0.002f)
                        {
                            // Draw line between points
                            canvas.DrawLine(lastContactPoint, hit.point, brushColor, brushSize);
                            lastContactPoint = hit.point;
                        }
                    }
                    
                    return; // Stop after finding one hit
                }
            }
        }
    }
    
    /// <summary>
    /// For debug visualization
    /// </summary>
    private void OnDrawGizmos()
    {
        if (brushTip != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(brushTip.position, 0.015f);
            Gizmos.DrawRay(brushTip.position, brushTip.forward * 0.05f);
        }
    }
}