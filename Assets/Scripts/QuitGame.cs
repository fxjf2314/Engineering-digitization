using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // �˳���Ϸ�ķ���
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
