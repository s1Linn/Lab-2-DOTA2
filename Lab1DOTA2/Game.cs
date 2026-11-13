using System;

namespace Lab2DOTA2
{
    // Абстрактний клас гри
    public abstract class BaseGame
    {
        public abstract int GetRating();
    }

    // Стандартна гра
    public class StandardGame : BaseGame
    {
        private readonly int _rating;
        public StandardGame(int rating) { _rating = rating; }
        public override int GetRating() => _rating;
    }

    // Тренувальна гра
    public class TrainingGame : BaseGame
    {
        public override int GetRating() => 0;
    }

    // Особлива гра (на рейтинг грає 1 гравець)
    public class SinglePlayerRatingGame : BaseGame
    {
        private readonly int _rating;
        public SinglePlayerRatingGame(int rating) { _rating = rating; }
        public override int GetRating() => _rating;
    }

    // Фабрика ігор
    public static class GameFactory
    {
        public static BaseGame CreateStandardGame(int rating) => new StandardGame(rating);
        public static BaseGame CreateTrainingGame() => new TrainingGame();
        public static BaseGame CreateSinglePlayerGame(int rating) => new SinglePlayerRatingGame(rating);
    }
}