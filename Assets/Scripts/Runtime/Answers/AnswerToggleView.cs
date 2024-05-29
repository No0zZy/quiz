using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Answers
{
    public class AnswerToggleView : MonoBehaviour
    {
        public Action<AnswerToggleView, bool> AnswerToggled = delegate { };

        public bool IsChosen => _toggle.isOn;
        public string Answer => _answerText.text;

        [SerializeField] private Toggle _toggle;
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private Image[] _images;
        [SerializeField] private Sprite _checkmark;
        [SerializeField] private Sprite _x;
        [Header("Colors")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _correctColor;
        [SerializeField] private Color _wrongColor;
        [SerializeField] private Color _chosenColor;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggle);
        }

        public void SetState(AnswerState state)
        {
            switch (state)
            {
                case AnswerState.Default:
                    ColorizeImages(_defaultColor);
                    _toggle.image.sprite = _checkmark;
                    _toggle.image.SetNativeSize();
                    _toggle.isOn = false;
                    SetInteractable(true);
                    break;
                case AnswerState.Correct:
                    ColorizeImages(_correctColor);
                    SetInteractable(false);
                    break;
                case AnswerState.Wrong:
                    ColorizeImages(_wrongColor);
                    _toggle.image.sprite = _x;
                    _toggle.image.SetNativeSize();
                    SetInteractable(false);
                    break;
                case AnswerState.Chosen:
                    ColorizeImages(_chosenColor);
                    SetInteractable(true);
                    break;
            }
        }

        public void SetAnswerText(string answer)
        {
            _answerText.text = answer;
        }

        public void SetInteractable(bool interactable)
        {
            _toggle.interactable = interactable;
        }

        private void ColorizeImages(Color color)
        {
            foreach (var image in _images)
            {
                image.color = color;
            }
        }

        private void OnToggle(bool isOn)
        {
            AnswerToggled.Invoke(this, isOn);
        }
    }
}