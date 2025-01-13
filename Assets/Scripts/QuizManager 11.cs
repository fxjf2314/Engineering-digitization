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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        SwitchQuestionGroup(0);
      
        nextButton.interactable = false;
    }

    public void SwitchQuestionGroup(int groupIndex)
    {

        currentQuestionData = questionDatas[groupIndex];
        currentGroupName = currentQuestionData.groupName; 
        totalQuestions = currentQuestionData.questions.Length;
 
        GlobalQuizManager.Instance.InitializeSceneStatus(sceneName, currentGroupName, totalQuestions);

        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);
        UpdateProgress();
    }

    void LoadQuestion(int index)
    {
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

        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "�ύ" : "��һ��";

        feedbackText.text = "";

        foreach (var button in optionButtons)
        {
            button.interactable = true;
        }

        nextButton.interactable = false;
    }

    void OnOptionClick(int selectedOption, int correctOption)
    {
        bool isCorrect = selectedOption == correctOption;

        GlobalQuizManager.Instance.UpdateSceneStatus(sceneName, currentGroupName, currentQuestionIndex, isCorrect);

        feedbackText.text = isCorrect ? "��ȷ��" : "����";

        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }
        nextButton.interactable = true;

        UpdateProgress();
    }

    void UpdateProgress()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress(); 
        fillImage.fillAmount = progress; 
        progressText.text = $"{(int)(progress * 100)}%"; 
    }

    public void OnNextButtonClick()
    {
        if (currentQuestionIndex < totalQuestions - 1)
        {
            currentQuestionIndex++;
            LoadQuestion(currentQuestionIndex);

            foreach (var button in optionButtons)
            {
                button.interactable = true;
            }

            nextButton.interactable = false;

            feedbackText.text = "";
        }
        else
        {
           
            feedbackText.text = $"��ǰ��������ɣ�";
            nextButton.interactable = false; 
        }
    }
    public bool IsGroupComplete()
    {
        return currentQuestionIndex == totalQuestions - 1 && !nextButton.interactable;
    }
    public void OnCloseButtonClick()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(false); 
        }
    }
}