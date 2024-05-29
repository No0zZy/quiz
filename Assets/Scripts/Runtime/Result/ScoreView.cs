using TMPro;
using UnityEngine;

namespace HGtest.Result
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetViewActive(bool setActive)
        {
            gameObject.SetActive(setActive);
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}