using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isRotating = false; // 标记是否正在旋转
    public float rotateSpeed = 50f;  // 旋转速度
    private Quaternion initialRotation; // 初始旋转状态
    private Vector3 centerPoint; // 物体的中心点

    void Start()
    {
        // 记录初始旋转状态
        initialRotation = transform.rotation;
        // 计算物体的中心点
        centerPoint = GetCenterPoint();
    }

    void Update()
    {
        if (isRotating)
        {
            // 绕物体的中心点沿 X 轴旋转
            transform.RotateAround(centerPoint, Vector3.right, rotateSpeed * Time.deltaTime);
        }
    }

    public void StartRotating()
    {
        isRotating = true;
    }

    public void StopRotating()
    {
        isRotating = false;
        ResetToInitialRotation();
    }

    private void ResetToInitialRotation()
    {
        // 重置旋转状态
        transform.rotation = initialRotation;
    }

    private Vector3 GetCenterPoint()
    {
        // 获取物体的中心点
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.center;
        }
        else
        {
            // 如果没有 Renderer 组件，则返回物体的位置
            return transform.position;
        }
    }
}