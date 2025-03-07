using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class ButtonControlledCameraRotation : MonoBehaviour
{
    [Header("UI 组件")]
    public Button toggleButton;          // 控制按钮
    public TextMeshProUGUI buttonText;              // 按钮状态显示文本

    [Header("相机设置")]
    public Transform cameraPivot;        // 旋转中心点
    public float rotationSpeed = 5f;     // 旋转速度
    public float smoothFactor = 8f;      // 平滑过渡系数

    [Header("角度限制")]
    public Vector2 verticalLimits = new Vector2(-30, 80); // 垂直角度限制

    private bool isRotationEnabled;      // 旋转功能启用状态
    private float currentXRotation;      // 当前X轴角度
    private float currentYRotation;      // 当前Y轴角度
    private Vector3 lastMousePosition;   // 鼠标上一帧位置

    void Start()
    {
        // 初始化按钮状态
        UpdateButtonVisual();
        toggleButton.onClick.AddListener(ToggleRotation);

        // 初始化相机角度
        currentXRotation = cameraPivot.eulerAngles.x;
        currentYRotation = cameraPivot.eulerAngles.y;
    }

    void Update()
    {
        if (isRotationEnabled)
        {
            HandleRotationInput();
        }
    }

    void ToggleRotation()
    {
        isRotationEnabled = !isRotationEnabled;
        UpdateButtonVisual();
    }

    void UpdateButtonVisual()
    {
        buttonText.text = isRotationEnabled ? "禁用旋转" : "启用旋转";
    }

    void HandleRotationInput()
    {
        // 检测UI遮挡和鼠标按键
        if (EventSystem.current.IsPointerOverGameObject() || !Input.GetMouseButton(0))
            return;

        // 计算鼠标增量
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // 更新旋转角度
        currentYRotation += mouseDelta.x * rotationSpeed * Time.deltaTime;
        currentXRotation -= mouseDelta.y * rotationSpeed * Time.deltaTime;
        currentXRotation = Mathf.Clamp(currentXRotation, verticalLimits.x, verticalLimits.y);

        // 应用平滑旋转
        ApplyCameraRotation();

        // 保存当前鼠标位置
        lastMousePosition = Input.mousePosition;
    }

    void ApplyCameraRotation()
    {
        // 创建目标旋转
        Quaternion targetRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);

        // 应用平滑过渡
        cameraPivot.rotation = Quaternion.Lerp(
            cameraPivot.rotation,
            targetRotation,
            Time.deltaTime * smoothFactor
        );
    }

    // 外部复位方法
    public void ResetCameraAngle()
    {
        currentXRotation = 25f;
        currentYRotation = 0f;
        ApplyCameraRotation();
    }
}