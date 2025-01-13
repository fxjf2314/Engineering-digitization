using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public Transform target;     // �����Χ�Ƶ�Ŀ������
    public float distance = 5.0f; // �������Ŀ��ĳ�ʼ����

    public float xSpeed = 120.0f; // ˮƽ��ת�ٶ�
    public float ySpeed = 120.0f; // ��ֱ��ת�ٶ�
    public float yMinLimit = 0f; // ��ֱ��ת�Ƕ�����
    public float yMaxLimit = 45f;  // ��ֱ��ת�Ƕ�����

    private float x = 0.0f; // ˮƽ��ת�Ƕ�
    private float y = 0.0f; // ��ֱ��ת�Ƕ�
    private bool isDraggingOtherObject = false; // �Ƿ�������ק��������

    private Vector3 initialAngles; // ��ʼ��ת�Ƕ�

    void Start()
    {

        initialAngles = transform.eulerAngles;
        x = initialAngles.y;
        y = initialAngles.x;

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
            CheckIfDraggingOtherObject(); 

            if (!isDraggingOtherObject && Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                y = Mathf.Clamp(y, yMinLimit, yMaxLimit); 
            }

            Quaternion rotation = Quaternion.Euler(y, x, 0); 
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void CheckIfDraggingOtherObject()
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
    }

    public void ResetCameraAngles()
    {
        x = initialAngles.y;
        y = initialAngles.x;
    }
}
