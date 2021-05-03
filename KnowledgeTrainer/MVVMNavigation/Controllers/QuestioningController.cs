using KnowledgeTrainer.MVVMNavigation.Models;
using KnowledgeTrainer.MVVMNavigation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeTrainer.MVVMNavigation.Controllers
{
    public class QuestioningController
    {
        private QuestioningViewModel m_viewModel;

        private Questioning m_data;

        public QuestioningController(QuestioningViewModel vm)
        {
            m_viewModel = vm;
            m_viewModel.SubscribeToShowAnswer(ShowAnswer);
            m_viewModel.SubscribeToAnswerCorrect(SetCardToNextLevel);
            m_viewModel.SubscribeToAnswerFalse(SetCardToFirstLevel);
        }

        public void Init(string category)
        {
            // Pull all questions to ask ...
            IEnumerable<Core.Cards.Card> cardsToUse;
            if (string.IsNullOrEmpty(category))
                cardsToUse = App.CardController.GetCardsToRepeat();
            else
                cardsToUse = App.CardController.GetCardsByCategory(category);

            m_data = new Questioning(cardsToUse);

            // ...and get the length to set.
            m_viewModel.QuestionsToAsk = m_data.CardStackSize;

            // Set the card index to a negative value and call new card to draw the first one.
            m_data.CurrentCardIndex = -1;
            GetNewCardData();
        }

        private void GetNewCardData()
        {
            m_data.CurrentCardIndex++;

            if(m_data.CurrentCardIndex == m_data.CardStackSize)
            {
                Mediator.Notify("GoToMainMenu", "");
                return;
            }

            // Get the carc from the current index of the stack and ... 
            var cardToShow = m_data.GetCardFromIndex(m_data.CurrentCardIndex);

            // ... prepare the view model
            m_viewModel.Question = cardToShow.Question;
            m_viewModel.Answer = cardToShow.Answer;
            m_viewModel.QuestionsAsked = m_data.CurrentCardIndex;
            m_viewModel.QuestionProgress = m_data.CardStackSize == 0 ? 0 : (double)m_data.CurrentCardIndex / m_data.CardStackSize;
            m_viewModel.ShowAnswerAndOptions = false;
        }

        private void ShowAnswer()
        {
            m_viewModel.ShowAnswerAndOptions = true;
        }

        private void SetCardToNextLevel()
        {
            var card = m_data.GetCardFromIndex(m_data.CurrentCardIndex);
            card.Level++;
            card.NextRepeat = GetNextDateToQuestionThisCard(card.Level);

            App.CardController.UpdateCard(card);
            GetNewCardData();

        }

        private void SetCardToFirstLevel()
        {
            var card = m_data.GetCardFromIndex(m_data.CurrentCardIndex);
            card.Level = 0;
            card.NextRepeat = GetNextDateToQuestionThisCard(card.Level);

            App.CardController.UpdateCard(card);
            GetNewCardData();
        }

        private DateTime GetNextDateToQuestionThisCard(int level)
        {
            DateTime today = DateTime.Today;

            return level switch
            {
                0 => today,
                1 => today.AddDays(1),
                2 => today.AddDays(3),
                3 => today.AddDays(7),
                4 => today.AddDays(14),
                5 => today.AddDays(30),
                6 => today.AddMonths(2),
                _ => today.AddMonths(3),
            };
        }
    }
}
