using HGtest.Answers;
using HGtest.Gameplay;
using HGtest.Questions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Screens
{
    public class TextInputView : QuestionScreenView
    {
        public AnswerInputView AnswerInputView => _answerInputView;
        public ButtonConfirmView ConfirmButton => _confirmButton;
        public Button NextButton => _nextButton;
        public BackgroundPanelView BackgroundPanel => _backgroundPanel;

        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private ButtonConfirmView _confirmButton;
        [SerializeField] private BackgroundPanelView _backgroundPanel;
        [SerializeField] private AnswerInputView _answerInputView;

        protected override void BuildView(IQuestion question)
        {
            if (question is not TextInputQuestion textInputQuestion)
            {
                Debug.LogError($"[{nameof(SingleChoiceView)}.{nameof(BuildView)}] " +
                               $"Another type of question.");
                return;
            }

            _backgroundPanel.SetState(BackgroundState.Default);
            _nextButton.gameObject.SetActive(false);
            _questionText.text = question.QuestionText;

            _confirmButton.SetState(ButtonConfirmState.Inactive);
            _answerInputView.SetState(AnswerState.Default);
            _answerInputView.InputField.text = string.Empty;
        }
    }
}