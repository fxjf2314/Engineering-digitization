//�ӽ���ת���߼�
using UnityEngine;

public class CameraDragRotate : MonoBehaviour
{
    public Transform target;     // �����Χ�Ƶ�Ŀ��
    public float distance = 5.0f; // �������Ŀ��ľ���
    public float distanceMin = 2.0f; // ��С����
    public float distanceMax = 10.0f; // ������
    public float xSpeed = 120.0f; // ˮƽ��ק�ٶ�
    public float ySpeed = 120.0f; // ��ֱ��ק�ٶ�

    public float yMinLimit = -20f; // ��ֱ�Ƕ�����
    public float yMaxLimit = 80f;  // ��ֱ�Ƕ�����

    private float x = 0.0f;
    private float y = 0.0f;

    //private bool isDraggingOtherObject = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

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
            if (Input.GetMouseButton(0)) // ֱ���ж��������Ƿ���
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                distance -= scroll * 5;
                distance = Mathf.Clamp(distance, distanceMin, distanceMax);
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

   /* void CheckIfDraggingOtherObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
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

        if (Input.GetMouseButtonUp(0))
        {
            isDraggingOtherObject = false;
        }
    }*/

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}