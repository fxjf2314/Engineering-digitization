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
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        if (quizManager != null && (quizManager.IsGroupComplete() || quizManager11.IsGroupComplete()))
        {
            if (completionText != null)
            {
                completionText.text = "��ǰ�����Ŀ�ѻش����";
                completionText.gameObject.SetActive(true);

                Invoke("HideCompletionText", 3f);
            }
        }
        else
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    private void HideCompletionText()
    {
        if (completionText != null)
        {
            completionText.gameObject.SetActive(false);
        }
    }
}