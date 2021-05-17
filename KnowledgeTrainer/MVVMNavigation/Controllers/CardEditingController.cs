using Core.Cards;
using KnowledgeTrainer.MVVMNavigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeTrainer.MVVMNavigation.Controllers
{
    class CardEditingController
    {
        private CardEditViewModel m_viewModel;

        private Card m_activeCard;

        private uint? m_selectedCardIndex =  null;

        public CardEditingController(CardEditViewModel vm)
        {
            m_viewModel = vm;
            m_viewModel.RegisterForNecessaryFieldsCheck(CheckNecessaryFields);
            m_viewModel.RegisterForCreateNewCard(ShowCardData);
            m_viewModel.RegisterForEditCard(ShowCardData);
            m_viewModel.RegisterForCancelAction(ClearFields);
            m_viewModel.RegsisterForConfirmAction(SaveCardChanges);
            m_viewModel.RegisterForDeleteCard(DeleteCard);
            //m_viewModel.RegisterForNewCategory()

        }

        private void ShowCardData()
        {
            m_viewModel.EditingFieldsVisible = true;

            if (m_selectedCardIndex == null)
            {
                CreateNewCard();
                return;
            }


            //m_activeCard = App.CardController.GetCardByID(cardID);
            if (m_activeCard == null)
                return;

            m_viewModel.CardID = m_activeCard.ID.ToString();
            m_viewModel.Level = m_activeCard.Level;
            m_viewModel.Category = m_activeCard.Category;
            m_viewModel.QuestionText = m_activeCard.Question;
            m_viewModel.AnswerText = m_activeCard.Answer;
            m_viewModel.NecessaryFieldsAreSet = true;

        }

        public void DeleteCard()
        {


            //App.CardController.DeleteCard(cardID);
            ClearFields();
        }

        public void SaveCardChanges()
        {
            if (m_activeCard == null)
            {
                App.CardController.CreateNewCard(m_viewModel.Category, m_viewModel.QuestionText, m_viewModel.AnswerText);
                return;
            }

            m_activeCard.Category = m_viewModel.Category;
            m_activeCard.Question = m_viewModel.QuestionText;
            m_activeCard.Answer = m_viewModel.AnswerText;

            App.CardController.UpdateCard(m_activeCard);
        }

        public void ClearFields()
        {
            m_viewModel.CardID = "";
            m_viewModel.Level = 0;
            m_viewModel.Category = "";
            m_viewModel.QuestionText = "";
            m_viewModel.AnswerText = "";
            m_viewModel.NecessaryFieldsAreSet = false;
            m_viewModel.EditingFieldsVisible = false;
        }

        private void CreateNewCard()
        {
            m_viewModel.CardID = "Neue CardID wird beim Speichern angelegt.";
            m_viewModel.Level = 0;
            m_viewModel.Category = "";
            m_viewModel.QuestionText = "";
            m_viewModel.AnswerText = "";
            m_viewModel.NecessaryFieldsAreSet = false;
        }

        private void CheckNecessaryFields()
        {
            m_viewModel.NecessaryFieldsAreSet = !string.IsNullOrEmpty(m_viewModel.Category) &&
                !string.IsNullOrEmpty(m_viewModel.QuestionText) &&
                !string.IsNullOrEmpty(m_viewModel.AnswerText);
        }
    }
}
