using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // �洢ÿ�����������Ĵ���״̬���������� -> ��� -> ����״̬�б�
    public Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // ȫ��ͳ��
    public int totalQuestionsAllScenes { get; private set; } = 25; // ��������Ŀ��Ϊ 25
    public int totalAnsweredQuestions;                            // ���г��������Ѵ�����
    public int totalCorrectAnswers;                               // ���г���������ȷ������

    private void Awake()
    {
        // ����ģʽ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �糡������
        }
        else
        {
            Destroy(gameObject); // ��ֹ�ظ�����
        }
    }

    // ��ʼ�����������Ĵ���״̬
    public void InitializeSceneStatus(string sceneName, string groupName, int totalQuestions)
    {
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogWarning($"���� {sceneName} ����� {groupName} �ѳ�ʼ���������ظ���ʼ����");
            return;
        }

        if (!sceneGroupAnswerStatus.ContainsKey(sceneName))
        {
            // ������������ڣ���ʼ������
            sceneGroupAnswerStatus[sceneName] = new Dictionary<string, List<bool>>();
        }

        // ��ʼ�����Ĵ���״̬�б�Ĭ��ֵΪ false
        sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);

        Debug.Log($"���� {sceneName} ����� {groupName} ��ʼ����ɣ���Ŀ����{totalQuestions}");
    }

    // ���´���״̬
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName) ||
            !sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogError($"���� {sceneName} ����� {groupName} δ��ʼ����");
            return;
        }

        // ȷ�� questionIndex �ںϷ���Χ��
        if (questionIndex < 0 || questionIndex >= sceneGroupAnswerStatus[sceneName][groupName].Count)
        {
            Debug.LogError($"��Ŀ���� {questionIndex} ������Χ��");
            return;
        }

        // �����ǰ��Ŀδ���ش���������Ѵ�����
        if (!sceneGroupAnswerStatus[sceneName][groupName][questionIndex])
        {
            totalAnsweredQuestions++;
        }

        // �������ȷ��������ȷ������
        if (isCorrect)
        {
            totalCorrectAnswers++;
        }

        // ���´���״̬
        sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = true;
    }

    // ��ȡȫ�ֽ���
    public float GetGlobalProgress()
    {
        if (totalQuestionsAllScenes == 0)
        {
            Debug.LogError("���г���������Ŀ��Ϊ 0���������ã�");
            return 0;
        }

        // ����ȫ�ֽ��ȣ��Ѵ����� / ����Ŀ����
        return (float)totalAnsweredQuestions / totalQuestionsAllScenes;
    }

    // ��ȡ���������Ĵ���״̬
    public List<bool> GetSceneGroupStatus(string sceneName, string groupName)
    {
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName) ||
            !sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            Debug.LogError($"���� {sceneName} ����� {groupName} δ��ʼ����");
            return null;
        }

        return sceneGroupAnswerStatus[sceneName][groupName];
    }

    // ��ȡȫ����ȷ��
    public float GetGlobalCorrectRate()
    {
        if (totalAnsweredQuestions == 0)
        {
            Debug.LogWarning("��δ���⣬�޷�������ȷ�ʣ�");
            return 0;
        }

        // ����ȫ����ȷ�ʣ���ȷ������ / �Ѵ�������
        return (float)totalCorrectAnswers / totalAnsweredQuestions;
    }
}