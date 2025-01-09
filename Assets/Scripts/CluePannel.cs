using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // 引用新界面

    // 按钮点击时调用
    public void OnButtonClick()
    {
        // 切换界面的显示状态
        panel.SetActive(!panel.activeSelf);
    }
}