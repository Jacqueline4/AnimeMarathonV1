using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;
        public List<AnimeDTO> Animes { get; private set; } = new List<AnimeDTO>();
        public List<GenreDTO> Genres { get; private set; } = new List<GenreDTO>();
        public Dictionary<string, List<AnimeDTO>> AnimesGroupedByStatus { get; private set; } = new Dictionary<string, List<AnimeDTO>>();
        public Dictionary<string, List<AnimeDTO>> AnimesGroupedBySubtype { get; private set; } = new Dictionary<string, List<AnimeDTO>>();
        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        //public void OnGet()
        //{

        //}

        public async Task OnGetAsync()
        {
                var response = await _httpClient.GetAsync("https://localhost:7269/Anime");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                             AnimesGroupedByStatus = Animes.GroupBy(a => a.Status)
                                                .ToDictionary(g => g.Key, g => g.ToList());
                             AnimesGroupedBySubtype = Animes.GroupBy(a => a.Subtype)
                                   .ToDictionary(g => g.Key, g => g.ToList());
                }


                 var responseGenreName = await _httpClient.GetAsync("https://localhost:7269/Genre");
                 if (responseGenreName.IsSuccessStatusCode)
                 {
                    var content = await responseGenreName.Content.ReadAsStringAsync();
                    Genres = JsonSerializer.Deserialize<List<GenreDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    var genreNames = Genres.Select(genre => genre.Name).ToList();
                 }

            //if (responseGenre.IsSuccessStatusCode)
            //{
            //    var content = await responseGenre.Content.ReadAsStringAsync();
            //    Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //}
        }

    }
}
