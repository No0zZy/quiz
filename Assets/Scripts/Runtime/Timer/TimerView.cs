using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Timer
{
    public class TimerView : MonoBehaviour
    {
        public Image TimerImage => _timerImage;

        [SerializeField] private Image _timerImage;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private GameObject _timerErrorGO;

        public void SetTimerText(int timer)
        {
            _timerText.text = timer.ToString();
        }

        public void SetTimerErrorActive(bool setActive)
        {
            _timerErrorGO.SetActive(setActive);
        }
    }
}