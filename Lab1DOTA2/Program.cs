using System;

namespace Lab2DOTA2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА 2 (DOTA2) ===\n");

            // Створення гравців (MMR = 1000)
            GameAccount player1 = new StandardAccount("Arteezy", 1000);
            GameAccount player2 = new HalfLossAccount("Dendi", 1000);
            GameAccount player3 = new StreakBonusAccount("stariy_bog", 1000);

            Console.WriteLine("Початкова статистика:");
            player1.GetStats();
            player2.GetStats();
            player3.GetStats();
            Console.WriteLine(new string('-', 60));

            // Створення ігор через фабрику (рейтингова гра дає 25 MMR)
            BaseGame rankedGame = GameFactory.CreateStandardGame(25);
            BaseGame trainingGame = GameFactory.CreateTrainingGame();

            // Матч 1
            Console.WriteLine("\n[Матч 1] Рейтингова гра (stariy_bog виграє у Arteezy):");
            player3.WinGame(player1, rankedGame); // stariy_bog отримує +25
            player1.LoseGame(player3, rankedGame); // Arteezy втрачає -40
            player3.GetStats();
            player1.GetStats();

            // Матч 2
            Console.WriteLine("\n[Матч 2] Рейтингова гра (stariy_bog виграє у Dendi, СТРІК!):");
            player3.WinGame(player2, rankedGame); // stariy_bog отримує +25 і бонус +25 (разом 50)
            player2.LoseGame(player3, rankedGame); // Dendi втрачає -20 (половина від 40)
            player3.GetStats();
            player2.GetStats();

            // Матч 3
            Console.WriteLine("\n[Матч 3] Тренувальна гра (Arteezy виграє у stariy_bog):");
            player1.WinGame(player3, trainingGame); // 0 MMR
            player3.LoseGame(player1, trainingGame); // 0 MMR (але стрік скинуто)
            player1.GetStats();
            player3.GetStats();

            Console.ReadKey();
        }
    }
}