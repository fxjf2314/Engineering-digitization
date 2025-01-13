using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager11 : MonoBehaviour
{
    // 单例实例
    public static QuizManager11 Instance;

    public QuestionData[] questionDatas; // 多个题目数据（ScriptableObject）
    public Image fillImage;              // 圆环形进度条的 Fill 组件
    public TMP_Text progressText;        // 进度百分比文本
    public GameObject questionPanel;     // 题目面板
    public TMP_Text questionText;        // 题目内容
    public Button[] optionButtons;       // 选项按钮
    public TMP_Text feedbackText;        // 反馈提示
    public Button nextButton;            // 下一题按钮

    private int totalQuestions;          // 当前组别的总题目数
    private int currentQuestionIndex;    // 当前题目索引
    private string sceneName;            // 当前场景名称
    private string currentGroupName;     // 当前题目组别名称
    private QuestionData currentQuestionData; // 当前题目数据

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
        questionText.text = question.questionText; // 设置题目内容

        // 设置选项内容
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < question.options.Length)
            {
                optionButtons[i].gameObject.SetActive(true); // 启用按钮
                optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => OnOptionClick(optionIndex, question.correctOption));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false); // 隐藏多余按钮
            }
        }

        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "提交" : "下一题";

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

        feedbackText.text = isCorrect ? "正确！" : "错误！";

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
           
            feedbackText.text = $"当前组别答题完成！";
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