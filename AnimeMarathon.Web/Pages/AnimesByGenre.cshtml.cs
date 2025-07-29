using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class AnimesByGenreModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AnimesByGenreModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        //public GenreDTO Genre { get; private set; }

        public List<AnimeDTO> Animes { get; private set; }
        public List<GenreDTO> Genres { get; private set; } = new List<GenreDTO>();
        public string GenreName { get; private set; }
      

        public async Task OnGetAsync(String genreName)
        {
            GenreName = genreName;

            var responseGenreName = await _httpClient.GetAsync("https://localhost:7269/Genre");
            if (responseGenreName.IsSuccessStatusCode)
            {
                var content = await responseGenreName.Content.ReadAsStringAsync();
                Genres = JsonSerializer.Deserialize<List<GenreDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var genreNames = Genres.Select(genre => genre.Name).ToList();
            }

            var animesResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimesByGenre/{genreName}");
            if (animesResponse.IsSuccessStatusCode)
            {
                var content = await animesResponse.Content.ReadAsStringAsync();
                Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            
        }
    }
}
