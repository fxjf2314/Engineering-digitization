using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入 TextMeshPro 命名空间

public class MultiTextButton : MonoBehaviour
{
    public Button targetButton; // 目标按钮
    public TMP_Text displayText; // 使用 TMP_Text 替代 Text
    public string[] textArray;  // 存储多段文字的数组

    private int currentIndex = 0; // 当前显示的文本索引

    private void Start()
    {
        // 初始化按钮文本
        targetButton.GetComponentInChildren<TMP_Text>().text = "确定";

        // 为按钮添加点击事件
        targetButton.onClick.AddListener(OnButtonClick);

        // 显示第一段文字
        if (textArray.Length > 0)
        {
            displayText.text = textArray[currentIndex];
        }
    }

    private void OnButtonClick()
    {
        // 检查是否已经显示到最后一段文字
        if (currentIndex >= textArray.Length - 1)
        {
            // 如果是最后一段文字，退出游戏
            QuitGame();
        }
        else
        {
            // 显示下一段文字
            currentIndex++;
            displayText.text = textArray[currentIndex];

            // 如果是最后一段文字，修改按钮文本为“退出”
            if (currentIndex == textArray.Length - 1)
            {
                targetButton.GetComponentInChildren<TMP_Text>().text = "退出";
            }
        }
    }

    // 退出游戏
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 在编辑器中停止播放
#else
        Application.Quit(); // 在打包后的游戏中退出
#endif
    }
}