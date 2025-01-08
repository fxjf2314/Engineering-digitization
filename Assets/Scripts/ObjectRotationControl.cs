using UnityEngine;
using UnityEngine.UI;

public class ObjectRotationControl : MonoBehaviour
{
    public Button speedControlButton; // ������ת�ٶȵİ�ť
    public Slider speedSlider;        // ������ת�ٶȵ�Slider
    public Controller rotationController; // ����Controller�ű�

    public float minSpeed = 10f;      // ��С��ת�ٶ�
    public float maxSpeed = 200f;     // �����ת�ٶ�

    void Start()
    {
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);
            Debug.Log("SpeedControl Button Event Bound"); // ��ӵ�����־
        }
        else
        {
            Debug.LogError("SpeedControl Button is not assigned!");
        }

        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(false); 
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = rotationController.rotateSpeed;
            speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }
    }

    private void OnSpeedControlButtonClick()
    {
        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeSelf);
        }
    }
    private void OnSpeedSliderValueChanged(float value)
    {
        if (rotationController != null)
        {
            rotationController.rotateSpeed = value; 
        }
    }
}