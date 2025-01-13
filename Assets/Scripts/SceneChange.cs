using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public Button switchButton; // ���������л��İ�ť
    public string targetSceneName; // Ŀ�곡��������

    private void Start()
    {
        switchButton.onClick.AddListener(() => LoadScene(targetSceneName));
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
