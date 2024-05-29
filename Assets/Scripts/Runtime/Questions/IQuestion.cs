namespace HGtest.Questions
{
    public interface IQuestion
    {
        public string QuestionText { get; }

        public bool CheckAnswer(object chosenAnswer);
    }
}