using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDisplay : MonoBehaviour
{
    public GameObject[] invisiblePanels;
    public GameObject visiblePanel;

    public void Display()
    {
        foreach (var panel in invisiblePanels)
        {
            panel.gameObject.SetActive(false);
        }
        visiblePanel.gameObject.SetActive(true);
    }
}
