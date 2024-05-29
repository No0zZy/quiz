using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using HGtest.Level;
using VContainer.Unity;

namespace HGtest.Timer
{
    public class TimerController : IInitializable, IDisposable
    {
        private readonly TimerView _timerView;
        private readonly LevelConfig _levelConfig;
        private readonly LevelModel _levelModel;

        private CancellationTokenSource _cts;

        public TimerController(TimerView timerView, LevelConfig levelConfig, LevelModel levelModel)
        {
            _timerView = timerView;
            _levelConfig = levelConfig;
            _levelModel = levelModel;
        }

        public void Initialize()
        {
            _levelModel.StartLevel += ActivateTimerView;
            _levelModel.QuestionStarted += StartTimer;
            _levelModel.AnswerChosen += OnAnswerChosen;
            _levelModel.ShowResults += DeactivateTimerView;
        }

        public void Dispose()
        {
            _levelModel.StartLevel -= ActivateTimerView;
            _levelModel.QuestionStarted -= StartTimer;
            _levelModel.AnswerChosen -= OnAnswerChosen;
            _levelModel.ShowResults -= DeactivateTimerView;

            DisposeToken();
        }

        private void StartTimer() => StartTimerAsync().Forget();

        private async UniTask StartTimerAsync()
        {
            RefreshToken();

            _timerView.SetTimerErrorActive(false);

            _levelModel.RestTime = _levelConfig.TimePerQuestion;
            _timerView.SetTimerText(_levelModel.RestTime);

            _timerView.TimerImage.fillAmount = 1f;
            _timerView.TimerImage.DOFillAmount(0f, _levelConfig.TimePerQuestion)
                .SetEase(Ease.Linear)
                .WithCancellation(_cts.Token)
                .Forget();

            while(_levelModel.RestTime > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: _cts.Token);
                _levelModel.RestTime--;
                _timerView.SetTimerText(_levelModel.RestTime);
            }

            _timerView.SetTimerErrorActive(true);
            _levelModel.InvokeTimerElapsed();
        }

        private void ActivateTimerView() => SetTimerActive(true);
        private void DeactivateTimerView() => SetTimerActive(false);

        private void SetTimerActive(bool setActive)
        {
            _timerView.gameObject.SetActive(setActive);
        }

        private void OnAnswerChosen(bool _)
        {
            DisposeToken();
        }

        private void RefreshToken()
        {
            DisposeToken();
            _cts = new CancellationTokenSource();
        }

        private void DisposeToken()
        {
            if (_cts == null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}