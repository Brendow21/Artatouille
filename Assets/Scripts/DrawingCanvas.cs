using UnityEngine;

/// <summary>
/// Canvas script for the drawing system.
/// Attach this to a canvas object with a white texture.
/// </summary>
public class DrawingCanvas : MonoBehaviour
{
    [Header("Canvas Settings")]
    public int totalXPixels = 512;
    public int totalYPixels = 512;
    public Transform topLeftCorner;
    public Transform botRightCorner;
    public Material material;
    
    // Drawing data
    private Texture2D generatedTexture;
    private Color[] colorMap;
    private float xMult;
    private float yMult;
    private Vector3 canvasNormal;
    private Plane canvasPlane;
    
    private void Start()
    {
        // Check if corners are set
        if (topLeftCorner == null || botRightCorner == null)
        {
            return;
        }
        
        // Initialize canvas
        colorMap = new Color[totalXPixels * totalYPixels];
        generatedTexture = new Texture2D(totalXPixels, totalYPixels, TextureFormat.RGBA32, false);
        generatedTexture.filterMode = FilterMode.Point;
        
        // Ensure the texture is properly applied to the material
        if (material != null)
        {
            material.SetTexture("_MainTex", generatedTexture);
        }
        else
        {
            Debug.LogError("No material assigned to DrawingCanvas!");
        }
        
        // Reset to white
        ResetColor();
        
        // Log the corner positions for debugging
        Vector3 cornerDiff = botRightCorner.position - topLeftCorner.position;
        Debug.Log($"TopLeft: {topLeftCorner.position}, BotRight: {botRightCorner.position}");
        Debug.Log($"Corner difference: X={cornerDiff.x}, Y={cornerDiff.y}, Z={cornerDiff.z}");
        
        // Determine which plane the canvas is on by finding the smallest component
        float absY = Mathf.Abs(cornerDiff.y);
        float absZ = Mathf.Abs(cornerDiff.z);
        
        int width, height;
        Vector3 widthDir, heightDir;
        
        width = totalXPixels;
        height = totalYPixels;
        widthDir = new Vector3(0, 0, 1);  // Z is width
        heightDir = new Vector3(0, 1, 0); // Y is height
        
        // Calculate multipliers for Y-Z plane
        xMult = width / Mathf.Max(0.001f, absZ);  // Z is width
        yMult = height / Mathf.Max(0.001f, absY); // Y is height
        canvasNormal = new Vector3(1, 0, 0);      // X is normal
        
        // Ensure multipliers are reasonable (limit to 1000 max)
        if (xMult > 1000f)
        {
            xMult = 1000f;
        }
        
        if (yMult > 1000f)
        {
            yMult = 1000f;
        }
        
        // Create a plane for the canvas
        canvasPlane = new Plane(canvasNormal, topLeftCorner.position);
        
        // Apply the texture to make sure it's visible
        UpdateTexture();
    }
    
    /// <summary>
    /// Draw at a world position projected onto the canvas plane
    /// </summary>
    public void DrawAtPosition(Vector3 worldPos, Color color, int size)
    {
        // Calculate UV coordinates on the canvas
        Vector2 pixelCoord = WorldToCanvasPixel(worldPos);
        int xPixel = Mathf.RoundToInt(pixelCoord.x);
        int yPixel = Mathf.RoundToInt(pixelCoord.y);
        
        // Draw at the calculated pixel position
        Draw(xPixel, yPixel, color, size);
    }
    
    /// <summary>
    /// Converts a world position to canvas pixel coordinates
    /// </summary>
    private Vector2 WorldToCanvasPixel(Vector3 worldPos)
    {
        // Get direction vectors from top-left corner
        Vector3 localPos = worldPos - topLeftCorner.position;
        Vector3 cornerDiff = botRightCorner.position - topLeftCorner.position;
        
        float x, y;

        // Canvas is in Y-Z plane (X is normal)
        x = Mathf.Abs(localPos.z) * xMult;
        y = Mathf.Abs(localPos.y) * yMult;
        
        // Ensure we're within canvas boundaries
        x = Mathf.Clamp(x, 0, totalXPixels - 1);
        y = Mathf.Clamp(y, 0, totalYPixels - 1);
        
        return new Vector2(x, y);
    }
    
    /// <summary>
    /// Draw on the canvas at the specified pixel coordinates.
    /// </summary>
    public void Draw(int xPos, int yPos, Color color, int size)
    {
        // Validate coordinates - clamp to valid range
        xPos = Mathf.Clamp(xPos, 0, totalXPixels - 1);
        yPos = Mathf.Clamp(yPos, 0, totalYPixels - 1);
        
        // Calculate brush bounds
        int i = xPos - size + 1;
        int j = yPos - size + 1;
        int maxi = xPos + size - 1;
        int maxj = yPos + size - 1;
        
        // Clamp to canvas boundaries
        i = Mathf.Max(0, i);
        j = Mathf.Max(0, j);
        maxi = Mathf.Min(totalXPixels - 1, maxi);
        maxj = Mathf.Min(totalYPixels - 1, maxj);
        
        // Draw a circular brush at the specified position
        for (int x = i; x <= maxi; x++)
        {
            for (int y = j; y <= maxj; y++)
            {
                if ((x - xPos) * (x - xPos) + (y - yPos) * (y - yPos) <= size * size)
                {
                    int index = x * totalYPixels + y;
                    if (index >= 0 && index < colorMap.Length)
                    {
                        colorMap[index] = color;
                    }
                }
            }
        }
        
        // Update texture
        UpdateTexture();
    }
    
    /// <summary>
    /// Draw multiple pixels between two points for smooth strokes.
    /// </summary>
    public void DrawLine(Vector3 fromPos, Vector3 toPos, Color color, int size)
    {
        // Calculate distance and number of steps
        float distance = Vector3.Distance(fromPos, toPos);
        int steps = Mathf.CeilToInt(distance * 10); // Adjust multiplier for density
        
        if (steps > 1)
        {
            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                Vector3 interpolatedPoint = Vector3.Lerp(fromPos, toPos, t);
                DrawAtPosition(interpolatedPoint, color, size);
            }
        }
        else
        {
            // If only one step, just draw at the end position
            DrawAtPosition(toPos, color, size);
        }
    }
    
    /// <summary>
    /// Update the texture with the current color map.
    /// </summary>
    private void UpdateTexture()
    {
        if (generatedTexture != null)
        {
            generatedTexture.SetPixels(colorMap);
            generatedTexture.Apply();
            
            // Double-check texture is assigned to material
            if (material != null && material.GetTexture("_MainTex") != generatedTexture)
            {
                material.SetTexture("_MainTex", generatedTexture);
            }
        }
    }
    
    /// <summary>
    /// Reset the canvas to the specified color (default white).
    /// </summary>
    public void ResetColor(Color? color = null)
    {
        Color resetColor = color ?? Color.white;
        
        for (int i = 0; i < colorMap.Length; i++)
        {
            colorMap[i] = resetColor;
        }
        
        UpdateTexture();
    }
}