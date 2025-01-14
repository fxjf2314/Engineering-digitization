using TMPro;
using UnityEngine;

public class TogglePanel11 : MonoBehaviour
{
    public GameObject panel; // 引用新界面
    public TMP_Text completionText; // 引用用于显示完成提示的 TMP 文本
    public QuizManager11 quizManager11; // 引用 QuizManager11 以检查是否完成所有题目

    private void Start()
    {
        // 初始化 completionText 为隐藏状态
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // 检查 quizManager11 是否完成所有题目
        if (quizManager11 != null && quizManager11.IsGroupComplete())
        {
            // 如果完成，显示完成提示文本
            if (completionText != null)
            {
                completionText.text = "当前组别题目已回答完毕";
                completionText.gameObject.SetActive(true);

                // 3 秒后隐藏 completionText
                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            // 如果未完成，切换 panel 的显示状态
            if (panel != null)
            {
                panel.SetActive(!panel.activeSelf);
            }
        }
    }

    private void HideCompletionText()
    {
        // 隐藏 completionText
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}