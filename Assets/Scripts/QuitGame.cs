using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // 退出游戏的方法
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
