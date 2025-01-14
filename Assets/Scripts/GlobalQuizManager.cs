using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // 存储每个场景和组别的答题状态（场景名称 -> 组别 -> 答题状态列表）
    public Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // 存储每个组别是否已经统计过
    private Dictionary<string, HashSet<string>> groupCompletionStatus =
        new Dictionary<string, HashSet<string>>();

    // 全局统计
    public int totalQuestionsAllScenes { get; private set; } = 25;
    public int totalAnsweredQuestions;                            // 所有场景的总已答题数
    public int totalCorrectAnswers;                               // 所有场景的总正确答题数

    // 定义事件，用于通知答题状态更新
    public delegate void AnswerUpdatedHandler();
    public static event AnswerUpdatedHandler OnAnswerUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        }

        // 初始化组别完成状态
        if (!groupCompletionStatus.ContainsKey(sceneName))
        {
            groupCompletionStatus[sceneName] = new HashSet<string>();
        }
    }

    // 更新答题状态
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        // 更新当前题目的答题状态
        sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = true;

        // 检查当前组别是否已完成
        if (IsGroupComplete(sceneName, groupName))
        {
            // 如果组别未统计过，则统计一次
            if (!groupCompletionStatus[sceneName].Contains(groupName))
            {
                groupCompletionStatus[sceneName].Add(groupName);

                // 统计该组的总答题数和正确答题数
                int groupAnsweredQuestions = sceneGroupAnswerStatus[sceneName][groupName].Count;
                int groupCorrectAnswers = 0;

                foreach (bool isAnsweredCorrectly in sceneGroupAnswerStatus[sceneName][groupName])
                {
                    if (isAnsweredCorrectly)
                    {
                        groupCorrectAnswers++;
                    }
                }

                // 更新全局统计
                totalAnsweredQuestions += groupAnsweredQuestions;
                totalCorrectAnswers += groupCorrectAnswers;

                // 触发事件
                OnAnswerUpdated?.Invoke();
            }
        }
    }

    // 检查当前组别是否已完成
    private bool IsGroupComplete(string sceneName, string groupName)
    {
        foreach (bool isAnswered in sceneGroupAnswerStatus[sceneName][groupName])
        {
            if (!isAnswered)
            {
                return false;
            }
        }
        return true;
    }

    public float GetGlobalProgress()
    {
        return (float)totalAnsweredQuestions / totalQuestionsAllScenes;
    }

    public List<bool> GetSceneGroupStatus(string sceneName, string groupName)
    {
        return sceneGroupAnswerStatus[sceneName][groupName];
    }

    public float GetGlobalCorrectRate()
    {
        return (float)totalCorrectAnswers / totalAnsweredQuestions;
    }
}