using UnityEngine;

namespace HGtest.Screens
{
    public class ScreenView : MonoBehaviour, IScreenView
    {
        public void SetScreenActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}