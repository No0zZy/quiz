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
    public class SingleChoiceView : QuestionScreenView
    {
        public Button NextButton => _nextButton;
        public AnswerButtonView[] AnswerViews => _answerViews;
        public BackgroundPanelView BackgroundPanel => _backgroundPanel;

        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private AnswerButtonView[] _answerViews;
        [SerializeField] private BackgroundPanelView _backgroundPanel;

        protected override void BuildView(IQuestion question)
        {
            if (question is not SingleChoiceQuestion singleChoiceQuestion)
            {
                Debug.LogError($"[{nameof(SingleChoiceView)}.{nameof(BuildView)}] " +
                               $"Another type of question.");
                return;
            }

            _backgroundPanel.SetState(BackgroundState.Default);
            _nextButton.gameObject.SetActive(false);
            _questionText.text = question.QuestionText;

            var answers = new List<string>(4);
            
            answers.AddRange(singleChoiceQuestion.IncorrectAnswers);
            answers.Add(singleChoiceQuestion.CorrectAnswer);

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