using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    class CardEditViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand m_goToMainMenu;

        private Action m_checkNessecaryFieldsAreSet;

        private Action m_createNewCard;

        private Action m_editCard;

        private Action m_deleteCard;

        private Action m_newCategory;

        private Action m_cancelAction;

        private Action m_confirmAction;

        private string m_questionText;

        private string m_answerText;

        private bool m_editingFieldsVisible;

        private bool m_editingFieldsSelectable;


        public ICommand GoToMainMenu => m_goToMainMenu ??= new RelayCommand(x => { Mediator.Notify("GoToMainMenu", ""); });

        public ICommand CreateNewCard => new RelayCommand(x => { m_createNewCard.Invoke(); });

        public ICommand EditCard => new RelayCommand(x => { m_editCard.Invoke(); });

        public ICommand DeleteCard => new RelayCommand(x => { m_deleteCard.Invoke(); });

        public ICommand NewCategory => new RelayCommand(x => { m_newCategory.Invoke(); });

        public ICommand CancelAction => new RelayCommand(x => { m_cancelAction.Invoke(); });

        public ICommand ConfirmAction => new RelayCommand(x => { m_confirmAction.Invoke(); });


        public string CardID { get; set; }

        public int Level { get; set; }

        public string Categorie { get; set; }

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

        public List<string> Cards { get; set; }

        public bool NecessaryFieldsAreSet { get; set; }

        public bool EditingFieldsVisible
        {
            get => m_editingFieldsVisible;
            set
            {
                m_editingFieldsVisible = value;
                OnPropertyChanged("EditingFieldsVisible");
            }
        }

        public bool EditingPossible { get => m_editingFieldsSelectable; set { m_editingFieldsSelectable = value; OnPropertyChanged("EditingPossible"); } }

        public void RegisterForNecessaryFieldsCheck(Action action)
        {
            m_checkNessecaryFieldsAreSet = action;
        }

        public void RegisterForCreateNewCard(Action action)
        {
            m_createNewCard = action;
        }

        public void RegisterForEditCard(Action action)
        {
            m_editCard = action;
        }

        public void RegisterForDeleteCard(Action action)
        {
            m_deleteCard = action;
        }

        public void RegisterForNewCategory(Action action)
        {
            m_newCategory = action;
        }

        public void RegisterForCancelAction(Action action)
        {
            m_cancelAction = action;
        }

        public void RegsisterForConfirmAction(Action action)
        {
            m_confirmAction = action;
        }
    }
}
