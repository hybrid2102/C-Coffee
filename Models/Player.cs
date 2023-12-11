namespace C_Coffee.Models;

public record class Player
{
    public string? Name { get; set; }

    public override string ToString() => Name ?? string.Empty;
}

