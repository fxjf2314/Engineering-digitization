using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager11 : MonoBehaviour
{
    // ����ʵ��
    public static QuizManager11 Instance;

    public QuestionData[] questionDatas; // �����Ŀ���ݣ�ScriptableObject��
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

    private void Awake()
    {
        // ����ģʽ��ʼ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �糡������
        }
        else
        {
            Destroy(gameObject); // �����ظ�ʵ��
        }
    }

    private void Start()
    {
        if (questionDatas == null || questionDatas.Length == 0)
        {
            Debug.LogError("δ������Ŀ���ݣ�");
            return;
        }

        // ��ȡ��ǰ����������
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Ĭ�ϼ��ص�һ����Ŀ��
        SwitchQuestionGroup(0);
        
        // ��ʼ����һ�ⰴť״̬
        nextButton.interactable = false;
    }

    // �ֶ��л���Ŀ��
    public void SwitchQuestionGroup(int groupIndex)
    {

        // ��� questionDatas �Ƿ����
        if (questionDatas == null || questionDatas.Length == 0)
        {
            Debug.LogError("δ������Ŀ���ݣ�");
            return;
        }

        // ��� groupIndex �Ƿ���Ч
        if (groupIndex < 0 || groupIndex >= questionDatas.Length)
        {
            Debug.LogError("��Ŀ������������Χ��");
            return;
        }

        // ���õ�ǰ��Ŀ��
        currentQuestionData = questionDatas[groupIndex];

        // ��� currentQuestionData �Ƿ����
        if (currentQuestionData == null)
        {
            Debug.LogError("��ǰ��Ŀ����δ��ʼ����");
            return;
        }

        // ��� groupName �Ƿ����
        if (string.IsNullOrEmpty(currentQuestionData.groupName))
        {
            Debug.LogError("��ǰ��Ŀ������δ���ã�");
            return;
        }

        // ��� questions �Ƿ����
        if (currentQuestionData.questions == null || currentQuestionData.questions.Length == 0)
        {
            Debug.LogError("��ǰ��Ŀ��������б�Ϊ�գ�");
            return;
        }

        currentGroupName = currentQuestionData.groupName; // ʹ�� groupName ��ʶ���
        totalQuestions = currentQuestionData.questions.Length;
        Debug.Log($"����Ŀ����{totalQuestions}");


        // ��� GlobalQuizManager.Instance �Ƿ����
        if (GlobalQuizManager.Instance == null)
        {
            Debug.LogError("GlobalQuizManager δ��ʼ����");
            return;
        }

        // ��ʼ����ǰ���������Ĵ���״̬
        GlobalQuizManager.Instance.InitializeSceneStatus(sceneName, currentGroupName, totalQuestions);

        // ���ص�ǰ���ĵ�һ��
        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);
        UpdateProgress();
        Debug.Log($"��ǰ��Ŀ�飺{currentGroupName}����Ŀ������{currentQuestionData.questions.Length}");
        for (int i = 0; i < currentQuestionData.questions.Length; i++)
        {
            Debug.Log($"��Ŀ {i + 1}: {currentQuestionData.questions[i].questionText}");
        }
    }

    // ������Ŀ
    void LoadQuestion(int index)
    {
        if (index < 0 || index >= currentQuestionData.questions.Length)
        {
            Debug.LogError("��Ŀ����������Χ��");
            return;
        }

        QuestionData.Question question = currentQuestionData.questions[index];
        questionText.text = question.questionText; // ������Ŀ����

        // ����ѡ������
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < question.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true); // ���ð�ť
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionClick(optionIndex, question.correctOption));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false); // ���ض��ఴť
            }
        }

        // ������һ�ⰴť����
        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "�ύ" : "��һ��";

        // ��շ�����ʾ
        feedbackText.text = "";

        // ����ѡ�ť
        foreach (var button in optionButtons)
        {
            button.interactable = true;
        }

        // ������һ�ⰴť��ֱ��ѡ���
        nextButton.interactable = false;
    }

    // ѡ�����¼�
    void OnOptionClick(int selectedOption, int correctOption)
    {
        bool isCorrect = selectedOption == correctOption;

        // ���µ�ǰ���������Ĵ���״̬
        GlobalQuizManager.Instance.UpdateSceneStatus(sceneName, currentGroupName, currentQuestionIndex, isCorrect);

        // ��ʾ������ʾ
        feedbackText.text = isCorrect ? "��ȷ��" : "����";

        // ����ѡ�ť����ֹ�ظ����
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }

        // ������һ�ⰴť
        nextButton.interactable = true;

        // ���½�����
        UpdateProgress();
    }
    // ���½�����
    void UpdateProgress()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress(); // ��ȡȫ�ֽ���
        fillImage.fillAmount = progress; // ����Բ���ν�����
        progressText.text = $"{(int)(progress * 100)}%"; // ���°ٷֱ��ı�
    }


    // ��һ�ⰴť����¼�
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

            // ������һ�ⰴť��ֱ��ѡ���
            nextButton.interactable = false;

            // ��շ�����ʾ
            feedbackText.text = "";
        }
        else
        {
            // �ύ�߼�
            feedbackText.text = $"��ǰ��������ɣ�";
            nextButton.interactable = false; // ���ð�ť
        }
    }
    public bool IsGroupComplete()
    {
        // �����ǰ��Ŀ���������һ�⣬������һ�ⰴť�����ã���ʾ����ɣ�
        return currentQuestionIndex == totalQuestions - 1 && !nextButton.interactable;
    }
    // �ر���Ŀ���
    public void OnCloseButtonClick()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(false); // ���� Panel
        }
    }
}