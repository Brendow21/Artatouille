using UnityEngine;

public class PaintCup : MonoBehaviour
{
    public Material targetMaterial; // assign in Inspector
    public float dumpThreshold = -0.9f; // Up vector threshold
    private Color mixedColor = Color.white;

    private Vector3 accumulatedColor = Vector3.zero; // Sum of all RGB values
    private int mixCount = 0;

    void Start()
    {
        ResetColor();
    }

    void Update()
    {
        // Reset when cup is flipped
        if (Vector3.Dot(transform.up, Vector3.up) < dumpThreshold)
        {
            ResetColor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Brush brush = other.GetComponent<Brush>();
        if (brush != null)
        {
            AddColor(brush.brushColor);
        }
    }

    private void AddColor(Color newColor)
    {
        Vector3 newRGB = new Vector3(newColor.r, newColor.g, newColor.b);
        accumulatedColor += newRGB;
        mixCount++;

        Vector3 averageRGB = accumulatedColor / mixCount;
        mixedColor = new Color(averageRGB.x, averageRGB.y, averageRGB.z);

        if (targetMaterial != null)
        {
            targetMaterial.color = mixedColor;
        }
    }

    private void ResetColor()
    {
        accumulatedColor = Vector3.zero;
        mixCount = 0;
        mixedColor = Color.white;

        if (targetMaterial != null)
        {
            targetMaterial.color = mixedColor;
        }
    }
}
