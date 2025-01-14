using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // �洢ÿ�����������Ĵ���״̬���������� -> ��� -> ����״̬�б�
    public Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // �洢ÿ������Ƿ��Ѿ�ͳ�ƹ�
    private Dictionary<string, HashSet<string>> groupCompletionStatus =
        new Dictionary<string, HashSet<string>>();

    // ȫ��ͳ��
    public int totalQuestionsAllScenes { get; private set; } = 25;
    public int totalAnsweredQuestions;                            // ���г��������Ѵ�����
    public int totalCorrectAnswers;                               // ���г���������ȷ������

    // �����¼�������֪ͨ����״̬����
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
            // ������������ڣ���ʼ������
            sceneGroupAnswerStatus[sceneName] = new Dictionary<string, List<bool>>();
        }

        if (!sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            // ��ʼ�����Ĵ���״̬�б�Ĭ��ֵΪ false
            sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);
        }

        // ��ʼ��������״̬
        if (!groupCompletionStatus.ContainsKey(sceneName))
        {
            groupCompletionStatus[sceneName] = new HashSet<string>();
        }
    }

    // ���´���״̬
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        // ���µ�ǰ��Ŀ�Ĵ���״̬
        sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = true;

        // ��鵱ǰ����Ƿ������
        if (IsGroupComplete(sceneName, groupName))
        {
            // ������δͳ�ƹ�����ͳ��һ��
            if (!groupCompletionStatus[sceneName].Contains(groupName))
            {
                groupCompletionStatus[sceneName].Add(groupName);

                // ͳ�Ƹ�����ܴ���������ȷ������
                int groupAnsweredQuestions = sceneGroupAnswerStatus[sceneName][groupName].Count;
                int groupCorrectAnswers = 0;

                foreach (bool isAnsweredCorrectly in sceneGroupAnswerStatus[sceneName][groupName])
                {
                    if (isAnsweredCorrectly)
                    {
                        groupCorrectAnswers++;
                    }
                }

                // ����ȫ��ͳ��
                totalAnsweredQuestions += groupAnsweredQuestions;
                totalCorrectAnswers += groupCorrectAnswers;

                // �����¼�
                OnAnswerUpdated?.Invoke();
            }
        }
    }

    // ��鵱ǰ����Ƿ������
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