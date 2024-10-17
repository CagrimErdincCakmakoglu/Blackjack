using NeoBJv2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBJv2.Entities
{
    public class GameManager
    {
        private Deck deck;
        private List<IPlayer> players;
        private Dealer dealer;
        int betAmount = 0; //Diğer metotlarda ulaşabilmek için globalde tanımlandı.
        int winnings = 0;

        public GameManager(int oyuncuSayisi)
        {
            deck = new Deck();
            players = new List<IPlayer>();
            dealer = new Dealer();

            // Oyuncuları oluştur
            for (int i = 1; i <= oyuncuSayisi; i++)
            {
                Console.Write($"{i}. oyuncunun ismini giriniz: ");
                string name = Console.ReadLine();
                players.Add(new Player(name));
            }

            Console.Clear();
            StartGame();
        }

        public void StartGame()
        {
            bool gameInProgress = true;
            

            while (gameInProgress)
            {
                // Kartları sıfırlıyoruz
                ResetPlayersHands();

                // Bahis ve kart işlemleri
                foreach (var player in players)
                {
                    if (player is Player p && p.IsInGame)
                    {
                        Console.WriteLine($"{p.Name}, bahis miktarınızı giriniz (Bakiye: {p.Balance} TL):");
                        betAmount = int.Parse(Console.ReadLine());
                        p.PlaceBet(betAmount);
                    }
                }

                // Her oyuncuya ve kurpiyere 2 kart dağıtılır
                foreach (var player in players)
                {
                    if (player is Player p && p.IsInGame)
                    {
                        p.AddCard(deck.DrawCard());
                        p.AddCard(deck.DrawCard());
                    }
                }
                dealer.AddCard(deck.DrawCard()); // İlk kart açık
                dealer.AddCard(deck.DrawCard()); // İkinci kart gizli

                // Oyuncuların ellerini göster
                foreach (var player in players)
                {
                    if (player is Player p && p.IsInGame)
                    {
                        p.ShowHand();
                    }
                }

                // Kurpiyerin sadece ilk kartı gösterilecek
                dealer.ShowInitialHand();

                // Oyunculara ekstra kart alıp almayacaklarını sor
                foreach (var player in players)
                {
                    if (player is Player p && p.IsInGame)
                    {
                        while (true)
                        {
                            Console.WriteLine($"{p.Name}, bir kart daha almak ister misiniz? (e/h)");
                            string choice = Console.ReadLine();
                            if (choice.ToLower() == "e")
                            {
                                Console.Clear();//1. EKLENEN
                                p.AddCard(deck.DrawCard());
                                //p.ShowHand();

                                // Tüm oyuncuların ellerini yeniden gösteriyoruz
                                foreach (var pl in players)
                                {
                                    if (pl is Player playerToShow && playerToShow.IsInGame)
                                    {
                                        playerToShow.ShowHand();
                                    }
                                }
                                // Kurpiyerin ilk kartını tekrar gösteriyoruz
                                dealer.ShowInitialHand();

                                if (p.CalculateScore() > 21)
                                {
                                    Console.WriteLine($"{p.Name} oyunu kaybetti! Skor 21'i aştı.\n");
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                // Kurpiyerin ikinci kartı açılır
                Console.WriteLine("Kurpiyer kartlarını açıyor...");
                dealer.ShowHand();

                // Kurpiyer kart almalı mı?
                while (dealer.CalculateScore() < 17)
                {
                    Console.WriteLine("Kurpiyer bir kart çekiyor...");
                    dealer.AddCard(deck.DrawCard());
                }

                dealer.ShowHand();

                // Sonuçları karşılaştır
                ShowResults();

                // Oyuncuların bakiyesini kontrol et ve devam etmek isteyip istemediklerini sor
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    if (players[i] is Player p && p.IsInGame)
                    {
                        p.CheckBalance();
                        if (!p.IsInGame || !p.ContinueGame())
                        {
                            players.RemoveAt(i); // Oyundan çıkan oyuncuyu listeden çıkarıyoruz
                        }
                    }
                }

                // Eğer tüm oyuncular oyunu terk ettiyse, oyun sona erer
                if (players.Count == 0)
                {
                    Console.WriteLine("Tüm oyuncular oyundan çıktı. Oyun sona erdi.");
                    gameInProgress = false;
                }
            }
        }


        //Oyuncuların ellerini sıfırlamak için
        private void ResetPlayersHands()
        {
            foreach (var player in players)
            {
                if (player is Player p)
                {
                    p.ResetHand(); // Oyuncunun elini sıfırlamak için bir metot çağrıyoruz
                }
            }
            dealer.ResetHand(); // Kurpiyerin elini de sıfırlıyoruz
        }

        private void ShowResults()
        {
            int dealerScore = dealer.CalculateScore();
            Console.WriteLine($"Kurpiyer'in skoru: {dealerScore}\n");

            foreach (var player in players)
            {
                if (player is Player p && p.IsInGame)
                {
                    int playerScore = p.CalculateScore();
                    Console.WriteLine($"{p.Name} skoru: {playerScore}");

                    if (playerScore > 21)
                    {
                        Console.WriteLine($"{p.Name} kaybetti.");
                    }
                    else if (dealerScore > 21 || playerScore > dealerScore)
                    {
                        int winnings = betAmount * 2; // Kazanan bahis miktarını ikiye katlar
                        p.UpdateBalance(winnings);
                        Console.WriteLine($"{p.Name} kazandı!");
                    }
                    else if (playerScore == dealerScore)
                    {
                        Console.WriteLine($"{p.Name} berabere.");
                        int winnings = betAmount; // Bahis miktarı iade edilir
                    }
                    else
                    {
                        Console.WriteLine($"{p.Name} kaybetti.");
                    }
                }
            }
        }
    }
}
