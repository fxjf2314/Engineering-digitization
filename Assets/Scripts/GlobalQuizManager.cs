using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // 存储每个场景和组别的答题状态（场景名称 -> 组别 -> 答题状态列表）
    public Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // 全局统计
    public int totalQuestionsAllScenes { get; private set; } = 25; // 定死总题目数为 25
    public int totalAnsweredQuestions;                            // 所有场景的总已答题数
    public int totalCorrectAnswers;                               // 所有场景的总正确答题数

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
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogWarning($"场景 {sceneName} 的组别 {groupName} 已初始化，无需重复初始化！");
            return;
        }

        if (!sceneGroupAnswerStatus.ContainsKey(sceneName))
        {
            // 如果场景不存在，初始化场景
            sceneGroupAnswerStatus[sceneName] = new Dictionary<string, List<bool>>();
        }

        // 初始化组别的答题状态列表，默认值为 false
        sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);

        Debug.Log($"场景 {sceneName} 的组别 {groupName} 初始化完成，题目数：{totalQuestions}");
    }

    // 更新答题状态
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName) ||
            !sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogError($"场景 {sceneName} 的组别 {groupName} 未初始化！");
            return;
        }

        // 确保 questionIndex 在合法范围内
        if (questionIndex < 0 || questionIndex >= sceneGroupAnswerStatus[sceneName][groupName].Count)
        {
            Debug.LogError($"题目索引 {questionIndex} 超出范围！");
            return;
        }

        // 如果当前题目未被回答过，更新已答题数
        if (!sceneGroupAnswerStatus[sceneName][groupName][questionIndex])
        {
            totalAnsweredQuestions++;
        }

        // 如果答案正确，更新正确答题数
        if (isCorrect)
        {
            totalCorrectAnswers++;
        }

        // 更新答题状态
        sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = true;
    }

    // 获取全局进度
    public float GetGlobalProgress()
    {
        if (totalQuestionsAllScenes == 0)
        {
            Debug.LogError("所有场景的总题目数为 0，请检查设置！");
            return 0;
        }

        // 返回全局进度（已答题数 / 总题目数）
        return (float)totalAnsweredQuestions / totalQuestionsAllScenes;
    }

    // 获取场景和组别的答题状态
    public List<bool> GetSceneGroupStatus(string sceneName, string groupName)
    {
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName) ||
            !sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogError($"场景 {sceneName} 的组别 {groupName} 未初始化！");
            return null;
        }

        return sceneGroupAnswerStatus[sceneName][groupName];
    }

    // 获取全局正确率
    public float GetGlobalCorrectRate()
    {
        if (totalAnsweredQuestions == 0)
        {
            Debug.LogWarning("尚未答题，无法计算正确率！");
            return 0;
        }

        // 返回全局正确率（正确答题数 / 已答题数）
        return (float)totalCorrectAnswers / totalAnsweredQuestions;
    }
}