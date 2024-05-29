using System;
using System.Linq;
using HGtest.Answers;
using HGtest.Level;
using HGtest.Screens;
using VContainer.Unity;

namespace HGtest.Gameplay
{
    public class MultipleChoiceController : IInitializable, IDisposable
    {
        private readonly MultipleChoiceView _multipleChoiceView;
        private readonly LevelModel _levelModel;

        public MultipleChoiceController(MultipleChoiceView multipleChoiceView, LevelModel levelModel)
        {
            _multipleChoiceView = multipleChoiceView;
            _levelModel = levelModel;
        }

        public void Initialize()
        {
            _multipleChoiceView.NextButton.onClick.AddListener(_levelModel.InvokeNextQuestion);
            _multipleChoiceView.ConfirmButton.Button.onClick.AddListener(ConfirmAnswer);
            _levelModel.TimerElapsed += OnTimerElapsed;

            foreach (var answerView in _multipleChoiceView.AnswerViews)
            {
                answerView.AnswerToggled += OnToggleAnswer;
            }
        }

        public void Dispose()
        {
            _multipleChoiceView.NextButton.onClick.RemoveListener(_levelModel.InvokeNextQuestion);
            _multipleChoiceView.ConfirmButton.Button.onClick.RemoveListener(ConfirmAnswer);
            _levelModel.TimerElapsed -= OnTimerElapsed;

            foreach (var answerView in _multipleChoiceView.AnswerViews)
            {
                answerView.AnswerToggled -= OnToggleAnswer;
            }
        }

        private void OnToggleAnswer(AnswerToggleView answerToggleView, bool isOn)
        {
            answerToggleView.SetState(isOn ? AnswerState.Chosen : AnswerState.Default);

            _multipleChoiceView.ConfirmButton.SetState(_multipleChoiceView.AnswerViews.Count(x => x.IsChosen) > 0
                ? ButtonConfirmState.Active
                : ButtonConfirmState.Inactive);
        }

        private void ConfirmAnswer()
        {
            var chosenAnswers = _multipleChoiceView.AnswerViews.Where(x => x.IsChosen).ToList();
            var answerStrings = chosenAnswers.Select(a => a.Answer).ToArray();

            if (_levelModel.CurrentQuestion.CheckAnswer(answerStrings))
            {
                _levelModel.InvokeAnswerChosen(true);
                foreach (var answer in chosenAnswers)
                {
                    answer.SetState(AnswerState.Correct);
                }
            }
            else
            {
                _levelModel.InvokeAnswerChosen(false);
                foreach (var answer in chosenAnswers)
                {
                    answer.SetState(AnswerState.Wrong);
                }
            }

            _multipleChoiceView.ConfirmButton.SetState(ButtonConfirmState.Disable);
            _multipleChoiceView.NextButton.gameObject.SetActive(true);
        }

        private void OnTimerElapsed()
        {
            foreach (var answer in _multipleChoiceView.AnswerViews)
            {
                answer.SetState(AnswerState.Wrong);
            }

            _multipleChoiceView.BackgroundPanel.SetState(BackgroundState.Wrong);

            _multipleChoiceView.NextButton.gameObject.SetActive(true);
            _multipleChoiceView.ConfirmButton.SetState(ButtonConfirmState.Disable);
        }
    }
}