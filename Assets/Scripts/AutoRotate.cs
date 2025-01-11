using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;  // 旋转速度
    private Vector3 centerPoint;     // 物体的中心点

    void Start()
    {
        // 计算物体的中心点
        centerPoint = GetCenterPoint();
    }

    void Update()
    {
        // 绕 Y 轴自转
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
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