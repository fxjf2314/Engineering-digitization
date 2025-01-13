using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons; // 五个按钮列表
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // 按钮和文本内容数组的映射
    public Dictionary<Button, GameObject> buttonToObjectMap = new Dictionary<Button, GameObject>(); // 按钮和物体的映射
    public List<TextMeshProUGUI> texts; // 所有需要更新的文本组件
    public GameObject defaultObject; // 默认显示的物体

    public List<GameObject> objects; // 手动绑定的物体列表

    private GameObject currentActiveObject; // 当前显示的物体
    private bool isInitialClick = true; // 是否是第一次点击

    private void Start()
    {
        // 初始化按钮和文本的映射
        InitializeButtonToTextMap();

        // 初始化按钮和物体的映射
        InitializeButtonToObjectMap();

        // 隐藏所有物体
        HideAllObjects();

        // 初始化默认显示的物体
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }

        // 隐藏所有 UI（除了五个按钮）
        HideAllUIExceptButtons();

        // 为每个按钮添加点击事件
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            }
            else
            {
                Debug.LogError("按钮未绑定！请检查 buttons 列表。");
            }
        }

        // 打印调试信息
        DebugButtonAndObjectInfo();
    }

    private void InitializeButtonToTextMap()
    {
        // 示例初始化，根据实际情况进行调整
        if (buttons.Count >= 5)
        {
            buttonToTextMap[buttons[0]] = new string[] { "转子与磁钢", "无", "无", "永磁转子" };
            buttonToTextMap[buttons[1]] = new string[] { "定子与机壳", "无", "无", "定子" };
            buttonToTextMap[buttons[2]] = new string[] { "减速齿轮", "无", "无", "减速齿轮" };
            buttonToTextMap[buttons[3]] = new string[] { "差速器", "无", "无", "差速器" };
            buttonToTextMap[buttons[4]] = new string[] { "电机控制器", "控制主板", "IGBT", "电机控制器" };
        }
        else
        {
            Debug.LogError("按钮数量不足！请确保 buttons 列表中有至少 5 个按钮。");
        }
    }

    private void InitializeButtonToObjectMap()
    {
        // 确保按钮和物体数量一致
        if (buttons.Count != objects.Count)
        {
            Debug.LogError("按钮和物体数量不匹配！请检查 buttons 和 objects 列表。");
            return;
        }

        // 将按钮与物体一一对应
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null && objects[i] != null)
            {
                buttonToObjectMap[buttons[i]] = objects[i];
            }
            else
            {
                Debug.LogError($"按钮 {i} 或物体 {i} 未绑定！");
            }
        }
    }

    private void HideAllObjects()
    {
        // 隐藏所有与按钮关联的物体
        foreach (var obj in buttonToObjectMap.Values)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void HideAllUIExceptButtons()
    {
        // 隐藏所有文本组件
        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
        }

        // 隐藏所有物体
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // 隐藏默认物体
        if (defaultObject != null)
        {
            defaultObject.SetActive(false);
        }
    }

    private void OnButtonClick(Button button)
    {
        Debug.Log($"按钮 {button.name} 被点击！");

        // 如果是第一次点击，显示所有文本和物体
        if (isInitialClick)
        {
            ShowAllUI();
            isInitialClick = false;
        }

        // 更新文本
        UpdateTexts(button);

        // 切换物体
        if (buttonToObjectMap.TryGetValue(button, out GameObject targetObject))
        {
            if (currentActiveObject != null)
            {
                currentActiveObject.SetActive(false); // 隐藏当前显示的物体
            }
            targetObject.SetActive(true); // 显示目标物体
            currentActiveObject = targetObject; // 更新当前显示的物体
        }
    }

    private void UpdateTexts(Button button)
    {
        // 根据按钮获取对应的文本内容数组并更新所有相关的文本组件
        if (buttonToTextMap.TryGetValue(button, out string[] textContents))
        {
            for (int i = 0; i < texts.Count; i++)
            {
                if (i < textContents.Length)
                {
                    texts[i].text = textContents[i];
                }
            }
        }
    }

    private void ShowAllUI()
    {
        // 显示所有文本组件
        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(true);
            }
        }

        // 显示默认物体
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }
    }

    private void DebugButtonAndObjectInfo()
    {
        // 打印按钮和物体的绑定信息
        Debug.Log("===== 按钮和物体绑定信息 =====");
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == null)
            {
                Debug.LogError($"按钮 {i} 未绑定！");
                continue;
            }

            if (buttonToObjectMap.TryGetValue(buttons[i], out GameObject obj))
            {
                if (obj == null)
                {
                    Debug.LogError($"按钮 {i} 对应的物体未绑定！");
                }
                else
                {
                    Debug.Log($"按钮 {i} 名称: {buttons[i].name}, 绑定物体: {obj.name}");
                }
            }
            else
            {
                Debug.LogError($"按钮 {i} 未在 buttonToObjectMap 中找到对应的物体！");
            }
        }
    }
}