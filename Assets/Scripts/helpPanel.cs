using UnityEngine;
using UnityEngine.UI;

public class ShowPanelOnClick : MonoBehaviour
{
    // 引用 Panel
    public GameObject panel;

    // 引用 Button
    public Button button;

    void Start()
    {
        // 确保 Panel 初始状态为隐藏
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // 绑定按钮点击事件
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button 未分配！请检查脚本设置。");
        }
    }

    // 按钮点击事件
    void OnButtonClick()
    {
        if (panel != null)
        {
            // 切换 Panel 的显示状态
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            Debug.LogError("Panel 未分配！请检查脚本设置。");
        }
    }
}
