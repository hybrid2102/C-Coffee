namespace C_Coffee.Models
{
    public record Event()
    {
        public Player? Player { get; init; }

        public int? Bet { get; init; }

        public string? Message { get; init; }
    }
}
