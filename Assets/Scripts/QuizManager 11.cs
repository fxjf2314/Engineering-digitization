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
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject); // 销毁重复实例
        }
    }

    private void Start()
    {
        if (questionDatas == null || questionDatas.Length == 0)
        {
            Debug.LogError("未设置题目数据！");
            return;
        }

        // 获取当前场景的名称
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 默认加载第一个题目组
        SwitchQuestionGroup(0);
        
        // 初始化下一题按钮状态
        nextButton.interactable = false;
    }

    // 手动切换题目组
    public void SwitchQuestionGroup(int groupIndex)
    {

        // 检查 questionDatas 是否分配
        if (questionDatas == null || questionDatas.Length == 0)
        {
            Debug.LogError("未设置题目数据！");
            return;
        }

        // 检查 groupIndex 是否有效
        if (groupIndex < 0 || groupIndex >= questionDatas.Length)
        {
            Debug.LogError("题目组索引超出范围！");
            return;
        }

        // 设置当前题目组
        currentQuestionData = questionDatas[groupIndex];

        // 检查 currentQuestionData 是否分配
        if (currentQuestionData == null)
        {
            Debug.LogError("当前题目数据未初始化！");
            return;
        }

        // 检查 groupName 是否分配
        if (string.IsNullOrEmpty(currentQuestionData.groupName))
        {
            Debug.LogError("当前题目组名称未设置！");
            return;
        }

        // 检查 questions 是否分配
        if (currentQuestionData.questions == null || currentQuestionData.questions.Length == 0)
        {
            Debug.LogError("当前题目组的问题列表为空！");
            return;
        }

        currentGroupName = currentQuestionData.groupName; // 使用 groupName 标识组别
        totalQuestions = currentQuestionData.questions.Length;
        Debug.Log($"总题目数：{totalQuestions}");


        // 检查 GlobalQuizManager.Instance 是否分配
        if (GlobalQuizManager.Instance == null)
        {
            Debug.LogError("GlobalQuizManager 未初始化！");
            return;
        }

        // 初始化当前场景和组别的答题状态
        GlobalQuizManager.Instance.InitializeSceneStatus(sceneName, currentGroupName, totalQuestions);

        // 加载当前组别的第一题
        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);
        UpdateProgress();
        Debug.Log($"当前题目组：{currentGroupName}，题目数量：{currentQuestionData.questions.Length}");
        for (int i = 0; i < currentQuestionData.questions.Length; i++)
        {
            Debug.Log($"题目 {i + 1}: {currentQuestionData.questions[i].questionText}");
        }
    }

    // 加载题目
    void LoadQuestion(int index)
    {
        if (index < 0 || index >= currentQuestionData.questions.Length)
        {
            Debug.LogError("题目索引超出范围！");
            return;
        }

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

        // 更新下一题按钮文字
        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "提交" : "下一题";

        // 清空反馈提示
        feedbackText.text = "";

        // 启用选项按钮
        foreach (var button in optionButtons)
        {
            button.interactable = true;
        }

        // 禁用下一题按钮，直到选择答案
        nextButton.interactable = false;
    }

    // 选项点击事件
    void OnOptionClick(int selectedOption, int correctOption)
    {
        bool isCorrect = selectedOption == correctOption;

        // 更新当前场景和组别的答题状态
        GlobalQuizManager.Instance.UpdateSceneStatus(sceneName, currentGroupName, currentQuestionIndex, isCorrect);

        // 显示反馈提示
        feedbackText.text = isCorrect ? "正确！" : "错误！";

        // 禁用选项按钮，防止重复点击
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }

        // 启用下一题按钮
        nextButton.interactable = true;

        // 更新进度条
        UpdateProgress();
    }
    // 更新进度条
    void UpdateProgress()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress(); // 获取全局进度
        fillImage.fillAmount = progress; // 更新圆环形进度条
        progressText.text = $"{(int)(progress * 100)}%"; // 更新百分比文本
    }


    // 下一题按钮点击事件
    public void OnNextButtonClick()
    {
        if (currentQuestionIndex < totalQuestions - 1)
        {
            currentQuestionIndex++;
            LoadQuestion(currentQuestionIndex);

            // 启用选项按钮
            foreach (var button in optionButtons)
            {
                button.interactable = true;
            }

            // 禁用下一题按钮，直到选择答案
            nextButton.interactable = false;

            // 清空反馈提示
            feedbackText.text = "";
        }
        else
        {
            // 提交逻辑
            feedbackText.text = $"当前组别答题完成！";
            nextButton.interactable = false; // 禁用按钮
        }
    }
    public bool IsGroupComplete()
    {
        // 如果当前题目索引是最后一题，并且下一题按钮被禁用（表示已完成）
        return currentQuestionIndex == totalQuestions - 1 && !nextButton.interactable;
    }
    // 关闭题目面板
    public void OnCloseButtonClick()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(false); // 隐藏 Panel
        }
    }
}