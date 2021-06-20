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

        private bool m_showAnswerButtonVisible;

        private string m_userAnswer;


        public ICommand GoToMainMenu => m_goToMainMenu ??= new RelayCommand(x => { Mediator.Notify("GoToMainMenu", ""); });

        public ICommand ShowAnswerText => new RelayCommand(x => { ToggleShowAnswerAndOptions.Invoke(); });

        public ICommand AnswerWasCorrect => new RelayCommand(x => { TellAnswerWasCorrect.Invoke(); UpdateVM(); });

        public ICommand AnswerWasFalse => new RelayCommand(x => { TellAnswerWasFalse.Invoke(); UpdateVM(); });


        public int QuestionsToAsk { get; set; }

        public int QuestionsAsked { get; set; }

        public string QuestionsToAskText => "Frage " + (QuestionsAsked + 1) + " von " + QuestionsToAsk;

        public double QuestionProgress { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string UserAnswer { get { return m_userAnswer; } set { m_userAnswer = value; OnPropertyChanged("UserAnswer"); } }

        public bool AnswerButtonIsVisible { get { return m_showAnswerButtonVisible; } set { m_showAnswerButtonVisible = value; OnPropertyChanged("AnswerButtonIsVisible"); } }
        
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

        private void UpdateVM()
        {
            OnPropertyChanged("QuestionsToAsk");
            OnPropertyChanged("QuestionsAsked");
            OnPropertyChanged("QuestionsToAskText");
            OnPropertyChanged("QuestionProgress");
            OnPropertyChanged("Question");
            OnPropertyChanged("Answer");
            OnPropertyChanged("UserAnswer");
        }
    }
}
