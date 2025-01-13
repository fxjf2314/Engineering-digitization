using UnityEngine;
using TMPro;

public class GlobalQuizUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI correctAnswersText;

    private void Awake()
    {
        // ����ģʽ��ȷ��ֻ��һ�� GlobalQuizUIUpdater ʵ��
        if (FindObjectsOfType<GlobalQuizUIUpdater>().Length > 1)
        {
            Destroy(gameObject); // ����Ѿ�����ʵ�������ٵ�ǰ����
            return;
        }

        DontDestroyOnLoad(gameObject); // �糡������
    }

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

    private void UpdateCorrectAnswersUI()
    {
        // ��̬���� correctAnswersText ����
        if (correctAnswersText == null)
        {
            correctAnswersText = GameObject.Find("CorrectAnswersText")?.GetComponent<TextMeshProUGUI>();
            if (correctAnswersText == null)
            {
                Debug.LogError("δ�ҵ� correctAnswersText ������ȷ������������Ϊ CorrectAnswersText �� TMP �ı�����");
                return;
            }
        }

        // ��ȡȫ�ִ����Ŀ����
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // ���� TMP �ı�
        correctAnswersText.text = $"�����Ŀ����: {correctAnswers}";
    }
}