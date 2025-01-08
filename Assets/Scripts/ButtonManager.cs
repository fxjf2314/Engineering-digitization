//�������а�ť������������߼�
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

            //removeButton.gameObject.SetActive(true);
            removeButton.interactable = true;
        }
        // ��� Revolve ����
        else if (isRevolveActive)
        {
            rotateButton.interactable = false;
            startSplicingButton.interactable = false;
            removeButton.interactable = false;
            revolveButton.interactable = false; 

            //revolveStopButton.gameObject.SetActive(true);
            revolveStopButton.interactable = true;
        }
        // ��� Splicing ����
        else if (isSplicingActive)
        {
            rotateButton.interactable = false;
            revolveButton.interactable = false;
            removeButton.interactable = false;
        }
        // Ĭ��״̬
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