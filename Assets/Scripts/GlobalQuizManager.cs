using System.Collections.Generic;
using UnityEngine;

public class GlobalQuizManager : MonoBehaviour
{
    public static GlobalQuizManager Instance { get; private set; }

    // �洢ÿ�����������Ĵ���״̬���������� -> ��� -> ����״̬�б�
    private Dictionary<string, Dictionary<string, List<bool>>> sceneGroupAnswerStatus =
        new Dictionary<string, Dictionary<string, List<bool>>>();

    // ȫ��ͳ��
    public int totalQuestionsAllScenes; // ���г���������Ŀ��
    public int totalCorrectAnswers;     // ���г���������ȷ������

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
        if (!sceneGroupAnswerStatus.ContainsKey(sceneName))
        {
            // ������������ڣ���ʼ������
            sceneGroupAnswerStatus[sceneName] = new Dictionary<string, List<bool>>();
        }

        if (!sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            // ��ʼ�����Ĵ���״̬�б�Ĭ��ֵΪ false
            sceneGroupAnswerStatus[sceneName][groupName] = new List<bool>(new bool[totalQuestions]);

            // ����ȫ������Ŀ��
            totalQuestionsAllScenes += totalQuestions;

            Debug.Log($"���� {sceneName} ����� {groupName} ��ʼ����ɣ�����Ŀ����{totalQuestions}");
        }
        else
        {
            Debug.LogWarning($"���� {sceneName} ����� {groupName} �ѳ�ʼ���������ظ���ʼ����");
        }
    }

    // ���´���״̬
    public void UpdateSceneStatus(string sceneName, string groupName, int questionIndex, bool isCorrect)
    {
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            // ȷ�� questionIndex �ںϷ���Χ��
            if (questionIndex >= 0 && questionIndex < sceneGroupAnswerStatus[sceneName][groupName].Count)
            {
                // ���´���״̬
                sceneGroupAnswerStatus[sceneName][groupName][questionIndex] = isCorrect;

                // �������ȷ������ȫ����ȷ������
                if (isCorrect)
                {
                    totalCorrectAnswers++;
                }
            }
            else
            {
                Debug.LogError($"��Ŀ���� {questionIndex} ������Χ��");
            }
        }
        else
        {
            Debug.LogError($"���� {sceneName} ����� {groupName} δ��ʼ����");
        }
    }

    // ��ȡȫ�ֽ���
    public float GetGlobalProgress()
    {
        if (totalQuestionsAllScenes == 0)
        {
            Debug.LogError("���г���������Ŀ��Ϊ 0���������ã�");
            return 0;
        }

        // �����Ѵ��������
        int totalAnswered = 0;
        foreach (var scene in sceneGroupAnswerStatus.Values)
        {
            foreach (var group in scene.Values)
            {
                totalAnswered += group.Count;
            }
        }

        // ����ȫ�ֽ��ȣ��Ѵ����� / ����Ŀ����
        return (float)totalAnswered / totalQuestionsAllScenes;
    }

    // ��ȡ���������Ĵ���״̬
    public List<bool> GetSceneGroupStatus(string sceneName, string groupName)
    {
        if (sceneGroupAnswerStatus.ContainsKey(sceneName) &&
            sceneGroupAnswerStatus[sceneName].ContainsKey(groupName))
        {
            return sceneGroupAnswerStatus[sceneName][groupName];
        }
        else
        {
            Debug.LogError($"���� {sceneName} ����� {groupName} δ��ʼ����");
            return null;
        }
    }
}