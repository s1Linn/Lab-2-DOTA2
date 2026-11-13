using System;
using System.Collections.Generic;

namespace Lab2DOTA2
{
    public abstract class GameAccount
    {
        private const int MinRating = 1;
        private string _userName;
        private int _currentRating;
        private readonly List<BaseGame> _gameHistory = new List<BaseGame>();

        public string UserName
        {
            get => _userName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я не може бути порожнім!");
                _userName = value;
            }
        }

        public int CurrentRating
        {
            get => _currentRating;
            protected set => _currentRating = value < MinRating ? MinRating : value;
        }

        public int GamesCount => _gameHistory.Count;

        protected GameAccount(string userName, int initialRating)
        {
            UserName = userName;
            CurrentRating = initialRating;
        }

        public virtual int CalculatePointsOnWin(BaseGame game)
        {
            return game.GetRating();
        }

        public abstract int CalculatePointsOnLose(BaseGame game);

        public virtual void WinGame(GameAccount opponent, BaseGame game)
        {
            CurrentRating += CalculatePointsOnWin(game);
            _gameHistory.Add(game);
        }

        public virtual void LoseGame(GameAccount opponent, BaseGame game)
        {
            CurrentRating -= CalculatePointsOnLose(game);
            _gameHistory.Add(game);
        }

        public virtual void GetStats()
        {
            Console.WriteLine($"Гравець: {UserName} | Тип: {GetType().Name} | MMR: {CurrentRating} | Ігор: {GamesCount}");
        }
    }

    // 1. Стандартний аккаунт (втрачає 40 MMR)
    public class StandardAccount : GameAccount
    {
        public StandardAccount(string userName, int initialRating) : base(userName, initialRating) { }

        public override int CalculatePointsOnLose(BaseGame game)
        {
            return game.GetRating() == 0 ? 0 : 40; // Якщо не тренування, знімаємо 40
        }
    }

    // 2. Аккаунт із половинним штрафом (втрачає 20 MMR)
    public class HalfLossAccount : GameAccount
    {
        public HalfLossAccount(string userName, int initialRating) : base(userName, initialRating) { }

        public override int CalculatePointsOnLose(BaseGame game)
        {
            return (game.GetRating() == 0 ? 0 : 40) / 2;
        }
    }

    // 3. Аккаунт із бонусом за стрік (+25 за стрік)
    public class StreakBonusAccount : GameAccount
    {
        private int _consecutiveWins = 0;

        public StreakBonusAccount(string userName, int initialRating) : base(userName, initialRating) { }

        public override int CalculatePointsOnWin(BaseGame game)
        {
            _consecutiveWins++;
            int points = game.GetRating();

            // Якщо 2+ перемоги підряд і гра рейтингова, даємо +25 MMR зверху
            if (_consecutiveWins >= 2 && points > 0)
            {
                points += 25;
            }
            return points;
        }

        public override void LoseGame(GameAccount opponent, BaseGame game)
        {
            _consecutiveWins = 0; // Скидаємо стрік
            base.LoseGame(opponent, game);
        }

        public override int CalculatePointsOnLose(BaseGame game)
        {
            return game.GetRating() == 0 ? 0 : 40;
        }
    }
}