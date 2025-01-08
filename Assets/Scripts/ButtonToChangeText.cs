using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons;
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // ��ť���ı����������ӳ��
    public List<TextMeshProUGUI> texts; // ������Ҫ���µ��ı����

    private void Start()
    {
        InitializeButtonToTextMap();
        // Ϊÿ����ť��ӵ���¼�
        foreach (var button in buttonToTextMap.Keys)
        {
            button.onClick.AddListener(() => UpdateTexts(button));
        }
    }

    private void InitializeButtonToTextMap()
    {
        // ʾ����ʼ��������ʵ��������е���
        buttonToTextMap[buttons[0]] = new string[] { "ת����Ÿ�", "��","��","����ת��"};
        buttonToTextMap[buttons[1]] = new string[] { "���������", "��","��","����"};
        buttonToTextMap[buttons[2]] = new string[] { "���ٳ���", "��","��","���ٳ���" };
        buttonToTextMap[buttons[3]] = new string[] { "������", "��","��","������" };
        buttonToTextMap[buttons[4]] = new string[] { "���������", "��������", "IGBT","���������" };
        // ��Ӹ��ఴť�Ͷ�Ӧ���ı���������
    }
    private void UpdateTexts(Button button)
    {
        // ���ݰ�ť��ȡ��Ӧ���ı��������鲢����������ص��ı����
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