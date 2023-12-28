namespace BlazingPokemon.Models
{
    public record PokemonType
    {
        public TypeDetails? Type { get; set; }
    }

    public record TypeDetails
    {
        public string? Name { get; set; }
    }
}