using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager11 : MonoBehaviour
{
    [Header("Question Data")]
    public QuestionData[] questionDatas; // 多个题目数据（ScriptableObject）

    [Header("UI Elements")]
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
    private bool isPanelDestroyed = false; // 标记 panel 是否已被销毁

    private void Awake()
    {
        // 确保实例在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SwitchQuestionGroup(0);
        nextButton.interactable = false;
    }

    // 切换题目组别
    public void SwitchQuestionGroup(int groupIndex)
    {
        currentQuestionData = questionDatas[groupIndex];
        currentGroupName = currentQuestionData.groupName;
        totalQuestions = currentQuestionData.questions.Length;

        // 初始化全局状态
        GlobalQuizManager.Instance.InitializeSceneStatus(sceneName, currentGroupName, totalQuestions);

        currentQuestionIndex = 0;
        LoadQuestion(currentQuestionIndex);
        UpdateProgress();
    }

    // 加载题目
    void LoadQuestion(int index)
    {
        QuestionData.Question question = currentQuestionData.questions[index];
        questionText.text = question.questionText;

        // 设置选项按钮
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = question.options[i];
            int optionIndex = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionClick(optionIndex, question.correctOption));
        }

        // 设置下一题按钮文本
        nextButton.GetComponentInChildren<TMP_Text>().text = (index == totalQuestions - 1) ? "提交" : "下一题";
    }

    // 处理选项点击
    void OnOptionClick(int selectedOption, int correctOption)
    {
        bool isCorrect = selectedOption == correctOption;

        // 更新全局状态
        GlobalQuizManager.Instance.UpdateSceneStatus(sceneName, currentGroupName, currentQuestionIndex, isCorrect);

        // 显示反馈
        feedbackText.text = isCorrect ? "正确！" : "错误！";

        // 禁用选项按钮
        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }

        // 启用下一题按钮
        nextButton.interactable = true;

        // 更新进度
        UpdateProgress();
    }

    // 更新进度
    void UpdateProgress()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress();
        fillImage.fillAmount = progress;
        progressText.text = $"{(int)(progress * 100)}%";
    }

    // 处理下一题点击
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

            // 禁用下一题按钮
            nextButton.interactable = false;

            // 清空反馈
            feedbackText.text = "";
        }
        else
        {
            feedbackText.text = $"当前组别答题完成！";
            nextButton.interactable = false;

            // 启动协程，延迟销毁 panel
            StartCoroutine(DestroyPanelAfterFeedback());
        }
    }

    // 检查当前组别是否完成
    public bool IsGroupComplete()
    {
        return currentQuestionIndex == totalQuestions - 1 && !nextButton.interactable;
    }

    // 关闭题目面板
    public void OnCloseButtonClick()
    {
        if (questionPanel != null && !isPanelDestroyed)
        {
            questionPanel.SetActive(false);
        }
    }

    // 协程：延迟销毁 panel
    private System.Collections.IEnumerator DestroyPanelAfterFeedback()
    {
        // 等待 2 秒
        yield return new WaitForSeconds(2f);

        // 销毁 panel
        if (questionPanel != null)
        {
            Destroy(questionPanel);
            isPanelDestroyed = true; // 标记 panel 已被销毁
        }
    }

    // 打开 panel 的方法
    public void OpenPanel()
    {
        if (!isPanelDestroyed && questionPanel != null)
        {
            questionPanel.SetActive(true);
        }
    }
}