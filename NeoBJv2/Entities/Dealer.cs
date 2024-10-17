using NeoBJv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Entities
{
    public class Dealer:IPlayer
    {
        public string Name { get; private set; }
        public int Balance { get; private set; } = 0; // Kurpiyer bahis yapmaz
        private List<Card> hand;
        private bool showAllCards = false;

        public Dealer()
        {
            Name = "Kurpiyer";
            hand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            hand.Add(card);
        }

        public int CalculateScore()
        {
            int score = 0;
            int aceCount = 0;
            foreach (var card in hand)
            {
                score += card.Value;
                if (card.Rank == "As")
                {
                    aceCount++;
                }
            }

            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        public void ShowInitialHand()
        {
            Console.WriteLine("Kurpiyer'in görünen kartı:");
            Console.WriteLine(hand[0].ToString()); // Sadece ilk kartı göster
            Console.WriteLine("Kurpiyer'in diğer kartı gizli.\n");
        }

        public void ShowHand()
        {
            Console.WriteLine($"Kurpiyer'in elindeki kartlar:");
            foreach (var card in hand)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine($"Kurpiyer'in skoru: {CalculateScore()}\n");
        }
        public void ResetHand()
        {
            hand.Clear(); // Kurpiyerin elindeki kartları temizler
        }

        public void PlaceBet(int amount) { } // Kurpiyer bahis yapmaz

        public void UpdateBalance(int amount) { } // Kurpiyerin bakiyesi yok
    }
}
