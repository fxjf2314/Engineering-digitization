using UnityEngine;
using UnityEngine.UI;
using TMPro; // 引入 TMP 命名空间

public class QuizButton : MonoBehaviour
{
    public Button targetButton; // 目标按钮
    public GameObject panelCorrect20; // 答题正确数达到 20 时打开的 Panel
    public GameObject panelLessThan20; // 答题正确数不足 20 时打开的 Panel
    public GameObject floatingTextPrefab; // 悬浮字体预制体（TMP）
    public Transform canvasTransform; // Canvas 的 Transform，用于实例化悬浮字体

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
            ShowFloatingText("回答完全部问题才可打开！");
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