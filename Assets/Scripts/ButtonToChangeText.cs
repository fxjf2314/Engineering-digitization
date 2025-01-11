using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons; // 按钮列表
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // 按钮和文本内容数组的映射
    public Dictionary<Button, GameObject> buttonToObjectMap = new Dictionary<Button, GameObject>(); // 按钮和物体的映射
    public List<TextMeshProUGUI> texts; // 所有需要更新的文本组件
    public GameObject defaultObject; // 默认显示的物体

    private GameObject currentActiveObject; // 当前显示的物体

    private void Start()
    {
        InitializeButtonToTextMap();
        InitializeButtonToObjectMap();

        // 初始化默认显示的物体
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }

        // 为每个按钮添加点击事件
        foreach (var button in buttonToTextMap.Keys)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void InitializeButtonToTextMap()
    {
        // 示例初始化，根据实际情况进行调整
        buttonToTextMap[buttons[0]] = new string[] { "转子与磁钢", "无", "无", "永磁转子" };
        buttonToTextMap[buttons[1]] = new string[] { "定子与机壳", "无", "无", "定子" };
        buttonToTextMap[buttons[2]] = new string[] { "减速齿轮", "无", "无", "减速齿轮" };
        buttonToTextMap[buttons[3]] = new string[] { "差速器", "无", "无", "差速器" };
        buttonToTextMap[buttons[4]] = new string[] { "电机控制器", "控制主板", "IGBT", "电机控制器" };
        // 添加更多按钮和对应的文本内容数组
    }

    private void InitializeButtonToObjectMap()
    {
        // 示例初始化，根据实际情况进行调整
        buttonToObjectMap[buttons[0]] = GameObject.Find("RotorAndMagnet"); // 替换为实际的物体名称
        buttonToObjectMap[buttons[1]] = GameObject.Find("StatorAndHousing"); // 替换为实际的物体名称
        buttonToObjectMap[buttons[2]] = GameObject.Find("Gear"); // 替换为实际的物体名称
        buttonToObjectMap[buttons[3]] = GameObject.Find("Differential"); // 替换为实际的物体名称
        buttonToObjectMap[buttons[4]] = GameObject.Find("MotorController"); // 替换为实际的物体名称
        // 添加更多按钮和对应的物体
    }

    private void OnButtonClick(Button button)
    {
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
}