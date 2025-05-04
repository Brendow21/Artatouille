using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireplace"))
        {
            Destroy(gameObject);
        }
    }
}