using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public GameObject panel; // 需要显示/隐藏的 UI 面板
    public Animator panelAnimator; // 面板的动画控制器

    private void Start()
    {
        // 确保面板初始状态为隐藏
        if (panel != null)
        {
            panel.SetActive(false);
        }

        // 获取按钮组件并绑定点击事件
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(TogglePanelVisibility);
        }
        else
        {
            Debug.LogWarning("TogglePanel script is not attached to a Button!");
        }
    }

    private void TogglePanelVisibility()
    {
        if (panel != null)
        {
            // 切换面板的显示状态
            bool isPanelActive = !panel.activeSelf;
            panel.SetActive(isPanelActive);

            // 播放动画
            if (panelAnimator != null)
            {
                panelAnimator.SetBool("IsOpen", isPanelActive);
            }
        }
        else
        {
            Debug.LogWarning("Panel is not assigned in the TogglePanel script!");
        }
    }
}