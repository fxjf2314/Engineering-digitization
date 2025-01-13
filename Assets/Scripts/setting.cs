using UnityEngine;
using UnityEngine.UI;

public class setting : MonoBehaviour
{
    public GameObject panel; // 需要显示/隐藏的 UI 面板
    public Animator panelAnimator; // 面板的动画控制器

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(TogglePanelVisibility);
        }
    }

    private void TogglePanelVisibility()
    {
        if (panel != null)
        {

            bool isPanelActive = !panel.activeSelf;
            panel.SetActive(isPanelActive);

            // 播放动画
            if (panelAnimator != null)
            {
                panelAnimator.SetBool("IsOpen", isPanelActive);
            }
        }
       
    }
}