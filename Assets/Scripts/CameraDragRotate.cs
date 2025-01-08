//视角旋转的逻辑
using UnityEngine;

public class CameraDragRotate : MonoBehaviour
{
    public Transform target;     // 摄像机围绕的目标
    public float distance = 5.0f; // 摄像机与目标的距离
    public float distanceMin = 2.0f; // 最小距离
    public float distanceMax = 10.0f; // 最大距离
    public float xSpeed = 120.0f; // 水平拖拽速度
    public float ySpeed = 120.0f; // 垂直拖拽速度

    public float yMinLimit = -20f; // 垂直角度下限
    public float yMaxLimit = 80f;  // 垂直角度上限

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
            if (Input.GetMouseButton(0)) // 直接判断鼠标左键是否按下
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