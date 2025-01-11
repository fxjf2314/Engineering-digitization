using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� TMP �����ռ�

public class QuizButton : MonoBehaviour
{
    public Button targetButton; // Ŀ�갴ť
    public GameObject panelCorrect20; // ������ȷ���ﵽ 20 ʱ�򿪵� Panel
    public GameObject panelLessThan20; // ������ȷ������ 20 ʱ�򿪵� Panel
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

        // ��ȡȫ����ȷ������
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // �ж��Ƿ�ȫ������
        if (progress >= 1.0f) // ȫ������
        {
            // �ж���ȷ�������Ƿ�ﵽ 20
            if (correctAnswers >= 20)
            {
                // �򿪴�����ȷ���ﵽ 20 �� Panel
                panelCorrect20.SetActive(true);
            }
            else
            {
                // �򿪴�����ȷ������ 20 �� Panel
                panelLessThan20.SetActive(true);
            }
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