using HGtest.Questions;

namespace HGtest.Screens
{
    public class QuestionScreenView : ScreenView
    {
        public void Build(IQuestion question)
        {
            BuildView(question);
        }

        protected virtual void BuildView(IQuestion question){}
        
    }
}