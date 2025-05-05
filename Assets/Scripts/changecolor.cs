using UnityEngine;

public class changecolor : MonoBehaviour
{
    public Brush brush;
    private bool isEraserActive = false;

    private void OnTriggerEnter(Collider other)
    {
        string objName = other.gameObject.name.ToLower();

        // If the eraser is selected, lock color to white and prevent changes
        if (objName == "eraser")
        {
            brush.brushColor = Color.white;
            isEraserActive = true; // Lock eraser mode
            return;
        }

        // Allow color changes only if the eraser is NOT active
        if (!isEraserActive)
        {
            switch (objName)
            {
                case "red":
                    brush.brushColor = Color.red;
                    break;
                case "green":
                    brush.brushColor = Color.green;
                    break;
                case "blue":
                    brush.brushColor = Color.blue;
                    break;
                case "yellow":
                    brush.brushColor = Color.yellow;
                    break;
                case "black":
                    brush.brushColor = Color.black;
                    break;
                case "white":
                    brush.brushColor = Color.white;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Call this function when switching away from eraser mode (e.g., user selects another tool).
    /// </summary>
    public void DisableEraser()
    {
        isEraserActive = false;
    }
}