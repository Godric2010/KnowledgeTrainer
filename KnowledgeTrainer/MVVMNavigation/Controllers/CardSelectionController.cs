using KnowledgeTrainer.MVVMNavigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

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
            m_viewModel.PreviewItems = new System.Collections.ObjectModel.ObservableCollection<CardPreviewItem>();
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "TestCategory", Question = "Test Question with a very long text which probably does not fit inside this thing here. Blablablablabla blablabla" });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 03", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 04", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 05", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
            m_viewModel.PreviewItems.Add(new CardPreviewItem { CardId = Guid.Empty, Category = "Test 02", Question = "A dumbass question with no smart answer behind." });
        }

        private void CreateNewCard()
        {
            Mediator.Notify("GoToCardEditing", "");
        }

        private void SelectCard(Guid cardID)
        {
            var card = App.CardController.GetCardByID(cardID);
            Mediator.Notify("GoToCardEditing", card);
        }
    }
}
