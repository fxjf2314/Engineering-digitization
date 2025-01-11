using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons; // ��ť�б�
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // ��ť���ı����������ӳ��
    public Dictionary<Button, GameObject> buttonToObjectMap = new Dictionary<Button, GameObject>(); // ��ť�������ӳ��
    public List<TextMeshProUGUI> texts; // ������Ҫ���µ��ı����
    public GameObject defaultObject; // Ĭ����ʾ������

    private GameObject currentActiveObject; // ��ǰ��ʾ������

    private void Start()
    {
        InitializeButtonToTextMap();
        InitializeButtonToObjectMap();

        // ��ʼ��Ĭ����ʾ������
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }

        // Ϊÿ����ť��ӵ���¼�
        foreach (var button in buttonToTextMap.Keys)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    private void InitializeButtonToTextMap()
    {
        // ʾ����ʼ��������ʵ��������е���
        buttonToTextMap[buttons[0]] = new string[] { "ת����Ÿ�", "��", "��", "����ת��" };
        buttonToTextMap[buttons[1]] = new string[] { "���������", "��", "��", "����" };
        buttonToTextMap[buttons[2]] = new string[] { "���ٳ���", "��", "��", "���ٳ���" };
        buttonToTextMap[buttons[3]] = new string[] { "������", "��", "��", "������" };
        buttonToTextMap[buttons[4]] = new string[] { "���������", "��������", "IGBT", "���������" };
        // ��Ӹ��ఴť�Ͷ�Ӧ���ı���������
    }

    private void InitializeButtonToObjectMap()
    {
        // ʾ����ʼ��������ʵ��������е���
        buttonToObjectMap[buttons[0]] = GameObject.Find("RotorAndMagnet"); // �滻Ϊʵ�ʵ���������
        buttonToObjectMap[buttons[1]] = GameObject.Find("StatorAndHousing"); // �滻Ϊʵ�ʵ���������
        buttonToObjectMap[buttons[2]] = GameObject.Find("Gear"); // �滻Ϊʵ�ʵ���������
        buttonToObjectMap[buttons[3]] = GameObject.Find("Differential"); // �滻Ϊʵ�ʵ���������
        buttonToObjectMap[buttons[4]] = GameObject.Find("MotorController"); // �滻Ϊʵ�ʵ���������
        // ��Ӹ��ఴť�Ͷ�Ӧ������
    }

    private void OnButtonClick(Button button)
    {
        // �����ı�
        UpdateTexts(button);

        // �л�����
        if (buttonToObjectMap.TryGetValue(button, out GameObject targetObject))
        {
            if (currentActiveObject != null)
            {
                currentActiveObject.SetActive(false); // ���ص�ǰ��ʾ������
            }
            targetObject.SetActive(true); // ��ʾĿ������
            currentActiveObject = targetObject; // ���µ�ǰ��ʾ������
        }
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