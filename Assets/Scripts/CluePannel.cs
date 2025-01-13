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
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        if (quizManager != null && (quizManager.IsGroupComplete() || quizManager11.IsGroupComplete()))
        {
            if (completionText != null)
            {
                completionText.text = "当前组别题目已回答完毕";
                completionText.gameObject.SetActive(true);

                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    private void HideCompletionText()
    {
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}