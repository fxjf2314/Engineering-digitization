using TMPro;
using UnityEngine;

public class TogglePanel11 : MonoBehaviour
{
    public GameObject panel; // �����½���
    public TMP_Text completionText; // ����������ʾ�����ʾ�� TMP �ı�
    public QuizManager11 quizManager11; // ���� QuizManager11 �Լ���Ƿ����������Ŀ

    private void Start()
    {
        // ��ʼ�� completionText Ϊ����״̬
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        // ��� quizManager11 �Ƿ����������Ŀ
        if (quizManager11 != null && quizManager11.IsGroupComplete())
        {
            // �����ɣ���ʾ�����ʾ�ı�
            if (completionText != null)
            {
                completionText.text = "��ǰ�����Ŀ�ѻش����";
                completionText.gameObject.SetActive(true);

                // 3 ������� completionText
                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            // ���δ��ɣ��л� panel ����ʾ״̬
            if (panel != null)
            {
                panel.SetActive(!panel.activeSelf);
            }
        }
    }

    private void HideCompletionText()
    {
        // ���� completionText
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}