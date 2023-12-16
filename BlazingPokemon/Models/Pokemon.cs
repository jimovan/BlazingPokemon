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
        public Sprite Sprite { get; set; }
    }

    public class Sprite
    {
        public OtherImage Other { get; set; }
    }

    public class OtherImage
    {
        [JsonPropertyName("official-artwork")]
        public OfficialArtwork OfficalArtwork { get; set; }
    }

    public class OfficialArtwork
    {
        [JsonPropertyName("front_default")]
        public string? ImageUrl { get; set; }
    }
}
