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

        private bool m_supressLevelIncrease;

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
            {
                cardsToUse = App.CardController.GetCardsToRepeat();
                m_supressLevelIncrease = false;
            }
            else
            {
                cardsToUse = App.CardController.GetCardsByCategory(category);
                m_supressLevelIncrease = true;
            }

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

            if (m_data.CurrentCardIndex == m_data.CardStackSize)
            {
                Mediator.Notify("GoToMainMenu", "");
                return;
            }

            // Get the carc from the current index of the stack and ... 
            var cardToShow = m_data.GetCardFromIndex(m_data.CurrentCardIndex);

            // ... prepare the view model
            m_viewModel.Question = cardToShow.Question;
            m_viewModel.Answer = cardToShow.Answer;
            m_viewModel.UserAnswer = " ";
            m_viewModel.QuestionsAsked = m_data.CurrentCardIndex;
            m_viewModel.QuestionProgress = m_data.CardStackSize == 0 ? 0 : (double)m_data.CurrentCardIndex / m_data.CardStackSize;
            m_viewModel.ShowAnswerAndOptions = false;
            m_viewModel.AnswerButtonIsVisible = true;
        }

        private void ShowAnswer()
        {
            m_viewModel.ShowAnswerAndOptions = true;
            m_viewModel.AnswerButtonIsVisible = false;
        }

        private void SetCardToNextLevel()
        {
            Core.Cards.Card card = m_data.GetCardFromIndex(m_data.CurrentCardIndex);

            if (!m_supressLevelIncrease)
            {
                card.Level++;
                card.LastRepeat = DateTime.Today;
                card.NextRepeat = GetNextDateToQuestionThisCard(card.Level);
            }

            App.CardController.UpdateCard(card);
            GetNewCardData();

        }

        private void SetCardToFirstLevel()
        {
            Core.Cards.Card card = m_data.GetCardFromIndex(m_data.CurrentCardIndex);
            if (!m_supressLevelIncrease)
            {
                card.Level = 0;
                card.LastRepeat = DateTime.Today;
                card.NextRepeat = GetNextDateToQuestionThisCard(card.Level);
            }
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
