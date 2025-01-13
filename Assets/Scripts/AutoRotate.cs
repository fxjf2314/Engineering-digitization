using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;  // ��ת�ٶ�
    private Vector3 centerPoint;     // ��������ĵ�

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