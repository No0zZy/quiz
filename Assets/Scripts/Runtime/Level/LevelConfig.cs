using HGtest.Questions;
using UnityEngine;

namespace HGtest.Level
{
    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "HGtest/" + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject
    {
        public int TimePerQuestion => _timePerQuestion;
        public int QuestionsCount => _questionsCount;
        public QuestionsBundleConfig[] UsedBundles => _usedBundles;

        [SerializeField] private int _timePerQuestion;
        [SerializeField] private int _questionsCount;
        [SerializeField] private QuestionsBundleConfig[] _usedBundles;
    }
}