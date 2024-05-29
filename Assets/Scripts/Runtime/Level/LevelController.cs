using System;
using HGtest.Screens;
using UnityEngine;
using VContainer.Unity;

namespace HGtest.Level
{
    public class LevelController : IInitializable, IDisposable
    {
        private readonly LevelGenerator _levelGenerator;
        private readonly LevelModel _levelModel;
        private readonly ScreensModel _screensModel;
        private readonly ScreenSwitcher _screenSwitcher;

        public LevelController(LevelGenerator levelGenerator, LevelModel levelModel, ScreensModel screensModel,
            ScreenSwitcher screenSwitcher)
        {
            _levelGenerator = levelGenerator;
            _levelModel = levelModel;
            _screensModel = screensModel;
            _screenSwitcher = screenSwitcher;
        }

        public void Initialize()
        {
            _levelModel.StartLevel += StartLevel;
            _levelModel.NextQuestion += NextQuestion;
        }

        public void Dispose()
        {
            _levelModel.StartLevel -= StartLevel;
            _levelModel.NextQuestion -= NextQuestion;
        }

        private void StartLevel()
        {
            _levelGenerator.GenerateLevelQuestions();
            SwitchQuestion();
        }

        private void NextQuestion()
        {
            _levelModel.IncreaseQuestionIndex();

            if (_levelModel.IsAllQuestionsDone)
            {
                _levelModel.InvokeShowResults();
                return;
            }

            SwitchQuestion();
        }

        private void SwitchQuestion()
        {
            var question = _levelModel.CurrentQuestion;
            var screen = _screensModel.GetScreenByQuestion(question);

            if (screen is null)
            {
                Debug.LogError($"[{nameof(LevelController)}.{nameof(SwitchQuestion)}] " +
                               $"Screen view is null.");
                return;
            }

            screen.Build(question);
            _screenSwitcher.SwitchScreen(screen);

            _levelModel.InvokeQuestionStarted();
        }
    }
}