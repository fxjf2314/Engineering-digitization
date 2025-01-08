//重置视角按钮的逻辑
using UnityEngine;

public class RemoveButton : MonoBehaviour
{
    public Camera mainCamera;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    private void Start()
    {
        if (mainCamera != null)
        {
            initialCameraPosition = mainCamera.transform.position;
            initialCameraRotation = mainCamera.transform.rotation;
        }
    }

    public void OnButtonClick()
    {
        if (mainCamera != null)
        {
            var cameraDragRotate = mainCamera.GetComponent<CameraDragRotate>();
            if (cameraDragRotate != null)
            {
                cameraDragRotate.enabled = false;
            }

            mainCamera.transform.position = initialCameraPosition;
            mainCamera.transform.rotation = initialCameraRotation;
        }
    }
}