using UnityEngine;

public class changecolor : MonoBehaviour
{
    public Brush brush;

    private void OnTriggerEnter(Collider other)
    {
        string objName = other.gameObject.name.ToLower();
        
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
