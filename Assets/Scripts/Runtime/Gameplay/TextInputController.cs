using System;
using HGtest.Answers;
using HGtest.Level;
using HGtest.Screens;
using Sirenix.Utilities;
using VContainer.Unity;

namespace HGtest.Gameplay
{
    public class TextInputController : IInitializable, IDisposable
    {
        private readonly TextInputView _textInputView;
        private readonly LevelModel _levelModel;

        public TextInputController(TextInputView textInputView, LevelModel levelModel)
        {
            _textInputView = textInputView;
            _levelModel = levelModel;
        }

        public void Initialize()
        {
            _textInputView.NextButton.onClick.AddListener(_levelModel.InvokeNextQuestion);
            _textInputView.ConfirmButton.Button.onClick.AddListener(ConfirmAnswer);
            _levelModel.TimerElapsed += OnTimerElapsed;
            _textInputView.AnswerInputView.InputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        }

        public void Dispose()
        {
            _textInputView.NextButton.onClick.RemoveListener(_levelModel.InvokeNextQuestion);
            _textInputView.ConfirmButton.Button.onClick.RemoveListener(ConfirmAnswer);
            _levelModel.TimerElapsed -= OnTimerElapsed;
            _textInputView.AnswerInputView.InputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
        }

        private void OnInputFieldValueChanged(string value)
        {
            _textInputView.ConfirmButton.SetState(!value.IsNullOrWhitespace()
                ? ButtonConfirmState.Active
                : ButtonConfirmState.Inactive);
        }

        private void ConfirmAnswer()
        {
            if (_levelModel.CurrentQuestion.CheckAnswer(_textInputView.AnswerInputView.Answer))
            {
                _levelModel.InvokeAnswerChosen(true);
                _textInputView.AnswerInputView.SetState(AnswerState.Correct);
            }
            else
            {
                _levelModel.InvokeAnswerChosen(false);
                _textInputView.AnswerInputView.SetState(AnswerState.Wrong);
            }

            _textInputView.ConfirmButton.SetState(ButtonConfirmState.Disable);
            _textInputView.NextButton.gameObject.SetActive(true);
        }

        private void OnTimerElapsed()
        {
            _textInputView.AnswerInputView.SetState(AnswerState.Wrong);

            _textInputView.BackgroundPanel.SetState(BackgroundState.Wrong);

            _textInputView.NextButton.gameObject.SetActive(true);
            _textInputView.ConfirmButton.SetState(ButtonConfirmState.Disable);
        }
    }
}