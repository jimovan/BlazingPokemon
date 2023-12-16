using BlazingPokemon.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazingPokemon.Services
{
    public interface IPokemonService
    {        
        Task<Pokemon> GetPokemonId(int id);
        Task<List<Pokemon>> GetPokemonByRegionId(int regionId, bool firstLoad = false);
        List<Region> GetRegions();
        string GetRegionName(int regionId);
    }

    public class PokemonService(IHttpClientFactory httpClientFactory) : IPokemonService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly string _pokeApiUrl = "https://pokeapi.co/api/v2";

        private readonly int _pageLimit = 24;
        private int offset = 0;

        public async Task<Pokemon> GetPokemonId(int id)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"{_pokeApiUrl}/pokemon/{id}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Pokemon>(content,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        private async Task<Pokemon> GetPokemonByUrl(string url)
        {
            using HttpClient client = _httpClientFactory.CreateClient();            

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Pokemon>(content,
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }

        public async Task<List<Pokemon>> GetPokemonByRegionId(int regionId, bool firstLoad = false)
        {
            using HttpClient client = _httpClientFactory.CreateClient();

            var region = GetRegions().FirstOrDefault(x => x.Id == regionId);

            var pageLimit = _pageLimit;

            if (firstLoad)
            {
                offset = region.StartIndex;
            }            

            if(offset + _pageLimit > region.EndIndex)
            {
                pageLimit = (region.EndIndex - offset);
            }

            if(pageLimit > 0)
            {
                var response = await client.GetFromJsonAsync<PokemonResponse>($"{_pokeApiUrl}/pokemon?limit={pageLimit}&offset={offset - 1}",
                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                var taskList = new List<Task<Pokemon>>();

                if (response is not null)
                {
                    var pokemon = new List<Pokemon>();
                    var tasks = response.Results?.Select(x => GetPokemonByUrl(x.Url));

                    foreach (var t in await Task.WhenAll(tasks))
                    {
                        pokemon.Add(t);
                    }

                    offset += pageLimit;

                    return pokemon;
                }
            }           

            return [];
        }

        public List<Region> GetRegions()
        {
            return
            [
                new() { Id = 1, Name = "Kanto", StartIndex = 1, EndIndex = 151 }, // 1-151
                new() { Id = 2, Name = "Johto", StartIndex = 152, EndIndex = 251 }, // 152-251
                new() { Id = 3, Name = "Hoenn", StartIndex = 252, EndIndex = 386 }, // 252-386
                new() { Id = 4, Name = "Sinnoh", StartIndex = 387, EndIndex = 493 }, // 387-493
                new() { Id = 5, Name = "Unova", StartIndex = 494, EndIndex = 649 }, // 494-649
                new() { Id = 6, Name = "Kalos", StartIndex = 650, EndIndex = 721 }, // 650-721
                new() { Id = 7, Name = "Alola", StartIndex = 722, EndIndex = 809 }, // 722-809
                new() { Id = 8, Name = "Galar", StartIndex = 810, EndIndex = 898 }, // 810-898
            ];
        }

        public string GetRegionName(int regionId)
        {
            return GetRegions()?.FirstOrDefault(x => x.Id == regionId)?.Name ?? "";
        }
    }
}
