using C_Coffee.Extensions;
using Microsoft.Extensions.Logging;
using MudBlazor.Interfaces;

namespace C_Coffee.Models
{
    public class Game
    {
        private const int _min = 1;
        private const int _max = 1000;

        public readonly int Secret = new Random().Next(_min, _max + 1);

        public Limits InitialLimits = new(1, 1000);

        public Limits MatchLimits { get; private set; } = new Limits(_min, _max);

        public Status Status { get; private set; } = Status.New;

        public List<Player>? Players { get; private set; }
        public Player? CurrentPlayer { get; private set; }

        public Player? Loser { get; private set; }

        public readonly List<string> History = [];

        public void Start(List<Player>? players = null)
        {
            Players = players;
            CurrentPlayer = Players?.FirstOrDefault();
            Status = Status.Ongoing;
            Console.WriteLine($"Secret: {Secret}");
            History.Insert(0, "Il gioco ha inizio!");
        }

        public bool Check(int bet, Player? player)
        {
            if (bet <= MatchLimits.Min || bet >= MatchLimits.Max) throw new ArgumentOutOfRangeException(nameof(bet));

            History.Insert(0, player is null ? $"Hai scommesso {bet}" : $"{player} ha scommesso {bet}");

            var hasLost = bet == Secret;

            return hasLost;
        }


        public void End(Player? loser)
        {
            History.Insert(0, loser is null ? "Hai perso!" : $"{loser} ha perso!");

            Loser = loser;
            Status = Status.Ended;
        }

        private void NarrowDown(int bet)
        {
            MatchLimits = bet < Secret
                ? (MatchLimits with { Min = bet })
                : (MatchLimits with { Max = bet });
        }

        private void NextPlayer()
        {
            if (Players is not null)
                CurrentPlayer = Players.NextItem(CurrentPlayer);
        }

        public void Continue(int bet)
        {
            NarrowDown(bet);
            NextPlayer();
        }



    }
}
