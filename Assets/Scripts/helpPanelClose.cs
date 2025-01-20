using UnityEngine;
using UnityEngine.UI;

public class ClosePanelOnClick : MonoBehaviour
{
    // 引用 Panel
    public GameObject panel;

    // 引用 Button
    public Button button;

    void Start()
    {
        // 确保 Panel 初始状态为显示
        if (panel != null)
        {
            panel.SetActive(true);
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
            // 关闭 Panel
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel 未分配！请检查脚本设置。");
        }
    }
}
