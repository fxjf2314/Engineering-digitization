using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public GameObject panel; // ��Ҫ��ʾ/���ص� UI ���
    public Animator panelAnimator; // ���Ķ���������

    private void Start()
    {
        // ȷ������ʼ״̬Ϊ����
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // ��ȡ��ť������󶨵���¼�
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(TogglePanelVisibility);
        }
        else
        {
            Debug.LogWarning("TogglePanel script is not attached to a Button!");
        }
    }

    private void TogglePanelVisibility()
    {
        if (panel != null)
        {
            // �л�������ʾ״̬
            bool isPanelActive = !panel.activeSelf;
            panel.SetActive(isPanelActive);

            // ���Ŷ���
            if (panelAnimator != null)
            {
                panelAnimator.SetBool("IsOpen", isPanelActive);
            }
        }
        else
        {
            Debug.LogWarning("Panel is not assigned in the TogglePanel script!");
        }
    }
}