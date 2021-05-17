using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KnowledgeTrainer.MVVMNavigation.ViewModels
{
    class MainMenuViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand m_goToQuestioning;

        private ICommand m_goToCategories;

        private ICommand m_goToCardEditing;

        private ICommand m_goToSettings;

        public ICommand GoToQuestioning => m_goToQuestioning ??= new RelayCommand(x => { Mediator.Notify("GoToQuestioning", ""); });

        public ICommand GoToCategories => m_goToCategories ??= new RelayCommand(x => { Mediator.Notify("GoToCategories", ""); });

        public ICommand GoToCardEditing => m_goToCardEditing ??= new RelayCommand(x => { Mediator.Notify("GoToCardSelection", ""); });

        public ICommand GoToSettings => m_goToSettings ??= new RelayCommand(x => { Mediator.Notify("GoToSettings", ""); });
    }
}
