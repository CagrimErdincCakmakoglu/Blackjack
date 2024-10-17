using NeoBJv2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Interfaces
{
    public interface IPlayer
    {
        string Name { get; }
        int Balance { get; }
        void AddCard(Card card);
        int CalculateScore();
        void ShowHand();
        void PlaceBet(int amount);
        void UpdateBalance(int amount);
    }
}
