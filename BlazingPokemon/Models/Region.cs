namespace BlazingPokemon.Models
{
    public record Region
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}
