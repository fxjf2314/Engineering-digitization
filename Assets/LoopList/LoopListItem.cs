using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoopListItem : MonoBehaviour {

    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private TMP_Text nameText;

    private Data currentData;

    public Data CurrentData => currentData;

    public void SetData(Data data) {
        currentData = data;
        backgroundImage.sprite = data.image;
        nameText.text = data.name;
    }

    public int index;

    public void ShiftRight(float offset, float duration = 0.2f) {
        StartCoroutine(ShiftCoroutine(1, duration, offset));
    }
    
    public void ShiftLeft(float offset, float duration = 0.2f) {
        StartCoroutine(ShiftCoroutine(-1, duration, offset));
    }

    private IEnumerator ShiftCoroutine(int direction, float duration, float offset) {
        direction /= Mathf.Abs(direction);

        index += direction;

        Vector3 newPos = new(index * offset, 0, 0);

        float currentTime = 0;
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, currentTime / duration);
            yield return null;
        }
        transform.localPosition = newPos;
    }

}
