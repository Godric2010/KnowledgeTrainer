using KnowledgeTrainer.MVVMNavigation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private QuestioningController m_questioningController;
        private CardSelectionController m_cardSelectionController;
        private CardEditingController m_cardEditingController;


        private IPageViewModel m_currentPageViewModel;
        private List<IPageViewModel> m_pageViewModels;

        public List<IPageViewModel> PageViewModels => m_pageViewModels;

        public IPageViewModel CurrentPageViewModel
        {
            get => m_currentPageViewModel;
            set
            {
                m_currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        private void GoToMainMenu(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void GoToQuestioning(object obj)
        {
            ChangeViewModel(PageViewModels[1]);

            if (obj == null)
                m_questioningController.Init(null);
            else
                m_questioningController.Init((string)obj);
        }

        private void GoToCategories(object obj)
        {
            ChangeViewModel(PageViewModels[2]);
        }

        private void GoToCardSelectionMenu(object obj)
        {
            ChangeViewModel(PageViewModels[3]);
        }

        private void GoToSettings(object obj)
        {
            ChangeViewModel(PageViewModels[4]);
        }

        private void GoToEditCardsMenu(object obj)
        {
            ChangeViewModel(PageViewModels[5]);
        }

        public MainWindowViewModel()
        {
            var questioningVM = new QuestioningViewModel();
            var cardSelectionVM = new CardSelectionViewModel();
            var editingVM = new CardEditViewModel();

            m_pageViewModels = new List<IPageViewModel>();

            PageViewModels.Add(new MainMenuViewModel());
            PageViewModels.Add(questioningVM);
            PageViewModels.Add(new CategoriesViewModel());
            PageViewModels.Add(cardSelectionVM);
            PageViewModels.Add(new SettingsViewModel());
            PageViewModels.Add(editingVM);

            CurrentPageViewModel = PageViewModels[0];

            m_questioningController = new QuestioningController(questioningVM);
            m_cardSelectionController = new CardSelectionController(cardSelectionVM);
            m_cardEditingController = new CardEditingController(editingVM);

            Mediator.Subscribe("GoToMainMenu", GoToMainMenu);
            Mediator.Subscribe("GoToCardSelection", GoToCardSelectionMenu);
            Mediator.Subscribe("GoToCardEditing", GoToEditCardsMenu);
            Mediator.Subscribe("GoToCategories", GoToCategories);
            Mediator.Subscribe("GoToQuestioning", GoToQuestioning);
            Mediator.Subscribe("GoToSettings", GoToSettings);
        }
    }
}
