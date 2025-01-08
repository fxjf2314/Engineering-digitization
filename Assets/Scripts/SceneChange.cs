using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public Button switchButton; // 触发场景切换的按钮
    public string targetSceneName; // 目标场景的名称

    private void Start()
    {
        // 为按钮添加点击事件
        switchButton.onClick.AddListener(() => LoadScene(targetSceneName));
    }

    private void LoadScene(string sceneName)
    {
        // 加载目标场景
        SceneManager.LoadScene(sceneName);
    }
}
