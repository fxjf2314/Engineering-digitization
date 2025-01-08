using UnityEngine;
using UnityEngine.UI;

public class ObjectRotationControl : MonoBehaviour
{
    public Button speedControlButton; // 控制旋转速度的按钮
    public Slider speedSlider;        // 控制旋转速度的Slider
    public Controller rotationController; // 引用Controller脚本

    public float minSpeed = 10f;      // 最小旋转速度
    public float maxSpeed = 200f;     // 最大旋转速度

    void Start()
    {
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);
            Debug.Log("SpeedControl Button Event Bound"); // 添加调试日志
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