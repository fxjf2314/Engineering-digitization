using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isRotating = false; // 标记是否正在旋转
    public float rotateSpeed = 50f;  // 旋转速度
    private Quaternion initialRotation; // 初始旋转状态
    private Vector3 centerPoint; // 物体的中心点

    void Start()
    {
        initialRotation = transform.rotation;
        centerPoint = GetCenterPoint();
    }

    void Update()
    {
        if (isRotating)
        {
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
        transform.rotation = initialRotation;
    }

    private Vector3 GetCenterPoint()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.center;
        }
        else
        {
            return transform.position;
        }
    }
}