using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Entities
{
    public class Card
    {
        public string Suit { get; private set; } // Maça, Karo, Sinek, Kupa gibi
        public string Rank { get; private set; } // 2, 3, 4, As, K gibi
        public int Value { get; private set; }   // Kartın Blackjack'teki değeri

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Suit} {Rank}";
        }
    }
}
