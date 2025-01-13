using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // �洢ÿ�����������Ĵ���״̬���������� -> ��� -> ����״̬�б�
    public Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

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

        // ��ʼ�����Ĵ���״̬�б�Ĭ��ֵΪ false
        sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);
    }

    // ���´���״̬
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        if (!sceneGroupAnswerStatus[sceneName][groupName][questionIndex])
        {
            totalAnsweredQuestions++;
        }
        if (isCorrect)
        {
            totalCorrectAnswers++;
        }

        sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = true;
        OnAnswerUpdated?.Invoke();
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