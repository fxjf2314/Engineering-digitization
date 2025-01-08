//物体旋转
using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isRotating = false;
    public float rotateSpeed = 50f;  // 旋转速度
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
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
}