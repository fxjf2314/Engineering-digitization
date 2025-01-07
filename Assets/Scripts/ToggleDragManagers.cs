//ƴ�Ӱ�ť���߼����������ã�ż������
using UnityEngine;
using UnityEngine.UI;

public class ToggleDragManagers : MonoBehaviour
{
    public CameraDragRotate cameraDragRotate;

    public ButtonManager buttonManager;

    private int clickCount = 0;

    void Start()
    {
        if (buttonManager != null)
        {
            buttonManager.startSplicingButton.onClick.AddListener(OnStartSplicingButtonClick);
        }
    }

    private void OnStartSplicingButtonClick()
    {
        clickCount++;

        if (clickCount % 2 == 0)
        {
            DisableDragManagers();
        }
        else
        {
            EnableDragManagers();
        }

        if (cameraDragRotate != null)
        {
            cameraDragRotate.enabled = false;
        }

    }

    // �������� DragManager �ű�
    private void EnableDragManagers()
    {
        DragManager[] dragManagers = FindObjectsOfType<DragManager>();
        foreach (DragManager dragManager in dragManagers)
        {
            dragManager.enabled = true;
        }
    }

    // �������� DragManager �ű�
    private void DisableDragManagers()
    {
        DragManager[] dragManagers = FindObjectsOfType<DragManager>();
        foreach (DragManager dragManager in dragManagers)
        {
            dragManager.enabled = false;
        }
    }
}