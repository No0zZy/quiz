using UnityEngine;

namespace HGtest.Result
{
    [CreateAssetMenu(fileName = nameof(ResultScreenConfig), menuName = "HGtest/" + nameof(ResultScreenConfig))]
    public class ResultScreenConfig : ScriptableObject
    {
        public int ColumnsCount => _columnsCount;
        public ResultHorizontalGroup HorizontalGroupPrefab => _horizontalGroupPrefab;
        public QuestionResultView QuestionResultPrefab => _questionResultPrefab;

        [SerializeField] private int _columnsCount;
        [SerializeField] private ResultHorizontalGroup _horizontalGroupPrefab;
        [SerializeField] private QuestionResultView _questionResultPrefab;
    }
}