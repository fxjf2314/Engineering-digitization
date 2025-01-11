using UnityEngine;
using TMPro;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // 引用新界面
    public TMP_Text completionText; // 引用用于显示完成提示的 TMP 文本
    public QuizManager quizManager; // 引用 QuizManager 以检查是否完成所有题目
    public QuizManager11 quizManager11;
    private void Start()
    {
        // 初始化完成提示文本为隐藏状态
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    // 按钮点击时调用
    public void OnButtonClick()
    {
        // 检查是否所有题目已完成
        if (quizManager != null && (quizManager.IsGroupComplete()||quizManager11.IsGroupComplete()))
        {
            // 显示完成提示文本
            if (completionText != null)
            {
                completionText.text = "当前组别题目已回答完毕";
                completionText.gameObject.SetActive(true);

                // 3 秒后隐藏提示文本
                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            // 切换界面的显示状态
            panel.SetActive(!panel.activeSelf);
        }
    }

    // 隐藏完成提示文本
    private void HideCompletionText()
    {
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}