namespace BlazingPokemon.Models
{
    public record PokemonType
    {
        public int Slot { get; set; }
        public TypeDetails? Type { get; set; }
    }

    public record TypeDetails
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}