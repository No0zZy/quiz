using System;
using HGtest.Level;
using HGtest.Result;
using HGtest.Screens;
using VContainer.Unity;

namespace HGtest.Gameplay
{
    public class MenuController : IInitializable, IStartable, IDisposable
    {
        private readonly MenuView _menuView;
        private readonly LevelModel _levelModel;
        private readonly HighScoreRecorder _highScoreRecorder;
        private readonly ScreenSwitcher _screenSwitcher;

        public MenuController(MenuView menuView, LevelModel levelModel, HighScoreRecorder highScoreRecorder, 
            ScreenSwitcher screenSwitcher)
        {
            _menuView = menuView;
            _levelModel = levelModel;
            _highScoreRecorder = highScoreRecorder;
            _screenSwitcher = screenSwitcher;
        }

        public void Initialize()
        {
            _menuView.PlayButton.onClick.AddListener(OnPlayButtonClicked);
            _levelModel.GoToMenu += SwitchToMenuScreen;
        }

        public void Start()
        {
            SwitchToMenuScreen();
            _menuView.SetBestScore(_highScoreRecorder.BestScore);
        }

        public void Dispose()
        {
            _menuView.PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
            _levelModel.GoToMenu -= SwitchToMenuScreen;
        }

        private void SwitchToMenuScreen()
        {
            _screenSwitcher.SwitchScreen(_menuView);
            _menuView.SetBestScore(_highScoreRecorder.BestScore);
        }

        private void OnPlayButtonClicked()
        {
            _levelModel.InvokeStartLevel();
        }
    }
}