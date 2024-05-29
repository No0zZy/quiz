using System;
using UnityEngine;

namespace HGtest.Questions
{
    [Serializable]
    public class TextInputQuestion : IQuestion
    {
        public string QuestionText => _questionText;

        [SerializeField] private string _questionText;
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