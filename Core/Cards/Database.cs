using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Cards
{
    internal class Database
    {
        private List<Card> m_cards;

        internal IEnumerable<Card> Cards => m_cards;

        public Database()
        {
            m_cards = new List<Card>();
        }


        /// <summary>
        /// Add a new card to the database and save it
        /// </summary>
        /// <param name="category">The category string of the card.</param>
        /// <param name="question">The question string of the card.</param>
        /// <param name="answer">The answer string of the card.</param>
        public void AddNewCard(string category, string question, string answer)
        {
            var card = new Card
            {
                ID = Guid.NewGuid(),
                Category = category,
                Question = question,
                Answer = answer,
                Level = 0,
                CreationDate = DateTime.Today,
                NextRepeat = DateTime.Today
            };
            m_cards.Add(card);
        }

        internal void AddCards(IEnumerable<Card> cards)
        {
            m_cards.AddRange(cards);
        }

        /// <summary>
        /// Delete a card with a certain ID and saves the database after it.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCard(Guid id)
        {
            try
            {
                var card = m_cards.Single(card => card.ID == id);
                m_cards.Remove(card);
            }
            catch (Exception e)
            {
                Console.WriteLine("Card not found... " + e);
            }
        }

        /// <summary>
        /// Edit a card in the database. Updated values get written and the database is saved afterwards.
        /// </summary>
        /// <param name="dirtyCard"></param>
        public void EditCard(Card dirtyCard)
        {
            if (!m_cards.Contains(dirtyCard))
            {
                AddNewCard(dirtyCard.Category, dirtyCard.Question, dirtyCard.Answer);
                return;
            }

            var oldCard = m_cards.Single(c => c.ID == dirtyCard.ID);
            var cardIndex = m_cards.IndexOf(oldCard);

            m_cards[cardIndex] = dirtyCard;
        }
    }
}
