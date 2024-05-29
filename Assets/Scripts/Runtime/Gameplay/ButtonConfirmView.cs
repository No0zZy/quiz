using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Gameplay
{
    public class ButtonConfirmView : MonoBehaviour
    {
        public Button Button => _button;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;
        [Header("Colors")]
        [SerializeField] private Color _activeButtonColor;
        [SerializeField] private Color _activeTextColor;
        [SerializeField] private Color _inactiveButtonColor;
        [SerializeField] private Color _inactiveTextColor;

        public void SetState(ButtonConfirmState state)
        {
            switch (state)
            {
                case ButtonConfirmState.Active:
                    gameObject.SetActive(true);
                    _button.interactable = true;
                    _image.color = _activeButtonColor;
                    _text.color = _activeTextColor;
                    break;
                case ButtonConfirmState.Inactive:
                    gameObject.SetActive(true);
                    _button.interactable = false;
                    _image.color = _inactiveButtonColor;
                    _text.color = _inactiveTextColor;
                    break;
                case ButtonConfirmState.Disable:
                    _button.interactable = false;
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}