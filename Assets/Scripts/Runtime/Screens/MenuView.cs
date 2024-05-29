using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Screens
{
    public class MenuView : ScreenView
    {
        public Button PlayButton => _playButton;

        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        public void SetBestScore(int bestScore)
        {
            _bestScoreText.text = bestScore.ToString();
        }
    }
}