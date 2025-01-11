using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float rotateSpeed = 20f;  // ��ת�ٶ�
    private Vector3 centerPoint;     // ��������ĵ�

    void Start()
    {
        // ������������ĵ�
        centerPoint = GetCenterPoint();
    }

    void Update()
    {
        // �� Y ����ת
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
    }

    private Vector3 GetCenterPoint()
    {
        // ��ȡ��������ĵ�
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.center;
        }
        else
        {
            // ���û�� Renderer ������򷵻������λ��
            return transform.position;
        }
    }
}