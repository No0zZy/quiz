using System;
using HGtest.Level;
using VContainer.Unity;

namespace HGtest.Result
{
    public class ScoreController : IInitializable, IDisposable
    {
        private readonly LevelModel _levelModel;
        private readonly ScoreView _scoreView;

        public ScoreController(LevelModel levelModel, ScoreView scoreView)
        {
            _levelModel = levelModel;
            _scoreView = scoreView;
        }

        public void Initialize()
        {
            _levelModel.StartLevel += OnStartLevel;
            _levelModel.TimerElapsed += OnTimerElapsed;
            _levelModel.AnswerChosen += OnAnswerChosen;
            _levelModel.ShowResults += OnShowResults;
        }

        public void Dispose()
        {
            _levelModel.StartLevel -= OnStartLevel;
            _levelModel.TimerElapsed -= OnTimerElapsed;
            _levelModel.AnswerChosen -= OnAnswerChosen;
            _levelModel.ShowResults -= OnShowResults;
        }

        private void OnShowResults()
        {
            _scoreView.SetViewActive(false);
        }

        private void OnStartLevel()
        {
            _scoreView.SetViewActive(true);
        }

        private void OnAnswerChosen(bool isCorrect)
        {
            _levelModel.SaveResultOfQuestion(isCorrect ? _levelModel.RestTime : 0);
            _scoreView.SetScore(_levelModel.CurrentScore);
        }

        private void OnTimerElapsed()
        {
            _levelModel.SaveResultOfQuestion(0);
        }
    }
}