using System.Text.Json.Serialization;

namespace BlazingPokemon.Models
{
    public record Evolution
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Sprite? Sprite { get; set; }
    }

    public record EvolutionChain
    {
        [JsonPropertyName("chain")]
        public Dictionary<string, Chain>? Chains { get; set; }
        
    }

    public record Chain
    {
        [JsonPropertyName("evolves_to")]
        public Chain? Evolution { get; set; }
        public Species? Species { get; set; }
    }

}
