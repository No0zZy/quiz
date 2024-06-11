using HGtest.Gameplay;
using HGtest.Level;
using HGtest.Result;
using HGtest.Screens;
using HGtest.Storage;
using HGtest.Timer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HGtest
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private ResultScreenConfig _resultScreenConfig;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private MenuView _menuView;
        [SerializeField] private SingleChoiceView _singleChoiceView;
        [SerializeField] private MultipleChoiceView _multipleChoiceView;
        [SerializeField] private TextInputView _textInputView;
        [SerializeField] private ResultScreenView _resultScreenView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_levelConfig);
            builder.RegisterComponent(_resultScreenConfig);

            builder.RegisterInstance(_timerView);
            builder.RegisterInstance(_scoreView);
            builder.RegisterInstance(_menuView);
            builder.RegisterInstance(_singleChoiceView);
            builder.RegisterInstance(_multipleChoiceView);
            builder.RegisterInstance(_textInputView);
            builder.RegisterInstance(_resultScreenView);

            builder.Register<LevelModel>(Lifetime.Singleton);
            builder.Register<ScreensModel>(Lifetime.Singleton);
            builder.Register<ScreenSwitcher>(Lifetime.Singleton);
            builder.Register<LevelGenerator>(Lifetime.Singleton);
            builder.Register<HighScoreRecorder>(Lifetime.Singleton);
            builder.Register<PlayerPrefsStorage>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.RegisterEntryPoint<TimerController>();
            builder.RegisterEntryPoint<MenuController>();
            builder.RegisterEntryPoint<LevelController>();
            builder.RegisterEntryPoint<ResultScreenController>();
            builder.RegisterEntryPoint<ScoreController>();
            builder.RegisterEntryPoint<SingleChoiceController>();
            builder.RegisterEntryPoint<TextInputController>();
            builder.RegisterEntryPoint<MultipleChoiceController>();
        }
    }
}