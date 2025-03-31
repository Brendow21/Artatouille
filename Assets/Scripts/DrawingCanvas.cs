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
            Debug.LogError("Canvas corners not set! Please assign topLeftCorner and botRightCorner.");
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
        float absX = Mathf.Abs(cornerDiff.x);
        float absY = Mathf.Abs(cornerDiff.y);
        float absZ = Mathf.Abs(cornerDiff.z);
        
        int width, height;
        Vector3 widthDir, heightDir;
        
        // Find the two largest dimensions to use as width/height
        if (absX <= absY && absX <= absZ)
        {
            // X is smallest, so canvas is primarily in Y-Z plane
            width = totalXPixels;
            height = totalYPixels;
            widthDir = new Vector3(0, 0, 1);  // Z is width
            heightDir = new Vector3(0, 1, 0); // Y is height
            
            // Calculate multipliers for Y-Z plane
            xMult = width / Mathf.Max(0.001f, absZ);  // Z is width
            yMult = height / Mathf.Max(0.001f, absY); // Y is height
            canvasNormal = new Vector3(1, 0, 0);      // X is normal
            
            Debug.Log("Canvas is in Y-Z plane");
        }
        else if (absY <= absX && absY <= absZ)
        {
            // Y is smallest, so canvas is primarily in X-Z plane
            width = totalXPixels;
            height = totalYPixels;
            widthDir = new Vector3(1, 0, 0);  // X is width
            heightDir = new Vector3(0, 0, 1); // Z is height
            
            // Calculate multipliers for X-Z plane
            xMult = width / Mathf.Max(0.001f, absX);  // X is width
            yMult = height / Mathf.Max(0.001f, absZ); // Z is height
            canvasNormal = new Vector3(0, 1, 0);      // Y is normal
            
            Debug.Log("Canvas is in X-Z plane");
        }
        else
        {
            // Z is smallest, so canvas is primarily in X-Y plane
            width = totalXPixels;
            height = totalYPixels;
            widthDir = new Vector3(1, 0, 0);  // X is width
            heightDir = new Vector3(0, 1, 0); // Y is height
            
            // Calculate multipliers for X-Y plane
            xMult = width / Mathf.Max(0.001f, absX);  // X is width
            yMult = height / Mathf.Max(0.001f, absY); // Y is height
            canvasNormal = new Vector3(0, 0, 1);      // Z is normal
            
            Debug.Log("Canvas is in X-Y plane");
        }
        
        // Ensure multipliers are reasonable (limit to 1000 max)
        if (xMult > 1000f)
        {
            Debug.LogWarning($"X multiplier ({xMult}) is too high, limiting to 1000");
            xMult = 1000f;
        }
        
        if (yMult > 1000f)
        {
            Debug.LogWarning($"Y multiplier ({yMult}) is too high, limiting to 1000");
            yMult = 1000f;
        }
        
        // Create a plane for the canvas
        canvasPlane = new Plane(canvasNormal, topLeftCorner.position);
        
        Debug.Log($"Canvas initialized: {totalXPixels}x{totalYPixels}");
        Debug.Log($"Canvas normal: {canvasNormal}");
        Debug.Log($"Coordinate multipliers: X={xMult}, Y={yMult}");
        
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
        
        Debug.Log($"Drawing at world position: {worldPos}, pixel: ({xPixel}, {yPixel})");
        
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
        
        float absX = Mathf.Abs(cornerDiff.x);
        float absY = Mathf.Abs(cornerDiff.y);
        float absZ = Mathf.Abs(cornerDiff.z);
        
        float x, y;
        
        // Determine pixel coordinates based on canvas orientation
        if (absX <= absY && absX <= absZ)
        {
            // Canvas is in Y-Z plane (X is normal)
            x = Mathf.Abs(localPos.z) * xMult;
            y = Mathf.Abs(localPos.y) * yMult;
        }
        else if (absY <= absX && absY <= absZ)
        {
            // Canvas is in X-Z plane (Y is normal)
            x = Mathf.Abs(localPos.x) * xMult;
            y = Mathf.Abs(localPos.z) * yMult;
        }
        else
        {
            // Canvas is in X-Y plane (Z is normal)
            x = Mathf.Abs(localPos.x) * xMult;
            y = Mathf.Abs(localPos.y) * yMult;
        }
        
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
        
        Debug.Log($"Drawing line from {fromPos} to {toPos} with {steps} steps");
        
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
    
    /// <summary>
    /// Debug function to visualize the canvas corners and normal
    /// </summary>
    private void OnDrawGizmos()
    {
        if (topLeftCorner != null && botRightCorner != null)
        {
            // Draw corners
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(topLeftCorner.position, 0.01f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(botRightCorner.position, 0.01f);
            
            // Connect corners with diagonal line
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(topLeftCorner.position, botRightCorner.position);
            
            // Draw a grid representation of the canvas if in play mode
            if (Application.isPlaying && canvasNormal != Vector3.zero)
            {
                Vector3 center = (topLeftCorner.position + botRightCorner.position) * 0.5f;
                Gizmos.color = Color.green;
                Gizmos.DrawRay(center, canvasNormal * 0.1f);
            }
        }
    }
}