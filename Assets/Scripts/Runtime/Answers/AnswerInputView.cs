using TMPro;
using UnityEngine;

namespace HGtest.Answers
{
    public class AnswerInputView : MonoBehaviour
    {
        public TMP_InputField InputField => _inputField;
        public string Answer => _inputField.text;

        [SerializeField] private TMP_InputField _inputField;
        [Header("Colors")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;

        public void SetState(AnswerState state)
        {
            switch (state)
            {
                case AnswerState.Default:
                    _inputField.image.color = _defaultColor;
                    SetInteractable(true);
                    break;
                case AnswerState.Correct:
                    _inputField.image.color = _correctColor;
                    SetInteractable(false);
                    break;
                case AnswerState.Wrong:
                    _inputField.image.color = _wrongColor;
                    SetInteractable(false);
                    break;
            }
        }
        public void SetInteractable(bool interactable)
        {
            _inputField.interactable = interactable;
        }
    }
}