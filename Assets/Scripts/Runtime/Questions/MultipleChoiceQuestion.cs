using System;
using System.Linq;
using UnityEngine;

namespace HGtest.Questions
{
    [Serializable]
    public class MultipleChoiceQuestion : IQuestion
    {
        public string QuestionText => _questionText;
        public string[] IncorrectAnswers => _incorrectAnswers;
        public string[] CorrectAnswers => _correctAnswers;

        [SerializeField] private string _questionText;
        [SerializeField] private string[] _incorrectAnswers = new string[2];
        [SerializeField] private string[] _correctAnswers = new string[2];

        public bool CheckAnswer(object chosenAnswer)
        {
            if (chosenAnswer is not string[] answers)
            {
                return false;
            }

            return answers.All(IsAnswerCorrect);
        }

        private bool IsAnswerCorrect(string answer)
        {
            foreach (var correctAnswer in _correctAnswers)
            {
                if (answer.Equals(correctAnswer))
                {
                    return true;
                }
            }

            return false;
        }
    }
}