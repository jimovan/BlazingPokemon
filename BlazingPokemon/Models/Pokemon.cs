using System.Text.Json.Serialization;

namespace BlazingPokemon.Models
{
    public record Pokemon
    {
        public int Id { get; set; }
        public string? Name { get; set; }        
        public List<PokemonType>? Types { get; set; }
        public string? Type => Types?.First()?.Type?.Name ?? "";

        [JsonPropertyName("sprites")]
        public Sprite? Sprite { get; set; }

        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public List<Stat>? Stats { get; set; }
        public Species? Species { get; set; }
        public List<Evolution>? Evolutions { get; set; }
    }

    public record Sprite
    {
        public OtherImage Other { get; set; }

        public record OtherImage
        {
            [JsonPropertyName("official-artwork")]
            public OfficialArtwork OfficalArtwork { get; set; }

            public record OfficialArtwork
            {
                [JsonPropertyName("front_default")]
                public string? ImageUrl { get; set; }
            }
        }
    }    

    public record Stat
    {
        [JsonPropertyName("base_stat")]
        public int Value { get; set; }
        [JsonPropertyName("stat")]
        public StatName StatsName { get; set; }

        public record StatName
        {            
            public string? Name { get; set; }
        }        
    }

    public record Species
    {
        public string? Name { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("evolution_chain")]
        public EvolutionChainUrl ChainUrl { get; set; }

        public record EvolutionChainUrl
        {
            public string Url { get; set; }
        }
    }
}
