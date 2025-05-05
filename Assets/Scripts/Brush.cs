using UnityEngine;

/// <summary>
/// Simplified brush for VR drawing with reliable collision detection.
/// Now supports both Brush and Paint Roller tools.
/// </summary>
public class Brush : MonoBehaviour
{
    public enum BrushType { Brush, Roller }

    [Header("Brush Properties")]
    public BrushType toolType = BrushType.Brush;
    public int brushSize = 4;
    public int rollerSize = 12;
    public Color brushColor = Color.black;
    public Material brushMaterial;

    [Header("Brush Setup")]
    public Transform brushTip;
    public LayerMask canvasLayer;
    public float detectionRadius = 0.02f;
    public float rayDistance = 0.1f;

    public bool canChangeColor;

    // State tracking
    private Vector3 lastContactPoint;
    private DrawingCanvas currentCanvas;
    private bool isDrawing = false;

    private void Start()
    {
        // Optional: auto-detect tool type based on name
        if (gameObject.name.ToLower().Contains("roller"))
        {
            toolType = BrushType.Roller;
        }

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
        RulerSnap ruler = FindObjectOfType<RulerSnap>();

        if (Physics.CheckSphere(brushTip.position, detectionRadius, canvasLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(brushTip.position, detectionRadius, canvasLayer);
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
            int size = (toolType == BrushType.Roller) ? rollerSize : brushSize;
            bool isOnRuler = ruler != null && ruler.IsBrushOnRuler(brushTip.position);
            if (!isDrawing || currentCanvas != canvas)
            {
                currentCanvas = canvas;
                lastContactPoint = contactPoint;
                isDrawing = true;

                canvas.DrawAtPosition(contactPoint, brushColor, size);
            }
            else
            {
                float distance = Vector3.Distance(lastContactPoint, contactPoint);
                float minDistance = 0.0005f;

                if (distance > minDistance)
                {
                    if (isOnRuler)
                    {
                        Vector3 rulerDirection = ruler.transform.right; // Horizontal direction
                        Vector3 constrainedPoint = lastContactPoint + Vector3.Project(contactPoint - lastContactPoint, rulerDirection);
                        canvas.DrawLine(lastContactPoint, constrainedPoint, brushColor, size);
                        lastContactPoint = constrainedPoint;

                    }
                    else
                    {
                        canvas.DrawLine(lastContactPoint, contactPoint, brushColor, size);
                        lastContactPoint = contactPoint;
                    }
                }
            }
        }
        else if (isDrawing)
        {
            isDrawing = false;
            currentCanvas = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(canChangeColor) {
            changeColor(other);
        }
    }

    public void changeColor(Collider other, Color color) {
        string objName = other.gameObject.name.ToLower();
        brushColor = color;
        brushMaterial.SetColor("_Color", brushColor);
    }

    public void changeColor(Collider other) {
        string objName = other.gameObject.name.ToLower();
        switch (objName)
        {
            case "red":
                brushColor = Color.red;
                break;
            case "green":
                brushColor = Color.green;
                break;
            case "blue":
                brushColor = Color.blue;
                break;
            case "yellow":
                brushColor = Color.yellow;
                break;
            case "black":
                brushColor = Color.black;
                break;
            case "white":
                brushColor = Color.white;
                break;
            case "purple":
                brushColor = Color.magenta;
                break;
            case "grey":
                brushColor = Color.grey;
                break;
            case "eraser":
                brushColor = Color.white;
                break;
            default:
                break;
        }
        brushMaterial.SetColor("_Color", brushColor);
    }
}