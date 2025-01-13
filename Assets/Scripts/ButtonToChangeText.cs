using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons; // �����ť�б�
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>(); // ��ť���ı����������ӳ��
    public Dictionary<Button, GameObject> buttonToObjectMap = new Dictionary<Button, GameObject>(); // ��ť�������ӳ��
    public List<TextMeshProUGUI> texts; // ������Ҫ���µ��ı����
    public GameObject defaultObject; // Ĭ����ʾ������

    public List<GameObject> objects; // �ֶ��󶨵������б�

    private GameObject currentActiveObject; // ��ǰ��ʾ������
    private bool isInitialClick = true; // �Ƿ��ǵ�һ�ε��

    private void Start()
    {
        // ��ʼ����ť���ı���ӳ��
        InitializeButtonToTextMap();

        // ��ʼ����ť�������ӳ��
        InitializeButtonToObjectMap();

        // ������������
        HideAllObjects();

        // ��ʼ��Ĭ����ʾ������
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }

        // �������� UI�����������ť��
        HideAllUIExceptButtons();

        // Ϊÿ����ť��ӵ���¼�
        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            }
            else
            {
                Debug.LogError("��ťδ�󶨣����� buttons �б�");
            }
        }

        // ��ӡ������Ϣ
        DebugButtonAndObjectInfo();
    }

    private void InitializeButtonToTextMap()
    {
        // ʾ����ʼ��������ʵ��������е���
        if (buttons.Count >= 5)
        {
            buttonToTextMap[buttons[0]] = new string[] { "ת����Ÿ�", "��", "��", "����ת��" };
            buttonToTextMap[buttons[1]] = new string[] { "���������", "��", "��", "����" };
            buttonToTextMap[buttons[2]] = new string[] { "���ٳ���", "��", "��", "���ٳ���" };
            buttonToTextMap[buttons[3]] = new string[] { "������", "��", "��", "������" };
            buttonToTextMap[buttons[4]] = new string[] { "���������", "��������", "IGBT", "���������" };
        }
        else
        {
            Debug.LogError("��ť�������㣡��ȷ�� buttons �б��������� 5 ����ť��");
        }
    }

    private void InitializeButtonToObjectMap()
    {
        // ȷ����ť����������һ��
        if (buttons.Count != objects.Count)
        {
            Debug.LogError("��ť������������ƥ�䣡���� buttons �� objects �б�");
            return;
        }

        // ����ť������һһ��Ӧ
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null && objects[i] != null)
            {
                buttonToObjectMap[buttons[i]] = objects[i];
            }
            else
            {
                Debug.LogError($"��ť {i} ������ {i} δ�󶨣�");
            }
        }
    }

    private void HideAllObjects()
    {
        // ���������밴ť����������
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
        // ���������ı����
        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
        }

        // ������������
        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // ����Ĭ������
        if (defaultObject != null)
        {
            defaultObject.SetActive(false);
        }
    }

    private void OnButtonClick(Button button)
    {
        Debug.Log($"��ť {button.name} �������");

        // ����ǵ�һ�ε������ʾ�����ı�������
        if (isInitialClick)
        {
            ShowAllUI();
            isInitialClick = false;
        }

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

    private void ShowAllUI()
    {
        // ��ʾ�����ı����
        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(true);
            }
        }

        // ��ʾĬ������
        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }
    }

    private void DebugButtonAndObjectInfo()
    {
        // ��ӡ��ť������İ���Ϣ
        Debug.Log("===== ��ť���������Ϣ =====");
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == null)
            {
                Debug.LogError($"��ť {i} δ�󶨣�");
                continue;
            }

            if (buttonToObjectMap.TryGetValue(buttons[i], out GameObject obj))
            {
                if (obj == null)
                {
                    Debug.LogError($"��ť {i} ��Ӧ������δ�󶨣�");
                }
                else
                {
                    Debug.Log($"��ť {i} ����: {buttons[i].name}, ������: {obj.name}");
                }
            }
            else
            {
                Debug.LogError($"��ť {i} δ�� buttonToObjectMap ���ҵ���Ӧ�����壡");
            }
        }
    }
}