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
        // �󶨰�ť����¼�
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);
        }

        // ��ʼ��Slider
        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(false); // Ĭ������Slider
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = rotationControllers[0].rotateSpeed; // Ĭ���Ե�һ��Controller���ٶ�Ϊ��ʼֵ
            speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }
    }

    // ��ť����¼�
    private void OnSpeedControlButtonClick()
    {
        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeSelf);
        }
    }

    // Sliderֵ�ı��¼�
    private void OnSpeedSliderValueChanged(float value)
    {
        if (rotationControllers != null && rotationControllers.Length > 0)
        {
            // ��������Controller��������ת�ٶ�
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