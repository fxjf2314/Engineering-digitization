using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public GameObject panel; // ��Ҫ��ʾ/���ص� UI ���
    public Animator panelAnimator; // ���Ķ���������

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(TogglePanelVisibility);
        }
    }

    private void TogglePanelVisibility()
    {
        if (panel != null)
        {

            bool isPanelActive = !panel.activeSelf;
            panel.SetActive(isPanelActive);

            // ���Ŷ���
            if (panelAnimator != null)
            {
                panelAnimator.SetBool("IsOpen", isPanelActive);
            }
        }
       
    }
}