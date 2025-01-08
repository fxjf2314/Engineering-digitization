//�������а�ť������������߼�
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

        // ���� Slider ������ɿ��¼�
        EventTrigger trigger = speedSlider.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp; 
        entry.callback.AddListener((data) => OnSliderPointerUp());
        trigger.triggers.Add(entry);
    }

    // Rotate the perspective ��ť����¼�
    private void OnRotateButtonClick()
    {
        isRotateActive = true;
        UpdateButtonStates();
    }

    // Start splicingButton ��ť����¼�
    private void OnStartSplicingButtonClick()
    {
        isSplicingActive = !isSplicingActive;
        UpdateButtonStates();
    }

    // RemoveButton ��ť����¼�
    private void OnRemoveButtonClick()
    {
        isRotateActive = false;
        removeButton.interactable = false;
        UpdateButtonStates();
    }

    // Revolve ��ť����¼�
    private void OnRevolveButtonClick()
    {
        isRevolveActive = true;
        UpdateButtonStates();
    }

    // Revolve Stop ��ť����¼�
    private void OnRevolveStopButtonClick()
    {
        isRevolveActive = false;
        UpdateButtonStates();
    }

    // SpeedControl ��ť����¼�
    private void OnSpeedControlButtonClick()
    {
        // �л� Slider �Ŀɼ���
        speedSlider.gameObject.SetActive(true);
    }

    // Slider ����ɿ��¼�
    private void OnSliderPointerUp()
    {
        // ����ɿ�ʱ������ Slider
        speedSlider.gameObject.SetActive(false);
    }

    // �������а�ť״̬
    private void UpdateButtonStates()
    {
        // ��� Rotate ����
        if (isRotateActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            revolveButton.interactable = false;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
            removeButton.interactable = true;
        }
        // ��� Splicing ����
        else if (isSplicingActive)
        {
            rotateButton.interactable = false;
            revolveButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = false;
            speedControlButton.interactable = false;
        }
        // ��� Revolve ����
        else if (isRevolveActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            removeButton.interactable = false;
            revolveStopButton.interactable = true;
            speedControlButton.interactable = true;
        }
        // Ĭ��״̬
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