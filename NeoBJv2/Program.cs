using NeoBJv2.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackjackOyunu
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> oyuncular = new List<string>();

            Console.Write("Lütfen oyuncu sayısını giriniz:");
            int oyuncuSayisi = int.Parse(Console.ReadLine());

            //// Oyuncu isimlerini alma
            //for (int i = 1; i <= oyuncuSayisi; i++)
            //{
            //    Console.Write($"{i}. oyuncunun ismini giriniz: ");
            //    string isim = Console.ReadLine();
            //    oyuncular.Add(isim);
            //}

            // 3 saniye geri sayım
            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine($"{i} saniye sonra oyun başlıyor...");
                Thread.Sleep(1000); // 1 saniye bekleme
            }

            Console.Clear();

            // Oyun başlar
            Console.WriteLine("Oyun başladı!");

            GameManager gameManager = new GameManager(oyuncuSayisi);
        }
    }
}
