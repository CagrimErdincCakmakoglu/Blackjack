using NeoBJv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Entities
{
    public class Player:IPlayer
    {
        public string Name { get; private set; }
        public int Balance { get; private set; }
        private int betAmount;
        private List<Card> hand;
        public bool IsInGame { get; private set; }


        public Player(string name)
        {
            Name = name;
            Balance = 1000; // Başlangıç bakiyesi
            hand = new List<Card>();
            IsInGame = true; // Oyuncu başlangıçta oyunda
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

            // Eğer As varsa ve toplamı 21'i aşıyorsa, As'ı 1 olarak say
            while (score > 21 && aceCount > 0)
            {
                score -= 10;
                aceCount--;
            }

            return score;
        }

        public void ShowHand()
        {
            Console.WriteLine($"{Name} oyuncusunun elindeki kartlar:");
            foreach (var card in hand)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine($"Skor: {CalculateScore()}\n");
        }

        public void PlaceBet(int amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException($"{Name} yeterli bakiyeye sahip değil.");
            }
            betAmount = amount;
            Balance -= amount;
            Console.WriteLine($"{Name} {amount} TL bahis yaptı. Kalan bakiye: {Balance} TL.");
        }

        public void UpdateBalance(int amount)
        {
            Balance += amount;
            Console.WriteLine($"{Name} kazandı ve bakiyesine {amount} TL eklendi. Güncel bakiye: {Balance} TL.");
        }

        public void CheckBalance()
        {
            if (Balance <= 0)
            {
                Console.WriteLine($"{Name}, tüm bakiyenizi kaybettiniz. Teşekkür ederiz, oyun dışında kaldınız.");
                IsInGame = false; // Oyuncuyu oyundan çıkarıyoruz
            }
        }

        public bool ContinueGame()
        {
            if (!IsInGame) return false;

            Console.WriteLine($"{Name}, oyuna devam etmek ister misiniz? (e/h)");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "h")
            {
                Console.WriteLine($"{Name}, oyun dışında kaldınız. Teşekkür ederiz.");
                IsInGame = false;
                return false;
            }
            return true;
        }

        public void ResetHand()
        {
            hand.Clear(); // Oyuncunun elindeki kartları temizler
        }
    }
}
