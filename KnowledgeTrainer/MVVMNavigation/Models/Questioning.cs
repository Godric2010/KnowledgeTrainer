using Core.Cards;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeTrainer.MVVMNavigation.Models
{
    public class Questioning
    {
        private readonly Card[] m_questionCards;

        public int CurrentCardIndex { get; set; }

        public int CardStackSize => m_questionCards.Length;


        public Questioning(IEnumerable<Card> cards)
        {
            m_questionCards = cards.ToArray();
        }


        public Card GetCardFromIndex(int index)
        {
            if (m_questionCards == null || index < 0 || index >= m_questionCards.Length)
                return GetErrorCard();

            return m_questionCards[index];
        }

        private Card GetErrorCard()
        {
            return new Card
            {
                Category = "Error",
                Answer = "No answer here",
                Question = "Diese Fehlerkarte sollte niemals erscheinen. Scheinbar fehlt die Datenbank oder aus einem unbekannten Grund, wurde ein falscher Index angegeben."
            };
        }
    }
}
