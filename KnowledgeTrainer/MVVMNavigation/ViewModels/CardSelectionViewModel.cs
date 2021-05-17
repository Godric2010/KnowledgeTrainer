using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    public class CardSelectionViewModel : BaseViewModel, IPageViewModel
    {
        private CardPreviewItem m_currentlySelectedItem;

        public CardPreviewItem SelectedItem
        {
            get => m_currentlySelectedItem;
            set
            {
                m_currentlySelectedItem = value;
                SelectCardAction.Invoke(m_currentlySelectedItem.CardId);
            }
        }

        public ObservableCollection<CardPreviewItem> PreviewItems { get; set; }

        public Action CreateNewCardAction { get; set; }

        public Action<Guid> SelectCardAction { get; set; }

        public ICommand GoToMainMenu => new RelayCommand(x => { Mediator.Notify("GoToMainMenu", ""); });

        public ICommand CreateNewCard => new RelayCommand(x => { CreateNewCardAction.Invoke(); });
    }

    public class CardPreviewItem
    {
        public Guid CardId { get; set; }

        public string Category { get; set; }

        public string Question { get; set; }
    }


}
