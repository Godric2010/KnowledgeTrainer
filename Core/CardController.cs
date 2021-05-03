using Core.Cards;
using Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class CardController
    {
        private readonly Database db;

        public CardController()
        {
            db = new Database();
        }

        public void Init()
        {
            var cards = JsonSerializer.LoadCardsFromDisk("");
            if (cards == null) return;

            db.AddCards(cards);
        }


        public void CreateNewCard(string category, string question, string answer)
        {
            db.AddNewCard(category, question, answer);
            JsonSerializer.SaveCardsToDisk(db.Cards, "");
        }

        public void DeleteCard(Guid cardID)
        {
            db.DeleteCard(cardID);
            JsonSerializer.SaveCardsToDisk(db.Cards, "");
        }

        public Card GetCardByID(Guid cardID)
        {
            var card = db.Cards.SingleOrDefault(c => c.ID == cardID);
            return card;
        }

        public void UpdateCard(Card dirtyCard)
        {
            db.EditCard(dirtyCard);
            JsonSerializer.SaveCardsToDisk(db.Cards, "");
        }

        public IEnumerable<Card> GetCardsByCategory(string category)
        {
            return db.Cards.Where(c => c.Category == category);
        }

        public IEnumerable<string> GetAllCategories()
        {
            var categories = new List<string>();
            foreach (var card in db.Cards)
            {
                if (categories.Contains(card.Category)) continue;
                categories.Add(card.Category);
            }

            return categories;
        }

        public IEnumerable<Card> GetCardsToRepeat()
        {
            var cardsToRepeat = new List<Card>();
            var today = DateTime.Today;
            foreach (var card in db.Cards)
            {
                int result = card.LastRepeat.Date.CompareTo(today.Date);
                if (result > 0) continue;

                cardsToRepeat.Add(card);
            }

            return cardsToRepeat;
        }

    }
}
