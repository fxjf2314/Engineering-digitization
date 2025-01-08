//拼接按钮的逻辑，奇数启用，偶数禁用
using UnityEngine;
using UnityEngine.UI;

public class ToggleDragManagers : MonoBehaviour
{
    public CameraDragRotate cameraDragRotate;

    public ButtonManager buttonManager;

    private int clickCount = 0;

    void Start()
    {
        if (buttonManager != null)
        {
            buttonManager.startSplicingButton.onClick.AddListener(OnStartSplicingButtonClick);
        }
    }

    private void OnStartSplicingButtonClick()
    {
        clickCount++;

        if (clickCount % 2 == 0)
        {
            DisableDragManagers();
        }
        else
        {
            EnableDragManagers();
        }

        if (cameraDragRotate != null)
        {
            cameraDragRotate.enabled = false;
        }

    }

    // 启用所有 DragManager 脚本
    private void EnableDragManagers()
    {
        DragManager[] dragManagers = FindObjectsOfType<DragManager>();
        foreach (DragManager dragManager in dragManagers)
        {
            dragManager.enabled = true;
        }
    }

    // 禁用所有 DragManager 脚本
    private void DisableDragManagers()
    {
        DragManager[] dragManagers = FindObjectsOfType<DragManager>();
        foreach (DragManager dragManager in dragManagers)
        {
            dragManager.enabled = false;
        }
    }
}