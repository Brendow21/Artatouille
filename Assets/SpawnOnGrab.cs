using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnOnGrab : MonoBehaviour
{
    public GameObject objectToSpawn;      // Prefab to spawn
    public Transform spawnAttachPoint;    // Fixed spawn location

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (objectToSpawn != null && spawnAttachPoint != null)
        {
            // Instantiate and place at attach point
            GameObject spawned = Instantiate(objectToSpawn, spawnAttachPoint.position, spawnAttachPoint.rotation);

            // Optional: parent it to the attach point to stick in place
            spawned.transform.SetParent(spawnAttachPoint);
        }
    }
}
