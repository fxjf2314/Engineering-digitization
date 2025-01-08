//旋转视角按钮的逻辑
using UnityEngine;
using UnityEngine.UI;

public class ToggleCameraDragAndDragManagers : MonoBehaviour
{
    public CameraDragRotate cameraDragRotate;
    public ButtonManager buttonManager;

    void Start()
    {
        if (buttonManager != null)
        {
            buttonManager.rotateButton.onClick.AddListener(OnRotateButtonClick);
        }
    }

    private void OnRotateButtonClick()
    {
        if (cameraDragRotate != null)
        {
            cameraDragRotate.enabled = !cameraDragRotate.enabled;
        }

        DragManager[] dragManagers = FindObjectsOfType<DragManager>();
        foreach (DragManager dragManager in dragManagers)
        {
            dragManager.enabled = !cameraDragRotate.enabled;
        }

    }
}