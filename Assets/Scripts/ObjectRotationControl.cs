using UnityEngine;
using UnityEngine.UI;

public class ObjectRotationControl : MonoBehaviour
{
    public Button speedControlButton; // 控制旋转速度的按钮
    public Slider speedSlider;        // 控制旋转速度的Slider
    public Controller[] rotationControllers; // 引用6个Controller脚本

    public float minSpeed = 10f;      // 最小旋转速度
    public float maxSpeed = 200f;     // 最大旋转速度

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