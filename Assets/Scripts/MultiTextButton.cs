using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiTextButton : MonoBehaviour
{
    public Button targetButton; // Ŀ�갴ť
    public TMP_Text displayText; // ʹ�� TMP_Text ��� Text
    public string[] textArray;  // �洢������ֵ�����

    private int currentIndex = 0; // ��ǰ��ʾ���ı�����

    private void Start()
    {
        targetButton.GetComponentInChildren<TMP_Text>().text = "ȷ��";

        targetButton.onClick.AddListener(OnButtonClick);

        if (textArray.Length > 0)
        {
            displayText.text = textArray[currentIndex];
        }
    }

    private void OnButtonClick()
    {
        if (currentIndex >= textArray.Length - 1)
        {
            QuitGame();
        }
        else
        {
            currentIndex++;
            displayText.text = textArray[currentIndex];
            if (currentIndex == textArray.Length - 1)
            {
                targetButton.GetComponentInChildren<TMP_Text>().text = "�˳�";
            }
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); // �ڴ�������Ϸ���˳�
#endif
    }
}