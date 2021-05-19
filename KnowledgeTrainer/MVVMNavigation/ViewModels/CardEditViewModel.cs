using System;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    class CardEditViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand m_goToMainMenu;

        private Action m_checkNessecaryFieldsAreSet;

        private Action m_confirmAction;

        private string m_categoryName;

        private string m_questionText;

        private string m_answerText;

        private bool m_allFieldsAreValid;

        public ICommand GoToMainMenu => m_goToMainMenu ??= new RelayCommand(x => { Mediator.Notify("GoToCardSelection", ""); });

        public ICommand ConfirmAction => new RelayCommand(x => { m_confirmAction.Invoke(); });

        public string CardID { get; set; }

        public int Level { get; set; }

        public string Category
        {
            get => m_categoryName; set
            {
                m_categoryName = value;
                OnPropertyChanged("Category");
                m_checkNessecaryFieldsAreSet.Invoke();
            }
        }

        public string QuestionText
        {
            get => m_questionText;
            set
            {
                m_questionText = value;
                OnPropertyChanged("QuestionText");
                m_checkNessecaryFieldsAreSet.Invoke();
            }
        }

        public string AnswerText
        {
            get => m_answerText;
            set
            {
                m_answerText = value;
                OnPropertyChanged("AnswerText");
                m_checkNessecaryFieldsAreSet.Invoke();
            }
        }

        public bool NecessaryFieldsAreSet
        {
            get => m_allFieldsAreValid; 
            set
            {
                m_allFieldsAreValid = value;
                OnPropertyChanged("NecessaryFieldsAreSet");
            }
        }

        public void RegisterForNecessaryFieldsCheck(Action action)
        {
            m_checkNessecaryFieldsAreSet = action;
        }

        public void RegisterConfirmAction(Action action)
        {
            m_confirmAction = action;
        }
    }
}
