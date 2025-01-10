
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManagerRevolve : MonoBehaviour
{
    public Button rotateButton;
    public Button startSplicingButton;
    public Button removeButton;
    public Button revolveButton;
    public Button revolveStopButton;
    public Button speedControlButton;
    public Slider speedSlider;

    public AudioSource audioSource;
    public AudioClip musicLowSpeed;
    public AudioClip musicMediumSpeed;
    public AudioClip musicHighSpeed;

    private enum ButtonState { Default, RotateActive, SplicingActive, RevolveActive }
    private ButtonState currentState = ButtonState.Default;

    private AudioClip currentClip;

    void Start()
    {
        // 初始化按钮状态
        UpdateButtonStates();

        // 绑定按钮点击事件
        rotateButton.onClick.AddListener(OnRotateButtonClick);
        startSplicingButton.onClick.AddListener(OnStartSplicingButtonClick);
        removeButton.onClick.AddListener(OnRemoveButtonClick);
        revolveButton.onClick.AddListener(OnRevolveButtonClick);
        revolveStopButton.onClick.AddListener(OnRevolveStopButtonClick);
        speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);

        // 初始时 Slider 不可见
        speedSlider.gameObject.SetActive(false);

        // 监听 Slider 的鼠标松开事件
        if (speedSlider != null)
        {
            EventTrigger trigger = speedSlider.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = speedSlider.gameObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => OnSliderPointerUp());
            trigger.triggers.Add(entry);

            // 监听 Slider 的值变化事件
            speedSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnRotateButtonClick()
    {
        currentState = ButtonState.RotateActive;
        UpdateButtonStates();
    }

    private void OnStartSplicingButtonClick()
    {
        currentState = ButtonState.SplicingActive;
        UpdateButtonStates();
    }

    private void OnRemoveButtonClick()
    {
        currentState = ButtonState.Default;
        UpdateButtonStates();
    }

    private void OnRevolveButtonClick()
    {
        currentState = ButtonState.RevolveActive;
        UpdateButtonStates();
        PlayMusicBasedOnSpeed();
    }

    private void OnRevolveStopButtonClick()
    {
        currentState = ButtonState.Default;
        UpdateButtonStates();
        audioSource.Stop();
    }

    private void OnSpeedControlButtonClick()
    {
        speedSlider.gameObject.SetActive(true);
    }

    private void OnSliderPointerUp()
    {
        speedSlider.gameObject.SetActive(false);
    }

    private void OnSliderValueChanged(float value)
    {
        if (currentState == ButtonState.RevolveActive)
        {
            PlayMusicBasedOnSpeed();
        }
    }

    private void PlayMusicBasedOnSpeed()
    {
        float speed = speedSlider.value;
        AudioClip targetClip = null;

        if (speed >= 0 && speed <= 50)
        {
            targetClip = musicLowSpeed;
        }
        else if (speed > 50 && speed <= 100)
        {
            targetClip = musicMediumSpeed;
        }
        else if (speed > 100)
        {
            targetClip = musicHighSpeed;
        }

        if (targetClip != null && targetClip != currentClip)
        {
            audioSource.Stop();
            audioSource.clip = targetClip;
            audioSource.Play();
            currentClip = targetClip;
        }
    }

    private void UpdateButtonStates()
    {
        rotateButton.interactable = true;
        startSplicingButton.interactable = true;
        removeButton.interactable = false;
        revolveButton.interactable = true;
        revolveStopButton.interactable = false;
        speedControlButton.interactable = false;

        switch (currentState)
        {
            case ButtonState.RotateActive:
                rotateButton.interactable = false;
                startSplicingButton.interactable = false;
                revolveButton.interactable = false;//111
                removeButton.interactable = true;
                break;

            case ButtonState.SplicingActive:
                rotateButton.interactable = false;
                revolveButton.interactable = false;
                removeButton.interactable = false;
                break;

            case ButtonState.RevolveActive:
                rotateButton.interactable = false;
                startSplicingButton.interactable = false;
                removeButton.interactable = false;
                revolveStopButton.interactable = true;
                speedControlButton.interactable = true;
                break;
        }
    }
}