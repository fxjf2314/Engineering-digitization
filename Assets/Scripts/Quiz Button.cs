using UnityEngine;
using UnityEngine.UI;
using TMPro; // ���� TMP �����ռ�

public class QuizButton : MonoBehaviour
{
    public Button targetButton; // Ŀ�갴ť
    public GameObject panelCorrect20; // ������ȷ���ﵽ 20 ʱ�򿪵� Panel
    public GameObject panelLessThan20; // ������ȷ������ 20 ʱ�򿪵� Panel
    public GameObject floatingTextPrefab; // ��������Ԥ���壨TMP��
    public Transform canvasTransform; // Canvas �� Transform������ʵ������������

    private void Start()
    {
        targetButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        float progress = GlobalQuizManager.Instance.GetGlobalProgress();

        int correctAnswers = GlobalQuizManager.Instance.totalCorrectAnswers;

        if (progress >= 1.0f) 
        {
            if (correctAnswers >= 20)
            {
                panelCorrect20.SetActive(true);
            }
            else
            {
                panelLessThan20.SetActive(true);
            }
        }
        else 
        {
            ShowFloatingText("�ش���ȫ������ſɴ򿪣�");
        }
    }

    private void ShowFloatingText(string message)
    {
        GameObject floatingTextObj = Instantiate(floatingTextPrefab, canvasTransform);

        TextMeshProUGUI floatingText = floatingTextObj.GetComponent<TextMeshProUGUI>();
        floatingText.text = message;

        Destroy(floatingTextObj, 1.5f);
    }
}