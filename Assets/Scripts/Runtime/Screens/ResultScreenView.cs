using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Screens
{
    public class ResultScreenView : ScreenView
    {
        public Button MenuButton => _menuButton;
        public Transform ResultViewsParent => _resultViewsParent;

        [SerializeField] private Transform _resultViewsParent;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}