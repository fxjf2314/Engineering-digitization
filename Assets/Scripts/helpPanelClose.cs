using UnityEngine;
using UnityEngine.UI;

public class ClosePanelOnClick : MonoBehaviour
{
    // ���� Panel
    public GameObject panel;

    // ���� Button
    public Button button;

    void Start()
    {
        // ȷ�� Panel ��ʼ״̬Ϊ��ʾ
        if (panel != null)
        {
            panel.SetActive(true);
        }

        // �󶨰�ť����¼�
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button δ���䣡����ű����á�");
        }
    }

    // ��ť����¼�
    void OnButtonClick()
    {
        if (panel != null)
        {
            // �ر� Panel
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel δ���䣡����ű����á�");
        }
    }
}
