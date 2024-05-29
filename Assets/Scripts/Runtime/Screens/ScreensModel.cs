using HGtest.Questions;
using JetBrains.Annotations;
using UnityEngine;

namespace HGtest.Screens
{
    public class ScreensModel
    {
        private readonly SingleChoiceView _singleChoiceView;
        private readonly MultipleChoiceView _multipleChoiceView;
        private readonly TextInputView _textInputView;

        public ScreensModel(SingleChoiceView singleChoiceView, MultipleChoiceView multipleChoiceView,
            TextInputView textInputView)
        {
            _singleChoiceView = singleChoiceView;
            _multipleChoiceView = multipleChoiceView;
            _textInputView = textInputView;
        }

        [CanBeNull]
        public QuestionScreenView GetScreenByQuestion(IQuestion question)
        {
            switch (question)
            {
                case SingleChoiceQuestion:
                    return _singleChoiceView;
                case MultipleChoiceQuestion:
                    return _multipleChoiceView;
                case TextInputQuestion:
                    return _textInputView;
            }

            Debug.LogError($"[{nameof(ScreensModel)}.{nameof(GetScreenByQuestion)}] " +
                           $"There is no screen view for chosen question type.");

            return null;
        }
    }
}