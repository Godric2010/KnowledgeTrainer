using Core.Cards;
using KnowledgeTrainer.MVVMNavigation.ViewModels;
using System;

namespace KnowledgeTrainer.MVVMNavigation.Controllers
{
    public class CardSelectionController
    {
        private CardSelectionViewModel m_viewModel;

        public CardSelectionController(CardSelectionViewModel vm)
        {
            m_viewModel = vm;
            m_viewModel.CreateNewCardAction = CreateNewCard;
            m_viewModel.SelectCardAction = SelectCard;
        }

        private void CreateNewCard()
        {
            Mediator.Notify("GoToCardEditing", null);
        }

        private void SelectCard(Guid cardID)
        {
            var card = App.CardController.GetCardByID(cardID);
            Mediator.Notify("GoToCardEditing", card);
        }

        public void UpdateDisplayedCards()
        {
            m_viewModel.PreviewItems = new System.Collections.ObjectModel.ObservableCollection<CardPreviewItem>();
            var cards = App.CardController.GetAllCards();
            foreach (Card card in cards)
            {
                m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = card.ID, Category = card.Category, Question = card.Question });
            }
        }
    }
}
