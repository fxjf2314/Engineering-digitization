using UnityEngine;
using UnityEngine.UI;

public class ShowPanelOnClick : MonoBehaviour
{
    // ���� Panel
    public GameObject panel;

    // ���� Button
    public Button button;

    void Start()
    {
        // ȷ�� Panel ��ʼ״̬Ϊ����
        if (panel != null)
        {
            panel.SetActive(false);
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
            // �л� Panel ����ʾ״̬
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            Debug.LogError("Panel δ���䣡����ű����á�");
        }
    }
}
