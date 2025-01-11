using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入 TMP 命名空间

public class QuizButton : MonoBehaviour
{
    public Button targetButton; // 目标按钮
    public GameObject panelCorrect20; // 答题正确数达到 20 时打开的 Panel
    public GameObject panelLessThan20; // 答题正确数不足 20 时打开的 Panel
    public GameObject floatingTextPrefab; // 悬浮字体预制体（TMP）
    public Transform canvasTransform; // Canvas 的 Transform，用于实例化悬浮字体

    private void Start()
    {
        // 为按钮添加点击事件
        targetButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // 获取全局进度
        float progress = GlobalQuizManager.Instance.GetGlobalProgress();

        // 获取全局正确答题数
        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        // 判断是否全部答完
        if (progress >= 1.0f) // 全部答完
        {
            // 判断正确答题数是否达到 20
            if (correctAnswers >= 20)
            {
                // 打开答题正确数达到 20 的 Panel
                panelCorrect20.SetActive(true);
            }
            else
            {
                // 打开答题正确数不足 20 的 Panel
                panelLessThan20.SetActive(true);
            }
        }
        else // 未全部答完
        {
            // 显示悬浮字体提示
            ShowFloatingText("回答完全部问题才可打开！");
        }
    }

    // 显示悬浮字体提示
    private void ShowFloatingText(string message)
    {
        // 实例化悬浮字体预制体
        GameObject floatingTextObj = Instantiate(floatingTextPrefab, canvasTransform);

        // 设置悬浮字体的文本内容
        TextMeshProUGUI floatingText = floatingTextObj.GetComponent<TextMeshProUGUI>();
        floatingText.text = message;

        // 3 秒后销毁悬浮字体
        Destroy(floatingTextObj, 3f);
    }
}