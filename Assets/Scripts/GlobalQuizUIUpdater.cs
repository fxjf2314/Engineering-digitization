using UnityEngine;
using TMPro;

public class GlobalQuizUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI correctAnswersText;

    private void Awake()
    {
        // 单例模式，确保只有一个 GlobalQuizUIUpdater 实例
        if (FindObjectsOfType<GlobalQuizUIUpdater>().Length > 1)
        {
            Destroy(gameObject); // 如果已经存在实例，销毁当前对象
            return;
        }

        DontDestroyOnLoad(gameObject); // 跨场景保留
    }

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

    private void UpdateCorrectAnswersUI()
    {
        // 动态查找 correctAnswersText 对象
        if (correctAnswersText == null)
        {
            correctAnswersText = GameObject.Find("CorrectAnswersText")?.GetComponent<TextMeshProUGUI>();
            if (correctAnswersText == null)
            {
                Debug.LogError("未找到 correctAnswersText 对象！请确保场景中有名为 CorrectAnswersText 的 TMP 文本对象。");
                return;
            }
        }

        // 获取全局答对题目数量
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // 更新 TMP 文本
        correctAnswersText.text = $"答对题目数量: {correctAnswers}";
    }
}