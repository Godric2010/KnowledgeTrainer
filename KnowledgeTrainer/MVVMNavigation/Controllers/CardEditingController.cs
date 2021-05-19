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

        public CardEditingController(CardEditViewModel vm)
        {
            m_viewModel = vm;
            m_viewModel.RegisterForNecessaryFieldsCheck(CheckNecessaryFields);
            m_viewModel.RegisterConfirmAction(SaveCardChanges);
        }

        public void ShowCardData(Card cardToShow)
        {
            if (cardToShow == null)
            {
                CreateNewCard();
                return;
            }

            m_activeCard = cardToShow;

            m_viewModel.CardID = m_activeCard.ID.ToString();
            m_viewModel.Level = m_activeCard.Level;
            m_viewModel.Category = m_activeCard.Category;
            m_viewModel.QuestionText = m_activeCard.Question;
            m_viewModel.AnswerText = m_activeCard.Answer;
            m_viewModel.NecessaryFieldsAreSet = false;

        }

        public void SaveCardChanges()
        {
            if (m_activeCard == null)
            {
                App.CardController.CreateNewCard(m_viewModel.Category, m_viewModel.QuestionText, m_viewModel.AnswerText);
                Mediator.Notify("GoToCardSelection", "");
                return;
            }

            m_activeCard.Category = m_viewModel.Category;
            m_activeCard.Question = m_viewModel.QuestionText;
            m_activeCard.Answer = m_viewModel.AnswerText;

            App.CardController.UpdateCard(m_activeCard);
            Mediator.Notify("GoToCardSelection", "");
        }


        private void CreateNewCard()
        {
            m_viewModel.CardID = "Neue CardID wird beim Speichern angelegt.";
            m_viewModel.Level = 0;
            m_viewModel.Category = "";
            m_viewModel.QuestionText = "";
            m_viewModel.AnswerText = "";
            m_viewModel.NecessaryFieldsAreSet = false;
            m_activeCard = null;
        }

        private void CheckNecessaryFields()
        {
            var enableConfirmationButton = !string.IsNullOrEmpty(m_viewModel.Category) &&
                !string.IsNullOrEmpty(m_viewModel.QuestionText) &&
                !string.IsNullOrEmpty(m_viewModel.AnswerText);

            m_viewModel.NecessaryFieldsAreSet = enableConfirmationButton;
        }
    }
}
