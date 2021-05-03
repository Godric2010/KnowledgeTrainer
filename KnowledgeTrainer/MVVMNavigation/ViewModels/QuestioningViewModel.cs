using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    public class QuestioningViewModel : BaseViewModel, IPageViewModel
    {
        private Action ToggleShowAnswerAndOptions;

        private Action TellAnswerWasCorrect;

        private Action TellAnswerWasFalse;


        private ICommand m_goToMainMenu;

        private bool m_answerIsVisible;


        public ICommand GoToMainMenu => m_goToMainMenu ??= new RelayCommand(x => { Mediator.Notify("GoToMainMenu", ""); });

        public ICommand ShowAnswerText => new RelayCommand(x => { ToggleShowAnswerAndOptions.Invoke(); });

        public ICommand AnswerWasCorrect => new RelayCommand(x => { TellAnswerWasCorrect.Invoke(); });

        public ICommand AnswerWasFalse => new RelayCommand(x => { TellAnswerWasFalse.Invoke(); });


        public int QuestionsToAsk { get; set; }

        public int QuestionsAsked { get; set; }

        public double QuestionProgress { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public bool ShowAnswerAndOptions { get { return m_answerIsVisible; } set { m_answerIsVisible = value; OnPropertyChanged("ShowAnswerAndOptions"); } }


        public QuestioningViewModel()
        {
            Question = "This is a default test question.";
            Answer = "This is a default test answer.";
        }

        public void SubscribeToShowAnswer(Action action)
        {
            ToggleShowAnswerAndOptions = action;
        }

        public void SubscribeToAnswerCorrect(Action action)
        {
            TellAnswerWasCorrect = action;
        }

        public void SubscribeToAnswerFalse(Action action)
        {
            TellAnswerWasFalse = action;
        }
    }
}
