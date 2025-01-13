using UnityEngine;

public class NewObjectController : MonoBehaviour
{
    public GameObject newObject; // ÐÂÎïÌå

    private void Start()
    {
        if (newObject != null)
        {
            newObject.SetActive(false);
        }
    }

    public void Show()
    {
        if (newObject != null)
        {
            newObject.SetActive(true); 
        }
    }

    public void Hide()
    {
        if (newObject != null)
        {
            newObject.SetActive(false);
        }
    }
}