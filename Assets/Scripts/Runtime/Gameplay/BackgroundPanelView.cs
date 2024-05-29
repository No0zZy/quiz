using UnityEngine;
using UnityEngine.UI;

namespace HGtest.Gameplay
{
    public class BackgroundPanelView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundPanel;
        [Header("Colors")]
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _wrongColor;

        public void SetState(BackgroundState state)
        {
            switch (state)
            {
                case BackgroundState.Default:
                    _backgroundPanel.color = _defaultColor;
                    break;
                case BackgroundState.Wrong:
                    _backgroundPanel.color = _wrongColor;
                    break;
            }
        }
    }
}