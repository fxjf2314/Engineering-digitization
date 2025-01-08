//û�õ��������ȱ�ɾ
using UnityEngine;
using UnityEngine.UI;

public class RemoveButtonController : MonoBehaviour
{
    // ���� ButtonManager
    public ButtonManager buttonManager;

    void Start()
    {
        // �� ButtonManager ���¼�
        if (buttonManager != null)
        {
            buttonManager.removeButton.onClick.AddListener(OnRemoveButtonClick);
        }
    }

    // RemoveButton ��ť����¼�
    private void OnRemoveButtonClick()
    {
        // ���� RemoveButton
        gameObject.SetActive(false);

    }
}
