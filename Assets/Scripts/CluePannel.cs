using UnityEngine;
using TMPro;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel; // �����½���
    public TMP_Text completionText; // ����������ʾ�����ʾ�� TMP �ı�
    public QuizManager quizManager; // ���� QuizManager �Լ���Ƿ����������Ŀ
    public QuizManager11 quizManager11;
    private void Start()
    {
        // ��ʼ�������ʾ�ı�Ϊ����״̬
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    // ��ť���ʱ����
    public void OnButtonClick()
    {
        // ����Ƿ�������Ŀ�����
        if (quizManager != null && (quizManager.IsGroupComplete()||quizManager11.IsGroupComplete()))
        {
            // ��ʾ�����ʾ�ı�
            if (completionText != null)
            {
                completionText.text = "��ǰ�����Ŀ�ѻش����";
                completionText.gameObject.SetActive(true);

                // 3 ���������ʾ�ı�
                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            // �л��������ʾ״̬
            panel.SetActive(!panel.activeSelf);
        }
    }

    // ���������ʾ�ı�
    private void HideCompletionText()
    {
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}