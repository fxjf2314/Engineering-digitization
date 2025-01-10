using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� TMP �����ռ�

public class QuizButton : MonoBehaviour
{
    public Button targetButton; // Ŀ�갴ť
    public GameObject panel;    // ��Ҫ��ʾ�� Panel
    public GameObject floatingTextPrefab; // ��������Ԥ���壨TMP��
    public Transform canvasTransform; // Canvas �� Transform������ʵ������������

    private void Start()
    {
        // Ϊ��ť��ӵ���¼�
        targetButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // ��ȡȫ�ֽ���
        float progress = GlobalQuizManager.Instance.GetGlobalProgress();

        // �ж��Ƿ�ȫ������
        if (progress >= 1.0f) // ȫ������
        {
            // ��ʾ Panel
            panel.SetActive(true);
        }
        else // δȫ������
        {
            // ��ʾ����������ʾ
            ShowFloatingText("�ش���ȫ������ſɴ򿪣�");
        }
    }

    // ��ʾ����������ʾ
    private void ShowFloatingText(string message)
    {
        // ʵ������������Ԥ����
        GameObject floatingTextObj = Instantiate(floatingTextPrefab, canvasTransform);

        // ��������������ı�����
        TextMeshProUGUI floatingText = floatingTextObj.GetComponent<TextMeshProUGUI>();
        floatingText.text = message;

        // 3 ���������������
        Destroy(floatingTextObj, 3f);
    }
}