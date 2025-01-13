using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class SceneSwitch : MonoBehaviour
{
    public Button switchButton; // ���������л��İ�ť
    public string targetSceneName; // Ŀ�곡��������
    private LoopList LoopList;

    private void Start()
    {
        LoopList = new LoopList();
        LoopList.childrenDeque = new LinkedList<LoopListItem>();
        switchButton.onClick.AddListener(() => LoadScene(targetSceneName));
    }

    
    private void LoadScene(string sceneName)
    {
        foreach (Transform item in transform)
        {
            LoopListItem loopListItem = item.GetComponentInChildren<LoopListItem>();
            if(loopListItem.index == 0)
            {
                string nextScene = item.GetComponentInChildren<TextMeshProUGUI>().text;
                switch(nextScene)
                {
                    case "ͼ��":
                        sceneName = "IllusHandbook";
                        break;
                    case "��װʵ��":

                        sceneName = "SplicingScene";
                        break;
                    case "�������":
                        sceneName = "Concept";
                        break;
                    case "��תʵ��":
                        sceneName = "RevolveScenes";

                        break;
                    default: break;
                }
            }
        }
        // ����Ŀ�곡��
        SceneManager.LoadScene(sceneName);
    }
}
