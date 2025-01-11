using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间

public class GlobalQuizUIUpdater : MonoBehaviour
{
    // 使用 TextMeshProUGUI 组件
    public TextMeshProUGUI correctAnswersText;

    private void Start()
    {
        // 确保 GlobalQuizManager 已初始化
        if (GlobalQuizManager.Instance == null)
        {
            Debug.LogError("GlobalQuizManager 未初始化！请确保场景中有 GlobalQuizManager 对象。");
            return;
        }

        // 初始化 UI
        UpdateCorrectAnswersUI();
    }

    private void OnEnable()
    {
        // 订阅答题状态变化事件
        GlobalQuizManager.OnAnswerUpdated += UpdateCorrectAnswersUI;
    }

    private void OnDisable()
    {
        // 取消订阅答题状态变化事件
        GlobalQuizManager.OnAnswerUpdated -= UpdateCorrectAnswersUI;
    }

    // 更新 UI 显示答对题目数量
    private void UpdateCorrectAnswersUI()
    {
        if (correctAnswersText == null)
        {
            Debug.LogError("correctAnswersText 未赋值！请检查 UI 组件。");
            return;
        }

        // 获取全局答对题目数量
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // 更新 TMP 文本
        correctAnswersText.text = $"答对题目数量: {correctAnswers}";
    }
}