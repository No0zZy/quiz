using System;
using System.Collections.Generic;
using HGtest.Level;
using HGtest.Screens;
using HGtest.Utils;
using VContainer.Unity;

namespace HGtest.Result
{
    public class ResultScreenController : IInitializable, IDisposable
    {
        private readonly LevelModel _levelModel;
        private readonly ResultScreenView _resultScreenView;
        private readonly ScreenSwitcher _screenSwitcher;
        private readonly HighScoreRecorder _highScoreRecorder;
        private readonly ResultScreenConfig _resultScreenConfig;

        private MonoBehaviourPool<ResultHorizontalGroup> _horizontalGroupPool;
        private MonoBehaviourPool<QuestionResultView> _resultViewPool;

        private List<ResultHorizontalGroup> _spawnedGroups = new List<ResultHorizontalGroup>(4);
        private List<QuestionResultView> _spawnedResultView = new List<QuestionResultView>(10);

        public ResultScreenController(LevelModel levelModel, ResultScreenView resultScreenView,
            ScreenSwitcher screenSwitcher, HighScoreRecorder highScoreRecorder, ResultScreenConfig resultScreenConfig)
        {
            _levelModel = levelModel;
            _resultScreenView = resultScreenView;
            _screenSwitcher = screenSwitcher;
            _highScoreRecorder = highScoreRecorder;
            _resultScreenConfig = resultScreenConfig;
        }

        public void Initialize()
        {
            _horizontalGroupPool = new MonoBehaviourPool<ResultHorizontalGroup>(
                _resultScreenConfig.HorizontalGroupPrefab,
                _resultScreenView.ResultViewsParent);

            _resultViewPool = new MonoBehaviourPool<QuestionResultView>(_resultScreenConfig.QuestionResultPrefab,
                _resultScreenView.ResultViewsParent);

            _levelModel.ShowResults += OnShowResults;
            _resultScreenView.MenuButton.onClick.AddListener(GoToMenu);
        }

        public void Dispose()
        {
            _levelModel.ShowResults -= OnShowResults;
            _resultScreenView.MenuButton.onClick.RemoveListener(GoToMenu);
        }

        private void OnShowResults()
        {
            _screenSwitcher.SwitchScreen(_resultScreenView);
            _resultScreenView.SetScore(_levelModel.CurrentScore);
            _highScoreRecorder.TrySaveNewBestScore(_levelModel.CurrentScore);

            ConfigureResultView();
        }

        private void GoToMenu()
        {
            ResetViews();
            _levelModel.InvokeGoToMenu();
        }

        private void ConfigureResultView()
        {
            var currentGroup = _horizontalGroupPool.Get();
            _spawnedGroups.Add(currentGroup);

            for (int i = 0; i < _levelModel.QuestionScores.Count; i++)
            {
                if (i > 0 && i % _resultScreenConfig.ColumnsCount == 0)
                {
                    currentGroup = _horizontalGroupPool.Get();
                    _spawnedGroups.Add(currentGroup);
                }

                var resultView = _resultViewPool.Get();
                resultView.transform.SetParent(currentGroup.transform);

                resultView.SetQuestionNumber(i + 1);

                var score = _levelModel.QuestionScores[i];

                if (score > 0)
                {
                    resultView.SetViewState(QuestionResultState.Correct);
                    resultView.SetQuestionScore(score);
                }
                else
                {
                    resultView.SetViewState(QuestionResultState.Wrong);
                }

                _spawnedResultView.Add(resultView);
            }
        }

        private void ResetViews()
        {
            foreach (var group in _spawnedGroups)
            {
                _horizontalGroupPool.ReturnToPool(group);
            }

            foreach (var resultView in _spawnedResultView)
            {
                _resultViewPool.ReturnToPool(resultView);
            }

            _spawnedGroups.Clear();
            _spawnedResultView.Clear();
        }
    }
}