using UnityEngine;
using UnityEngine.UI;

public class ObjectRotationControl : MonoBehaviour
{
    public Button speedControlButton; // ������ת�ٶȵİ�ť
    public Slider speedSlider;        // ������ת�ٶȵ�Slider
    public Controller[] rotationControllers; // ����6��Controller�ű�

    public float minSpeed = 10f;      // ��С��ת�ٶ�
    public float maxSpeed = 200f;     // �����ת�ٶ�

    void Start()
    {
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);
        }

        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(false); 
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = rotationControllers[0].rotateSpeed; 
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
        if (rotationControllers != null && rotationControllers.Length > 0)
        {
            foreach (var controller in rotationControllers)
            {
                if (controller != null)
                {
                    controller.rotateSpeed = value;
                }
            }
        }
    }
}