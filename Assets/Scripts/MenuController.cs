using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject instructionButtonsGroup;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private bool menuOpen = false;

    public void ToggleMenu()
    {
        menuOpen = !menuOpen;
        instructionButtonsGroup.SetActive(menuOpen);
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

}
