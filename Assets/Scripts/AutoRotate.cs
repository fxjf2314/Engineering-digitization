using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;  // 旋转速度
    private Vector3 centerPoint;     // 物体的中心点

    void Start()
    {
        centerPoint = GetCenterPoint();
    }

    void Update()
    {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
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