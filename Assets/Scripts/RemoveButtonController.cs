//û�õ��������ȱ�ɾ
using UnityEngine;
using UnityEngine.UI;

public class RemoveButtonController : MonoBehaviour
{
    public ButtonManager buttonManager;

    void Start()
    {
        if (buttonManager != null)
        {
            buttonManager.removeButton.onClick.AddListener(OnRemoveButtonClick);
        }
    }

    private void OnRemoveButtonClick()
    {
        gameObject.SetActive(false);

    }
}
