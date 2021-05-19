using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    public class CategoriesViewModel : BaseViewModel, IPageViewModel
    {
        private CategoryPreviewItem m_currentlySelectedItem;

        public CategoryPreviewItem SelectedCategory
        {
            get => m_currentlySelectedItem;
            set
            {
                m_currentlySelectedItem = value;
                SelectCardAction.Invoke(m_currentlySelectedItem.Category);
            }
        }

        public ObservableCollection<CategoryPreviewItem> Categories { get; set; }

        public Action<string> SelectCardAction { get; set; }

        public ICommand GoToMainMenu => new RelayCommand(x => { Mediator.Notify("GoToMainMenu", ""); });
    }

    public class CategoryPreviewItem
    {
        private const string eposition = " Karten in dieser Kategorie.";

        public string Category { get; set; }

        public int AmountOfCards { get; set; }

        public string CardsInCategory => AmountOfCards.ToString() + eposition;
    }
}
