using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Entities
{
    public class Deck
    {
        private List<Card> cards;
        private Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "Maça", "Kupa", "Karo", "Sinek" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Vale", "Kız", "Papaz", "As" };
            int[] values = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };

            foreach (var suit in suits)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    cards.Add(new Card(suit, ranks[i], values[i]));
                }
            }

            Shuffle(); // Oyunun başında karıştırma
        }

        public void Shuffle()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndex = random.Next(cards.Count);
                var temp = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count > 0)
            {
                Card cardToDeal = cards[0];
                cards.RemoveAt(0);
                return cardToDeal;
            }
            throw new InvalidOperationException("Deste bitti!");
        }
    }
}
