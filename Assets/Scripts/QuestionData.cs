using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/QuestionData")]
public class QuestionData : ScriptableObject
{
    [System.Serializable]
    public class Question
    {
        public string questionText; // 题目内容
        public string[] options;    // 选项
        public int correctOption;   // 正确答案的索引
    }

    public string groupName; // 题目组名称
    public Question[] questions; // 题目列表
}