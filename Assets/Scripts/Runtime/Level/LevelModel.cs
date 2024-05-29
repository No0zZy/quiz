using System;
using System.Collections.Generic;
using HGtest.Questions;

namespace HGtest.Level
{
    public class LevelModel
    {
        public event Action StartLevel = delegate { };
        public event Action NextQuestion = delegate { };
        public event Action QuestionStarted = delegate { };
        /// <summary>
        /// bool - is correct
        /// </summary>
        public event Action<bool> AnswerChosen = delegate { };
        public event Action TimerElapsed = delegate { };
        public event Action ShowResults = delegate { };
        public event Action GoToMenu = delegate { };

        public int RestTime { get; set; }
        public int CurrentScore { get; set; }

        public bool IsAllQuestionsDone => _currentQuestionIndex >= _questions.Count; 
        public IQuestion CurrentQuestion => _questions[_currentQuestionIndex];

        public Dictionary<int, int> QuestionScores { get; } = new Dictionary<int, int>();
        private List<IQuestion> _questions = new List<IQuestion>();
        private int _currentQuestionIndex;

        public void SetQuestions(IEnumerable<IQuestion> levelQuestions)
        {
            ResetToDefault();
            _questions.AddRange(levelQuestions);
        }

        public void SaveResultOfQuestion(int result)
        {
            CurrentScore += result;
            QuestionScores[_currentQuestionIndex] = result;
        }

        public void IncreaseQuestionIndex()
        {
            _currentQuestionIndex++;
        }

        private void ResetToDefault()
        {
            QuestionScores.Clear();
            _questions.Clear();
            CurrentScore = 0;
            _currentQuestionIndex = 0;
        }

        public void InvokeStartLevel()
        {
            StartLevel.Invoke();
        }

        public void InvokeNextQuestion()
        {
            NextQuestion.Invoke();
        }

        public void InvokeQuestionStarted()
        {
            QuestionStarted.Invoke();
        }

        public void InvokeAnswerChosen(bool isCorrect)
        {
            AnswerChosen.Invoke(isCorrect);
        }

        public void InvokeTimerElapsed()
        {
            TimerElapsed.Invoke();
        }

        public void InvokeShowResults()
        {
            ShowResults.Invoke();
        }

        public void InvokeGoToMenu()
        {
            GoToMenu.Invoke();
        }
    }
}