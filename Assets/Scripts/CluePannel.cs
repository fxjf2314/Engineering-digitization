using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // �����½���

    // ��ť���ʱ����
    public void OnButtonClick()
    {
        // �л��������ʾ״̬
        panel.SetActive(!panel.activeSelf);
    }
}