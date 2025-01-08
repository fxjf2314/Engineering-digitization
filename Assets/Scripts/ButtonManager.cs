//控制所有按钮的启用与禁用逻辑
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{
    public Button rotateButton;
    public Button startSplicingButton;
    public Button removeButton;
    public Button revolveButton;
    public Button revolveStopButton;
    public Button speedControlButton; 
    public Slider speedSlider;        

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
        speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);

        speedSlider.gameObject.SetActive(false); 

        // 监听 Slider 的鼠标松开事件
        EventTrigger trigger = speedSlider.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp; 
        entry.callback.AddListener((data) => OnSliderPointerUp());
        trigger.triggers.Add(entry);
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

    // SpeedControl 按钮点击事件
    private void OnSpeedControlButtonClick()
    {
        // 切换 Slider 的可见性
        speedSlider.gameObject.SetActive(true);
    }

    // Slider 鼠标松开事件
    private void OnSliderPointerUp()
    {
        // 鼠标松开时，隐藏 Slider
        speedSlider.gameObject.SetActive(false);
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
            speedControlButton.interactable = false;
            removeButton.interactable = true;
        }
        // 如果 Splicing 激活
        else if (isSplicingActive)
        {
            rotateButton.interactable = false;
            revolveButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
        }
        // 如果 Revolve 激活
        else if (isRevolveActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = true;
            speedControlButton.interactable = true;
        }
        // 默认状态
        else
        {
            rotateButton.interactable = true;
            startSplicingButton.interactable = true;
            revolveButton.interactable = true;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
            removeButton.interactable = false;
        }
    }
}