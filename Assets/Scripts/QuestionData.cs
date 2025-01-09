using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/QuestionData")]
public class QuestionData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string questionText; // ��Ŀ����
        public string[] options;    // ѡ��
        public int correctOption;   // ��ȷ�𰸵�����
    }

    public string groupName; // ��Ŀ������
    public Question[] questions; // ��Ŀ�б�
}