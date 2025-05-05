using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public GameObject instructionButtonsGroup;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;

    private bool menuOpen = false;

    public void ToggleMenu()
    {
        if(menuOpen) {
            ClosePanel(panel1);
            ClosePanel(panel2);
            ClosePanel(panel3);
        }
        menuOpen = !menuOpen;
        instructionButtonsGroup.SetActive(menuOpen);
    }

    public void TogglePanel(GameObject panel)
    {
        if(panel.activeSelf) {
            panel.SetActive(false);
            return;
        }
        CloseAllPanels();
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void CloseAllPanels() {
        ClosePanel(panel1);
        ClosePanel(panel2);
        ClosePanel(panel3);
    }

}
