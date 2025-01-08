using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons;
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // 按钮和文本内容数组的映射
    public List<TextMeshProUGUI> texts; // 所有需要更新的文本组件

    private void Start()
    {
        InitializeButtonToTextMap();
        // 为每个按钮添加点击事件
        foreach (var button in buttonToTextMap.Keys)
        {
            button.onClick.AddListener(() => UpdateTexts(button));
        }
    }

    private void InitializeButtonToTextMap()
    {
        // 示例初始化，根据实际情况进行调整
        buttonToTextMap[buttons[0]] = new string[] { "转子与磁钢", "无","无","永磁转子"};
        buttonToTextMap[buttons[1]] = new string[] { "定子与机壳", "无","无","定子"};
        buttonToTextMap[buttons[2]] = new string[] { "减速齿轮", "无","无","减速齿轮" };
        buttonToTextMap[buttons[3]] = new string[] { "差速器", "无","无","差速器" };
        buttonToTextMap[buttons[4]] = new string[] { "电机控制器", "控制主板", "IGBT","电机控制器" };
        // 添加更多按钮和对应的文本内容数组
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