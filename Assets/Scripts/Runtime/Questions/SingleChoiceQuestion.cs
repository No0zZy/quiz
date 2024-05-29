using System;
using UnityEngine;

namespace HGtest.Questions
{
    [Serializable]
    public class SingleChoiceQuestion : IQuestion
    {
        public string QuestionText => _questionText;
        public string[] IncorrectAnswers => _incorrectAnswers;
        public string CorrectAnswer => _correctAnswer;

        [SerializeField] private string _questionText;
        [SerializeField] private string[] _incorrectAnswers = new string[3];
        [SerializeField] private string _correctAnswer;

        public bool CheckAnswer(object chosenAnswer)
        {
            if (chosenAnswer is string answer)
            {
                return answer.Equals(_correctAnswer);
            }

            return false;
        }
    }
}