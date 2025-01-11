using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� TextMeshPro �����ռ�

public class MultiTextButton : MonoBehaviour
{
    public Button targetButton; // Ŀ�갴ť
    public TMP_Text displayText; // ʹ�� TMP_Text ��� Text
    public string[] textArray;  // �洢������ֵ�����

    private int currentIndex = 0; // ��ǰ��ʾ���ı�����

    private void Start()
    {
        // ��ʼ����ť�ı�
        targetButton.GetComponentInChildren<TMP_Text>().text = "ȷ��";

        // Ϊ��ť��ӵ���¼�
        targetButton.onClick.AddListener(OnButtonClick);

        // ��ʾ��һ������
        if (textArray.Length > 0)
        {
            displayText.text = textArray[currentIndex];
        }
    }

    private void OnButtonClick()
    {
        // ����Ƿ��Ѿ���ʾ�����һ������
        if (currentIndex >= textArray.Length - 1)
        {
            // ��������һ�����֣��˳���Ϸ
            QuitGame();
        }
        else
        {
            // ��ʾ��һ������
            currentIndex++;
            displayText.text = textArray[currentIndex];

            // ��������һ�����֣��޸İ�ť�ı�Ϊ���˳���
            if (currentIndex == textArray.Length - 1)
            {
                targetButton.GetComponentInChildren<TMP_Text>().text = "�˳�";
            }
        }
    }

    // �˳���Ϸ
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �ڱ༭����ֹͣ����
#else
        Application.Quit(); // �ڴ�������Ϸ���˳�
#endif
    }
}