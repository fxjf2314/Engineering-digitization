using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public Transform target;     // 摄像机围绕的目标物体
    public float distance = 5.0f; // 摄像机与目标的初始距离

    public float xSpeed = 120.0f; // 水平旋转速度
    public float ySpeed = 120.0f; // 垂直旋转速度
    public float yMinLimit = 0f; // 垂直旋转角度下限
    public float yMaxLimit = 45f;  // 垂直旋转角度上限

    private float x = 0.0f; // 水平旋转角度
    private float y = 0.0f; // 垂直旋转角度
    private bool isDraggingOtherObject = false; // 是否正在拖拽其他物体

    private Vector3 initialAngles; // 初始旋转角度

    void Start()
    {
        // 初始化摄像机的旋转角度
        initialAngles = transform.eulerAngles;
        x = initialAngles.y;
        y = initialAngles.x;

        // 如果摄像机有 Rigidbody 组件，冻结其旋转
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            CheckIfDraggingOtherObject(); // 检查是否正在拖拽其他物体

            // 如果没有拖拽其他物体，允许摄像机旋转
            if (!isDraggingOtherObject && Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f; // 更新水平旋转角度
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f; // 更新垂直旋转角度
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit); // 限制垂直旋转角度
            }

            // 更新摄像机的位置和旋转
            Quaternion rotation = Quaternion.Euler(y, x, 0); // 绕 X 轴和 Y 轴旋转
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    // 检查是否正在拖拽其他物体
    void CheckIfDraggingOtherObject()
    {
        if (Input.GetMouseButtonDown(0)) // 鼠标左键按下
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 如果点击的不是目标物体，则标记为正在拖拽其他物体
                if (hit.transform != target)
                {
                    isDraggingOtherObject = true;
                }
                else
                {
                    isDraggingOtherObject = false;
                }
            }
            else
            {
                isDraggingOtherObject = false;
            }
        }

        if (Input.GetMouseButtonUp(0)) // 鼠标左键释放
        {
            isDraggingOtherObject = false;
        }
    }

    // 重置摄像机的旋转角度
    public void ResetCameraAngles()
    {
        x = initialAngles.y;
        y = initialAngles.x;
    }
}
