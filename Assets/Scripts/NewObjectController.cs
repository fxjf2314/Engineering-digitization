using UnityEngine;

public class NewObjectController : MonoBehaviour
{
    private void Start()
    {
        // ��ʼ״̬Ϊ����
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // ��ʾ������
        gameObject.SetActive(true);
    }
}