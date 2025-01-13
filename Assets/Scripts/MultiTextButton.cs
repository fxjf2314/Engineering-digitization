using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiTextButton : MonoBehaviour
{
    public Button targetButton; // 目标按钮
    public TMP_Text displayText; // 使用 TMP_Text 替代 Text
    public string[] textArray;  // 存储多段文字的数组

    private int currentIndex = 0; // 当前显示的文本索引

    private void Start()
    {
        targetButton.GetComponentInChildren<TMP_Text>().text = "确定";

        targetButton.onClick.AddListener(OnButtonClick);

        if (textArray.Length > 0)
        {
            displayText.text = textArray[currentIndex];
        }
    }

    private void OnButtonClick()
    {
        if (currentIndex >= textArray.Length - 1)
        {
            QuitGame();
        }
        else
        {
            currentIndex++;
            displayText.text = textArray[currentIndex];
            if (currentIndex == textArray.Length - 1)
            {
                targetButton.GetComponentInChildren<TMP_Text>().text = "退出";
            }
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // 在打包后的游戏中退出
#endif
    }
}