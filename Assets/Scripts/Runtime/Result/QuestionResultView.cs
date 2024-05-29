using TMPro;
using UnityEngine;

namespace HGtest.Result
{
    public class QuestionResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _questionNumberText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _correctState;
        [SerializeField] private GameObject _wrongState;

        public void SetQuestionNumber(int number)
        {
            _questionNumberText.text = number.ToString();
        }

        public void SetQuestionScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void SetViewState(QuestionResultState state)
        {
            switch (state)
            {
                case QuestionResultState.Correct:
                    _correctState.SetActive(true);
                    _wrongState.SetActive(false);
                    break;
                case QuestionResultState.Wrong:
                    _correctState.SetActive(false);
                    _wrongState.SetActive(true);
                    break;
            }
        }
    }
}