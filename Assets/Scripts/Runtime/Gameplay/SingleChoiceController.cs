using System;
using HGtest.Answers;
using HGtest.Level;
using HGtest.Screens;
using VContainer.Unity;

namespace HGtest.Gameplay
{
    public class SingleChoiceController : IInitializable, IDisposable
    {
        private readonly SingleChoiceView _singleChoiceView;
        private readonly LevelModel _levelModel;

        public SingleChoiceController(SingleChoiceView singleChoiceView, LevelModel levelModel)
        {
            _singleChoiceView = singleChoiceView;
            _levelModel = levelModel;
        }

        public void Initialize()
        {
            _singleChoiceView.NextButton.onClick.AddListener(_levelModel.InvokeNextQuestion);

            foreach (var answerView in _singleChoiceView.AnswerViews)
            {
                answerView.AnswerClicked += AnswerChosen;
            }

            _levelModel.TimerElapsed += OnTimerElapsed;
        }

        public void Dispose()
        {
            _singleChoiceView.NextButton.onClick.RemoveListener(_levelModel.InvokeNextQuestion);

            foreach (var answerView in _singleChoiceView.AnswerViews)
            {
                answerView.AnswerClicked -= AnswerChosen;
            }

            _levelModel.TimerElapsed -= OnTimerElapsed;
        }

        private void AnswerChosen(AnswerButtonView answerView)
        {
            if (_levelModel.CurrentQuestion.CheckAnswer(answerView.Answer))
            {
                _levelModel.InvokeAnswerChosen(true);
                answerView.SetState(AnswerState.Correct);
            }
            else
            {
                _levelModel.InvokeAnswerChosen(false);
                answerView.SetState(AnswerState.Wrong);
            }

            foreach (var answer in _singleChoiceView.AnswerViews)
            {
                answer.SetInteractable(false);
            }

            _singleChoiceView.NextButton.gameObject.SetActive(true);
        }

        private void OnTimerElapsed()
        {
            foreach (var answerView in _singleChoiceView.AnswerViews)
            {
                answerView.SetState(AnswerState.Wrong);
            }

            _singleChoiceView.BackgroundPanel.SetState(BackgroundState.Wrong);

            _singleChoiceView.NextButton.gameObject.SetActive(true);
        }
    }
}