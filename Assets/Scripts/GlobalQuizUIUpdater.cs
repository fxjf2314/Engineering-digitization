using UnityEngine;
using TMPro; // ���� TextMeshPro �����ռ�

public class GlobalQuizUIUpdater : MonoBehaviour
{
    // ʹ�� TextMeshProUGUI ���
    public TextMeshProUGUI correctAnswersText;

    private void Start()
    {
        // ȷ�� GlobalQuizManager �ѳ�ʼ��
        if (GlobalQuizManager.Instance == null)
        {
            Debug.LogError("GlobalQuizManager δ��ʼ������ȷ���������� GlobalQuizManager ����");
            return;
        }

        // ��ʼ�� UI
        UpdateCorrectAnswersUI();
    }

    private void OnEnable()
    {
        // ���Ĵ���״̬�仯�¼�
        GlobalQuizManager.OnAnswerUpdated += UpdateCorrectAnswersUI;
    }

    private void OnDisable()
    {
        // ȡ�����Ĵ���״̬�仯�¼�
        GlobalQuizManager.OnAnswerUpdated -= UpdateCorrectAnswersUI;
    }

    // ���� UI ��ʾ�����Ŀ����
    private void UpdateCorrectAnswersUI()
    {
        if (correctAnswersText == null)
        {
            Debug.LogError("correctAnswersText δ��ֵ������ UI �����");
            return;
        }

        // ��ȡȫ�ִ����Ŀ����
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // ���� TMP �ı�
        correctAnswersText.text = $"�����Ŀ����: {correctAnswers}";
    }
}