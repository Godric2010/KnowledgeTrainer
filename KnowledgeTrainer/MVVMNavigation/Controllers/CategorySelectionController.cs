using KnowledgeTrainer.MVVMNavigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeTrainer.MVVMNavigation.Controllers
{
    public class CategorySelectionController
    {
        private CategoriesViewModel m_viewModel;

        public CategorySelectionController(CategoriesViewModel vm)
        {
            m_viewModel = vm;
            m_viewModel.SelectCardAction = SelectCard;
        }

        private void SelectCard(string category)
        {
            Mediator.Notify("GoToQuestioning", category);
        }

        public void UpdateDisplayedCards()
        {
            m_viewModel.Categories = new System.Collections.ObjectModel.ObservableCollection<CategoryPreviewItem>();
            var categories = App.CardController.GetAllCategories();
            foreach (string category in categories)
            {
                int cardAmount = App.CardController.GetCardsByCategory(category).Count();
                m_viewModel.Categories.Add(new CategoryPreviewItem { Category = category, AmountOfCards = cardAmount });
            }
        }
    }
}
