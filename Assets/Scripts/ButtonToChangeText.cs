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
        // 示例初始化，根据实际情况进行调整
        if (buttons.Count >= 5)
        {
            buttonToTextMap[buttons[0]] = new string[] { "转子与磁钢", "无", "无", "永磁转子" };
            buttonToTextMap[buttons[1]] = new string[] { "定子与机壳", "无", "无", "定子" };
            buttonToTextMap[buttons[2]] = new string[] { "减速齿轮", "无", "无", "减速齿轮" };
            buttonToTextMap[buttons[3]] = new string[] { "差速器", "无", "无", "差速器" };
            buttonToTextMap[buttons[4]] = new string[] { "电机控制器", "控制主板", "IGBT", "电机控制器" };
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
        Debug.Log($"按钮 {button.name} 被点击！");

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