using UnityEngine;

public class NewObjectController : MonoBehaviour
{
    private void Start()
    {
        // 初始状态为隐藏
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // 显示新物体
        gameObject.SetActive(true);
    }
}