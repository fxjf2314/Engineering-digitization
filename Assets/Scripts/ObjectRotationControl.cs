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
        // 绑定按钮点击事件
        if (speedControlButton != null)
        {
            speedControlButton.onClick.AddListener(OnSpeedControlButtonClick);
        }

        // 初始化Slider
        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(false); // 默认隐藏Slider
            speedSlider.minValue = minSpeed;
            speedSlider.maxValue = maxSpeed;
            speedSlider.value = rotationControllers[0].rotateSpeed; // 默认以第一个Controller的速度为初始值
            speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }
    }

    // 按钮点击事件
    private void OnSpeedControlButtonClick()
    {
        if (speedSlider != null)
        {
            speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeSelf);
        }
    }

    // Slider值改变事件
    private void OnSpeedSliderValueChanged(float value)
    {
        if (rotationControllers != null && rotationControllers.Length > 0)
        {
            // 遍历所有Controller，更新旋转速度
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