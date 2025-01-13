using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ButtonToChangeTexts : MonoBehaviour
{
    public List<Button> buttons; 
    public Dictionary<Button, string[]> buttonToTextMap = new Dictionary<Button, string[]>();
    public Dictionary<Button, GameObject> buttonToObjectMap = new Dictionary<Button, GameObject>();
    public List<TextMeshProUGUI> texts; 
    public GameObject defaultObject;

    public List<GameObject> objects; 

    private GameObject currentActiveObject; 
    private bool isInitialClick = true; 

    private void Start()
    {
        InitializeButtonToTextMap();

        InitializeButtonToObjectMap();

        HideAllObjects();

        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }

        HideAllUIExceptButtons();

        foreach (var button in buttons)
        {
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClick(button));
            }
        }

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
    }

    private void InitializeButtonToObjectMap()
    {

        
        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] != null && objects[i] != null)
            {
                buttonToObjectMap[buttons[i]] = objects[i];
            }
        }
    }

    private void HideAllObjects()
    {
       
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

        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
        }


        foreach (var obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        if (defaultObject != null)
        {
            defaultObject.SetActive(false);
        }
    }

    private void OnButtonClick(Button button)
    {
        Debug.Log($"��ť {button.name} �������");

        if (isInitialClick)
        {
            ShowAllUI();
            isInitialClick = false;
        }

        UpdateTexts(button);

        if (buttonToObjectMap.TryGetValue(button, out GameObject targetObject))
        {
            if (currentActiveObject != null)
            {
                currentActiveObject.SetActive(false); 
            }
            targetObject.SetActive(true); 
            currentActiveObject = targetObject; 
        }
    }

    private void UpdateTexts(Button button)
    {
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
        foreach (var text in texts)
        {
            if (text != null)
            {
                text.gameObject.SetActive(true);
            }
        }

        if (defaultObject != null)
        {
            defaultObject.SetActive(true);
            currentActiveObject = defaultObject;
        }
    }

}