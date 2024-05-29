using System.Collections.Generic;
using System.Linq;
using HGtest.Answers;
using HGtest.Gameplay;
using HGtest.Questions;
using HGtest.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Screens
{
    public class MultipleChoiceView : QuestionScreenView
    {
        public ButtonConfirmView ConfirmButton => _confirmButton;
        public Button NextButton => _nextButton;
        public BackgroundPanelView BackgroundPanel => _backgroundPanel;
        public AnswerToggleView[] AnswerViews => _answerViews;

        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private ButtonConfirmView _confirmButton;
        [SerializeField] private BackgroundPanelView _backgroundPanel;
        [SerializeField] private AnswerToggleView[] _answerViews;

        protected override void BuildView(IQuestion question)
        {
            if (question is not MultipleChoiceQuestion multipleChoiceQuestion)
            {
                Debug.LogError($"[{nameof(SingleChoiceView)}.{nameof(BuildView)}] " +
                               $"Another type of question.");
                return;
            }

            _backgroundPanel.SetState(BackgroundState.Default);
            _nextButton.gameObject.SetActive(false);
            _questionText.text = question.QuestionText;

            _confirmButton.SetState(ButtonConfirmState.Inactive);

            var answers = new List<string>(4);

            answers.AddRange(multipleChoiceQuestion.IncorrectAnswers);
            answers.AddRange(multipleChoiceQuestion.CorrectAnswers);

            if (answers.Count != _answerViews.Length)
            {
                Debug.LogError($"[{nameof(SingleChoiceView)}.{nameof(BuildView)}] " +
                               $"Count of answers not equal to count of views.");
                return;
            }

            answers = answers.Shuffle().ToList();

            for (int i = 0; i < answers.Count; i++)
            {
                _answerViews[i].SetAnswerText(answers[i]);
                _answerViews[i].SetState(AnswerState.Default);
            }
        }
    }
}