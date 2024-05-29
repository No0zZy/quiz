namespace HGtest.Screens
{
    public class ScreenSwitcher
    {
        private IScreenView _currentScreen;

        public void SwitchScreen(IScreenView screenView)
        {
            _currentScreen?.SetScreenActive(false);
            _currentScreen = screenView;
            _currentScreen.SetScreenActive(true);
        }
    }
}