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
            objectDrag = mainCamera.GetComponent<ObjectDrag>(); // ��ȡ ObjectDrag ���
        }
    }

    public void OnButtonClick()
    {
        //Debug.Log("OnButtonClick called.");

        if (mainCamera != null)
        {
            if (objectDrag != null)
            {
                objectDrag.enabled = false; // ���� ObjectDrag �ű�
            }

            // �������λ�ú���ת
            mainCamera.transform.position = initialCameraPosition;
            mainCamera.transform.rotation = initialCameraRotation;

            // ���� ObjectDrag �ű��е���ת�Ƕ�
            objectDrag.ResetCameraAngles();

            //Debug.Log($"Camera reset to Position: {mainCamera.transform.position}, Rotation: {mainCamera.transform.rotation}");

            // �ӳ�һ֡���������� ObjectDrag �ű�
            StartCoroutine(ReenableObjectDrag());
        }
        else
        {
            //Debug.LogWarning("MainCamera is not assigned!");
        }
    }

    private IEnumerator ReenableObjectDrag()
    {
        yield return null; // �ȴ�һ֡
        if (objectDrag != null)
        {
            objectDrag.enabled = true; // �������� ObjectDrag �ű�
        }
    }
}
