//控制所有按钮的启用与禁用逻辑
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button rotateButton;
    public Button startSplicingButton;
    public Button removeButton;
    public Button revolveButton;      
    public Button revolveStopButton; 

    private bool isRotateActive = false;
    private bool isSplicingActive = false;
    private bool isRevolveActive = false; 

    void Start()
    {
        UpdateButtonStates();

        rotateButton.onClick.AddListener(OnRotateButtonClick);
        startSplicingButton.onClick.AddListener(OnStartSplicingButtonClick);
        removeButton.onClick.AddListener(OnRemoveButtonClick);
        revolveButton.onClick.AddListener(OnRevolveButtonClick);       
        revolveStopButton.onClick.AddListener(OnRevolveStopButtonClick);
    }

    // Rotate the perspective 按钮点击事件
    private void OnRotateButtonClick()
    {
        isRotateActive = true;
        UpdateButtonStates();
    }

    // Start splicingButton 按钮点击事件
    private void OnStartSplicingButtonClick()
    {
        isSplicingActive = !isSplicingActive;
        UpdateButtonStates();
    }

    // RemoveButton 按钮点击事件
    private void OnRemoveButtonClick()
    {
        isRotateActive = false;
        removeButton.interactable = false; 
        UpdateButtonStates();
    }

    // Revolve 按钮点击事件
    private void OnRevolveButtonClick()
    {
        isRevolveActive = true;
        UpdateButtonStates();
    }

    // Revolve Stop 按钮点击事件
    private void OnRevolveStopButtonClick()
    {
        isRevolveActive = false;
        UpdateButtonStates();
    }

    // 更新所有按钮状态
    private void UpdateButtonStates()
    {
        // 如果 Rotate 激活
        if (isRotateActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            revolveButton.interactable = false;
            revolveStopButton.interactable = false;

            //removeButton.gameObject.SetActive(true);
            removeButton.interactable = true;
        }
        // 如果 Revolve 激活
        else if (isRevolveActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            removeButton.interactable = false;
            revolveButton.interactable = false; 

            //revolveStopButton.gameObject.SetActive(true);
            revolveStopButton.interactable = true;
        }
        // 如果 Splicing 激活
        else if (isSplicingActive)
        {
            rotateButton.interactable = false;
            revolveButton.interactable = false;
            removeButton.interactable = false;
        }
        // 默认状态
        else
        {
            rotateButton.interactable = true;
            startSplicingButton.interactable = true;
            revolveButton.interactable = true;

            removeButton.interactable = false;
            revolveStopButton.interactable = false;
        }
    }
}