namespace BlazingPokemon.Models
{
    public record PokemonResponse
    {
        public int Count { get; set; }
        public List<SimplePokemon>? Results { get; set; }
    }

    public record SimplePokemon
    {
        public string? Name { get; set; }
        public string? Url { get; set; }
    }
}
