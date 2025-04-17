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
    public Material material;
    
    // Drawing data
    private Texture2D generatedTexture;
    private Color[] colorMap;
    
    private void Start()
    {
        InitializeCanvas();
        ResetCanvas();
    }
    
    private void InitializeCanvas()
    {
        // Initialize texture
        colorMap = new Color[totalXPixels * totalYPixels];
        generatedTexture = new Texture2D(totalXPixels, totalYPixels, TextureFormat.RGBA32, false);
        generatedTexture.filterMode = FilterMode.Point;
        
        // Apply texture to material
        if (material != null)
        {
            material.SetTexture("_MainTex", generatedTexture);
        }
        else
        {
            Debug.LogError("No material assigned to DrawingCanvas!");
            
            // Try to get from renderer
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                material = renderer.material;
                material.SetTexture("_MainTex", generatedTexture);
            }
        }
    }
    
    /// <summary>
    /// Draw at a world position projected onto the canvas.
    /// </summary>
    public void DrawAtPosition(Vector3 worldPos, Color color, int size)
    {
        // Get local position on the canvas
        Vector3 localPos = transform.InverseTransformPoint(worldPos);
        
        // Get normalized texture coordinates (fixes mirroring issues)
        Vector2 normalizedPos = GetNormalizedPositionOnCanvas(localPos);
        
        // Convert to pixel coordinates
        int xPixel = Mathf.RoundToInt(normalizedPos.x * totalXPixels);
        int yPixel = Mathf.RoundToInt(normalizedPos.y * totalYPixels);
        
        // Draw at the calculated pixel position
        Draw(xPixel, yPixel, color, size);
    }
    
    /// <summary>
    /// Converts a local position to normalized texture coordinates (0-1 range)
    /// </summary>
    private Vector2 GetNormalizedPositionOnCanvas(Vector3 localPos)
    {
        // Canvas dimensions based on scale
        float width = transform.localScale.x;
        float height = transform.localScale.y;
        
        // Map from local space to texture space (0 to 1)
        float normalizedX = (localPos.x + width / 2f) / width;
        float normalizedY = (localPos.y + height / 2f) / height;
        
        // Fix for X-axis reflection issue
        normalizedX = 1.0f - normalizedX;
        
        // Fix for Y-axis reflection issue
        normalizedY = 1.0f - normalizedY;
        
        return new Vector2(normalizedX, normalizedY);
    }
    
    /// <summary>
    /// Draw on the canvas at the specified pixel coordinates.
    /// </summary>
    public void Draw(int xPos, int yPos, Color color, int size)
    {
        // Validate coordinates
        xPos = Mathf.Clamp(xPos, 0, totalXPixels - 1);
        yPos = Mathf.Clamp(yPos, 0, totalYPixels - 1);
        
        // Calculate brush bounds
        int left = Mathf.Max(0, xPos - size + 1);
        int bottom = Mathf.Max(0, yPos - size + 1);
        int right = Mathf.Min(totalXPixels - 1, xPos + size - 1);
        int top = Mathf.Min(totalYPixels - 1, yPos + size - 1);
        
        // Draw a circular brush
        for (int x = left; x <= right; x++)
        {
            for (int y = bottom; y <= top; y++)
            {
                // Check if pixel is within circular brush area
                float distSq = (x - xPos) * (x - xPos) + (y - yPos) * (y - yPos);
                if (distSq <= size * size)
                {
                    // Draw pixel
                    int index = x + y * totalXPixels;
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
        int steps = Mathf.Max(3, Mathf.CeilToInt(distance * 200)); 
        
        // Draw interpolated points along the line
        for (int i = 0; i <= steps; i++)
        {
            float t = (float)i / steps;
            Vector3 point = Vector3.Lerp(fromPos, toPos, t);
            DrawAtPosition(point, color, size);
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
        }
    }
    
    /// <summary>
    /// Reset the canvas to the specified color (default white).
    /// </summary>
    public void ResetCanvas(Color? color = null)
    {
        Color resetColor = color ?? Color.white;
        
        for (int i = 0; i < colorMap.Length; i++)
        {
            colorMap[i] = resetColor;
        }
        
        UpdateTexture();
    }
    
    /// <summary>
    /// Called from button or menu to clear the canvas
    /// </summary>
    public void ClearCanvas()
    {
        ResetCanvas(Color.white);
    }
}