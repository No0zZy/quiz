using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Answers
{
    public class AnswerButtonView : MonoBehaviour
    {
        public Action<AnswerButtonView> AnswerClicked = delegate(AnswerButtonView s) { };

        public string Answer => _answerText.text;

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _answerText;
        [Header("Colors")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;

        private void OnEnable()
        {
            _button.onClick.AddListener(InvokeAnswerClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(InvokeAnswerClicked);
        }

        public void SetState(AnswerState state)
        {
            switch (state)
            {
                case AnswerState.Default:
                    _image.color = _defaultColor;
                    SetInteractable(true);
                    break;
                case AnswerState.Correct:
                    _image.color = _correctColor;
                    SetInteractable(false);
                    break;
                case AnswerState.Wrong:
                    _image.color = _wrongColor;
                    SetInteractable(false);
                    break;
            }
        }
        
        public void SetAnswerText(string answer)
        {
            _answerText.text = answer;
        }

        public void SetInteractable(bool interactable)
        {
            _button.interactable = interactable;
        }

        private void InvokeAnswerClicked()
        {
            AnswerClicked.Invoke(this);
        }
    }
}