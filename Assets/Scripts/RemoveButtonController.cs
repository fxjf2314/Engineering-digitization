//没用到，但是先别删
using UnityEngine;
using UnityEngine.UI;

public class RemoveButtonController : MonoBehaviour
{
    // 引用 ButtonManager
    public ButtonManager buttonManager;

    void Start()
    {
        // 绑定 ButtonManager 的事件
        if (buttonManager != null)
        {
            buttonManager.removeButton.onClick.AddListener(OnRemoveButtonClick);
        }
    }

    // RemoveButton 按钮点击事件
    private void OnRemoveButtonClick()
    {
        // 隐藏 RemoveButton
        gameObject.SetActive(false);

    }
}
