using UnityEngine;

/// <summary>
/// Simplified brush for VR drawing with reliable collision detection.
/// </summary>
public class Brush : MonoBehaviour
{
    [Header("Brush Properties")]
    public int brushSize = 4;
    public Color brushColor = Color.black;
    
    [Header("Brush Setup")]
    public Transform brushTip;
    public LayerMask canvasLayer;
    public float detectionRadius = 0.02f;
    public float rayDistance = 0.1f;
    
    // State tracking
    private Vector3 lastContactPoint;
    private DrawingCanvas currentCanvas;
    private bool isDrawing = false;
    
    private void Start()
    {
        if (brushTip == null)
        {
            brushTip = new GameObject("BrushTip").transform;
            brushTip.SetParent(transform);
            brushTip.localPosition = new Vector3(0, 0, 0.1f);
        }
    }
    
    private void Update()
    {
        bool canvasDetected = false;
        Vector3 contactPoint = Vector3.zero;
        DrawingCanvas canvas = null;
        if (Physics.CheckSphere(brushTip.position, detectionRadius, canvasLayer))
        {
            // Find the closest canvas in range
            Collider[] colliders = Physics.OverlapSphere(brushTip.position, detectionRadius, canvasLayer);
            
            // Process the closest collider
            if (colliders.Length > 0)
            {
                Collider closestCollider = colliders[0];
                canvas = closestCollider.GetComponent<DrawingCanvas>();
                
                if (canvas != null)
                {
                    canvasDetected = true;
                    contactPoint = closestCollider.ClosestPoint(brushTip.position);
                }
            }
        }
        
        if (canvasDetected && canvas != null)
        {
            if (!isDrawing || currentCanvas != canvas)
            {
                // First contact with this canvas
                currentCanvas = canvas;
                lastContactPoint = contactPoint;
                isDrawing = true;
                
                // Draw initial point
                canvas.DrawAtPosition(contactPoint, brushColor, brushSize);
            }
            else
            {
                // Only draw if we've moved enough
                float distance = Vector3.Distance(lastContactPoint, contactPoint);
                float minDistance = 0.0005f; // Small threshold for smoother lines
                
                if (distance > minDistance)
                {
                    // Draw line between points
                    canvas.DrawLine(lastContactPoint, contactPoint, brushColor, brushSize);
                    lastContactPoint = contactPoint;
                }
            }
        }
        else if (isDrawing)
        {
            // Lost contact with canvas
            isDrawing = false;
            currentCanvas = null;
        }
    }
}