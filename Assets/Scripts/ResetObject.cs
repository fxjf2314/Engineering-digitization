using UnityEngine;
using System.Collections;

public class ResetObject : MonoBehaviour
{
    public Camera mainCamera;
    private ObjectDrag objectDrag;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    private void Start()
    {
        if (mainCamera != null)
        {
            initialCameraPosition = mainCamera.transform.position;
            initialCameraRotation = mainCamera.transform.rotation;
            objectDrag = mainCamera.GetComponent<ObjectDrag>(); // 获取 ObjectDrag 组件
        }
    }

    public void OnButtonClick()
    {
        //Debug.Log("OnButtonClick called.");

        if (mainCamera != null)
        {
            if (objectDrag != null)
            {
                objectDrag.enabled = false; // 禁用 ObjectDrag 脚本
            }

            // 重置相机位置和旋转
            mainCamera.transform.position = initialCameraPosition;
            mainCamera.transform.rotation = initialCameraRotation;

            // 重置 ObjectDrag 脚本中的旋转角度
            objectDrag.ResetCameraAngles();

            //Debug.Log($"Camera reset to Position: {mainCamera.transform.position}, Rotation: {mainCamera.transform.rotation}");

            // 延迟一帧后重新启用 ObjectDrag 脚本
            StartCoroutine(ReenableObjectDrag());
        }
        else
        {
            //Debug.LogWarning("MainCamera is not assigned!");
        }
    }

    private IEnumerator ReenableObjectDrag()
    {
        yield return null; // 等待一帧
        if (objectDrag != null)
        {
            objectDrag.enabled = true; // 重新启用 ObjectDrag 脚本
        }
    }
}
