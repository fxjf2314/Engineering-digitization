using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public class ButtonGroupController : MonoBehaviour
{
    public Button mainButton;
    public TextMeshProUGUI mainButtonText; // 使用TMP文本组件
    public Button[] otherButtons;
    public Vector3 moveOffset = new Vector3(100, 0, 0);
    public float animationDuration = 0.5f;

    private Vector3[] originalPositions;
    private bool isMoved = false;
    private Coroutine currentAnimation;

    void Start()
    {
        // 初始化TMP文本
        mainButtonText.text = "展开";

        // 自动获取TMP组件（如果未手动指定）
        if (mainButtonText == null)
        {
            mainButtonText = mainButton.GetComponentInChildren<TextMeshProUGUI>();
            Debug.LogWarning("请确保主按钮包含TextMeshPro组件");
        }

        // 记录初始位置
        originalPositions = new Vector3[otherButtons.Length + 1];
        StoreOriginalPositions();

        mainButton.onClick.AddListener(OnMainButtonClick);
    }

    void StoreOriginalPositions()
    {
        originalPositions[0] = mainButton.transform.position;
        for (int i = 0; i < otherButtons.Length; i++)
        {
            originalPositions[i + 1] = otherButtons[i].transform.position;
        }
    }

    void OnMainButtonClick()
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(isMoved ?
            MoveToOriginal() :
            MoveToOffset());

        // 更新按钮文字
        mainButtonText.text = isMoved ? "展开" : "收起";
        isMoved = !isMoved;
    }

    IEnumerator MoveToOffset()
    {
        Vector3[] targetPositions = new Vector3[otherButtons.Length + 1];
        targetPositions[0] = originalPositions[0] + moveOffset;
        for (int i = 0; i < otherButtons.Length; i++)
        {
            targetPositions[i + 1] = originalPositions[i + 1] + moveOffset;
        }
        yield return AnimateMovement(targetPositions);
    }

    IEnumerator MoveToOriginal()
    {
        yield return AnimateMovement(originalPositions);
    }

    IEnumerator AnimateMovement(Vector3[] targets)
    {
        float elapsed = 0;
        Vector3[] startPositions = GetCurrentPositions();

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / animationDuration);

            UpdatePositions(startPositions, targets, t);
            yield return null;
        }

        UpdatePositions(targets, targets, 1);
    }

    void UpdatePositions(Vector3[] from, Vector3[] to, float t)
    {
        mainButton.transform.position = Vector3.Lerp(from[0], to[0], t);
        for (int i = 0; i < otherButtons.Length; i++)
        {
            otherButtons[i].transform.position = Vector3.Lerp(from[i + 1], to[i + 1], t);
        }
    }

    Vector3[] GetCurrentPositions()
    {
        Vector3[] positions = new Vector3[otherButtons.Length + 1];
        positions[0] = mainButton.transform.position;
        for (int i = 0; i < otherButtons.Length; i++)
        {
            positions[i + 1] = otherButtons[i].transform.position;
        }
        return positions;
    }
}