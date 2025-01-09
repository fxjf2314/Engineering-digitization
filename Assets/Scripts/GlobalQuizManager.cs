using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // 存储每个场景和组别的答题状态（场景名称 -> 组别 -> 答题状态列表）
    private Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // 全局统计
    public int totalQuestionsAllScenes; // 所有场景的总题目数
    public int totalCorrectAnswers;     // 所有场景的总正确答题数

    private void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject); // 防止重复创建
        }
    }

    // 初始化场景和组别的答题状态
    public void InitializeSceneStatus(string sceneName, string groupName, int totalQuestions)
    {
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName))
        {
            // 如果场景不存在，初始化场景
            sceneGroupAnswerStatus[sceneName] = new Dictionary<string, List<bool>>();
        }

        if (!sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            // 初始化组别的答题状态列表，默认值为 false
            sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);

            // 更新全局总题目数
            totalQuestionsAllScenes += totalQuestions;

            Debug.Log($"场景 {sceneName} 的组别 {groupName} 初始化完成，总题目数：{totalQuestions}");
        }
        else
        {
            Debug.LogWarning($"场景 {sceneName} 的组别 {groupName} 已初始化，无需重复初始化！");
        }
    }

    // 更新答题状态
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            // 确保 questionIndex 在合法范围内
            if (questionIndex >= 0 && questionIndex < sceneGroupAnswerStatus[sceneName][groupName].Count)
            {
                // 更新答题状态
                sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = isCorrect;

                // 如果答案正确，更新全局正确答题数
                if (isCorrect)
                {
                    totalCorrectAnswers++;
                }
            }
            else
            {
                Debug.LogError($"题目索引 {questionIndex} 超出范围！");
            }
        }
        else
        {
            Debug.LogError($"场景 {sceneName} 的组别 {groupName} 未初始化！");
        }
    }

    // 获取全局进度
    public float GetGlobalProgress()
    {
        if (totalQuestionsAllScenes == 0)
        {
            Debug.LogError("所有场景的总题目数为 0，请检查设置！");
            return 0;
        }

        // 计算已答题的总数
        int totalAnswered = 0;
        foreach (var scene in sceneGroupAnswerStatus.Values)
        {
            foreach (var group in scene.Values)
            {
                totalAnswered += group.Count;
            }
        }

        // 返回全局进度（已答题数 / 总题目数）
        return (float)totalAnswered / totalQuestionsAllScenes;
    }

    // 获取场景和组别的答题状态
    public List<bool> GetSceneGroupStatus(string sceneName, string groupName)
    {
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            return sceneGroupAnswerStatus[sceneName][groupName];
        }
        else
        {
            Debug.LogError($"场景 {sceneName} 的组别 {groupName} 未初始化！");
            return null;
        }
    }
}