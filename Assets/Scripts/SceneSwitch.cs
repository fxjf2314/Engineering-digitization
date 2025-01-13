using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Metadata;

public class SceneSwitch : MonoBehaviour
{
    public Button switchButton; // 触发场景切换的按钮
    public string targetSceneName; // 目标场景的名称
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
                    case "图鉴":
                        sceneName = "IllusHandbook";
                        break;
                    case "拆装实验":

                        sceneName = "SplicingScene";
                        break;
                    case "概念介绍":
                        sceneName = "Concept";
                        break;
                    case "旋转实验":
                        sceneName = "RevolveScenes";

                        break;
                    default: break;
                }
            }
        }
        // 加载目标场景
        SceneManager.LoadScene(sceneName);
    }
}
