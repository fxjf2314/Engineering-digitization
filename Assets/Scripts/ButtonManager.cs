
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

        EventTrigger trigger = speedSlider.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp; 
        entry.callback.AddListener((data) => OnSliderPointerUp());
        trigger.triggers.Add(entry);
    }

    private void OnRotateButtonClick()
    {
        isRotateActive = true;
        UpdateButtonStates();
    }

    private void OnStartSplicingButtonClick()
    {
        isSplicingActive = !isSplicingActive;
        UpdateButtonStates();
    }

    private void OnRemoveButtonClick()
    {
        isRotateActive = false;
        removeButton.interactable = false;
        UpdateButtonStates();
    }

    private void OnRevolveButtonClick()
    {
        isRevolveActive = true;
        UpdateButtonStates();
    }

    private void OnRevolveStopButtonClick()
    {
        isRevolveActive = false;
        UpdateButtonStates();
    }

    private void OnSpeedControlButtonClick()
    {
        speedSlider.gameObject.SetActive(true);
    }

    private void OnSliderPointerUp()
    {
        speedSlider.gameObject.SetActive(false);
    }

    private void UpdateButtonStates()
    {
        if (isRotateActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            revolveButton.interactable = false;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
            removeButton.interactable = true;
        }
        else if (isSplicingActive)
        {
            rotateButton.interactable = false;
            revolveButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
        }
        else if (isRevolveActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = true;
            speedControlButton.interactable = true;
        }
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