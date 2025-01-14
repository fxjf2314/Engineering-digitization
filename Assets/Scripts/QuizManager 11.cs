using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager11 : MonoBehaviour
{
    [Header("Question Data")]
    public QuestionData[] questionDatas; // �����Ŀ���ݣ�ScriptableObject��

    [Header("UI Elements")]
    public Image fillImage;              // Բ���ν������� Fill ���
    public TMP_Text progressText;        // ���Ȱٷֱ��ı�
    public GameObject questionPanel;     // ��Ŀ���
    public TMP_Text questionText;        // ��Ŀ����
    public Button[] optionButtons;       // ѡ�ť
    public TMP_Text feedbackText;        // ������ʾ
    public Button nextButton;            // ��һ�ⰴť

    private int totalQuestions;          // ��ǰ��������Ŀ��
    private int currentQuestionIndex;    // ��ǰ��Ŀ����
    private string sceneName;            // ��ǰ��������
    private string currentGroupName;     // ��ǰ��Ŀ�������
    private QuestionData currentQuestionData; // ��ǰ��Ŀ����
    private bool isPanelDestroyed = false; // ��� panel �Ƿ��ѱ�����

    private void Awake()
    {
        // ȷ��ʵ���ڳ����л�ʱ��������
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SwitchQuestionGroup(0);
        nextButton.interactable = false;
    }

    // �л���Ŀ���
    public void SwitchQuestionGroup(int groupIndex)
    {
        currentQuestionData = questionDatas[groupIndex];
        currentGroupName = currentQuestionData.groupName;
        totalQuestions = currentQuestionData.questions.Length;

        // ��ʼ��ȫ��״̬
        GlobalQuizManager.Instance.InitializeSceneStatus(sceneName, currentGroupName, totalQuestions);

        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);
        UpdateProgress();
    }

    // ������Ŀ
    void LoadQuestion(int index)
    {
        QuestionData.Question question = currentQuestionData.questions[index];
        questionText.text = question.questionText;

        // ����ѡ�ť
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
            int optionIndex = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionClick(optionIndex, question.correctOption));
        }

        // ������һ�ⰴť�ı�
        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "�ύ" : "��һ��";
    }

    // ����ѡ����
    void OnOptionClick(int selectedOption, int correctOption)
    {
        bool isCorrect = selectedOption == correctOption;

        // ����ȫ��״̬
        GlobalQuizManager.Instance.UpdateSceneStatus(sceneName, currentGroupName, currentQuestionIndex, isCorrect);

        // ��ʾ����
        feedbackText.text = isCorrect ? "��ȷ��" : "����";

        // ����ѡ�ť
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }

        // ������һ�ⰴť
        nextButton.interactable = true;

        // ���½���
        UpdateProgress();
    }

    // ���½���
    void UpdateProgress()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress();
        fillImage.fillAmount = progress;
        progressText.text = $"{(int)(progress * 100)}%";
    }

    // ������һ����
    public void OnNextButtonClick()
    {
        if (currentQuestionIndex < totalQuestions - 1)
        {
            currentQuestionIndex++;
            LoadQuestion(currentQuestionIndex);

            // ����ѡ�ť
            foreach (var button in optionButtons)
            {
                button.interactable = true;
            }

            // ������һ�ⰴť
            nextButton.interactable = false;

            // ��շ���
            feedbackText.text = "";
        }
        else
        {
            feedbackText.text = $"��ǰ��������ɣ�";
            nextButton.interactable = false;

            // ����Э�̣��ӳ����� panel
            StartCoroutine(DestroyPanelAfterFeedback());
        }
    }

    // ��鵱ǰ����Ƿ����
    public bool IsGroupComplete()
    {
        return currentQuestionIndex == totalQuestions - 1 && !nextButton.interactable;
    }

    // �ر���Ŀ���
    public void OnCloseButtonClick()
    {
        if (questionPanel != null && !isPanelDestroyed)
        {
            questionPanel.SetActive(false);
        }
    }

    // Э�̣��ӳ����� panel
    private System.Collections.IEnumerator DestroyPanelAfterFeedback()
    {
        // �ȴ� 2 ��
        yield return new WaitForSeconds(2f);

        // ���� panel
        if (questionPanel != null)
        {
            Destroy(questionPanel);
            isPanelDestroyed = true; // ��� panel �ѱ�����
        }
    }

    // �� panel �ķ���
    public void OpenPanel()
    {
        if (!isPanelDestroyed && questionPanel != null)
        {
            questionPanel.SetActive(true);
        }
    }
}