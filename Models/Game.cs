﻿using C_Coffee.Extensions;
using MudBlazor.Interfaces;

namespace C_Coffee.Models
{
    public class Game
    {
        private const int _min = 1;
        private const int _max = 1000;

        private readonly int? _secret = new Random().Next(_min + 1, _max);

        public Limits Limits { get; private set; } = new Limits(_min, _max);

        public Status Status { get; private set; } = Status.New;

        public List<Player>? Players { get; private set; }
        public Player? CurrentPlayer { get; private set; }

        public Player? Loser { get; private set; }

        public void Start(List<Player>? players = null)
        {
            Players = players;
            CurrentPlayer = Players?.FirstOrDefault();
            Status = Status.Ongoing;
            Console.WriteLine($"Secret: {_secret}");
        }

        public bool Check(int bet)
        {
            if (bet < Limits.Min || bet > Limits.Max) throw new ArgumentOutOfRangeException();

            var hasLost = bet == _secret;

            return hasLost;
        }


        public void End(Player loser)
        {
            Loser = loser;
            Status = Status.Ended;
        }

        private void NarrowDown(int bet) => Limits = bet < _secret
            ? (Limits with { Min = bet })
            : (Limits with { Max = bet });

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