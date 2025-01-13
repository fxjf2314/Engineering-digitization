//没用到，但是先别删
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
