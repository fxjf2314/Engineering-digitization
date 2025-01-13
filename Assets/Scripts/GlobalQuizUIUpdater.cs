using UnityEngine;
using TMPro;

public class GlobalQuizUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI correctAnswersText;

    private void Awake()
    {
        if (FindObjectsOfType<GlobalQuizUIUpdater>().Length > 1)
        {
            Destroy(gameObject); 
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (GlobalQuizManager.Instance == null)
        {
            Debug.LogError("GlobalQuizManager δ��ʼ������ȷ���������� GlobalQuizManager ����");
            return;
        }
        UpdateCorrectAnswersUI();
    }

    private void OnEnable()
    {
        GlobalQuizManager.OnAnswerUpdated += UpdateCorrectAnswersUI;
    }

    private void OnDisable()
    {
        GlobalQuizManager.OnAnswerUpdated -= UpdateCorrectAnswersUI;
    }

    private void UpdateCorrectAnswersUI()
    {
        if (correctAnswersText == null)
        {
            correctAnswersText = GameObject.Find("CorrectAnswersText")?.GetComponent<TextMeshProUGUI>();
        }

        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        correctAnswersText.text = $"�����Ŀ����: {correctAnswers}";
    }
}