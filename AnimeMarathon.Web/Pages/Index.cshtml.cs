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
        private const int PageSize = 5;
        public List<AnimeDTO> Animes { get; private set; } = new List<AnimeDTO>();
        public IEnumerable<AnimeDTO> AnimesPaginados { get; set; }      
        public int TotalPages { get; set; }
        public int AnimePageNumber { get; set; }
        public List<GenreDTO> Genres { get; private set; } = new List<GenreDTO>();
        public List<CategoryDTO> Categories { get; private set; } = new List<CategoryDTO>();
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

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 8)
        {
            AnimePageNumber = pageNumber;//+pageSize

            var response = await _httpClient.GetAsync($"https://localhost:7269/Anime");//?pageNumber={pageNumber}&pageSize={pageSize}

            if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                TotalPages = (int)Math.Ceiling(Animes.Count / (double)pageSize);
                AnimesPaginados = Animes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                            AnimesGroupedByStatus = Animes.GroupBy(a => a.Status)
                            .ToDictionary(g => g.Key, g => g.Take(8).ToList());
                        //.ToDictionary(g => g.Key, g => g.ToList());
                AnimesGroupedBySubtype = Animes.GroupBy(a => a.Subtype)
                                   .ToDictionary(g => g.Key, g => g.ToList());
                }

            //var responseAnime = await _httpClient.GetAsync("https://localhost:7269/Anime");          
            //if (responseAnime.IsSuccessStatusCode)
            //{
            //    var content = await responseAnime.Content.ReadAsStringAsync();
            //    Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });              
            //    AnimesGroupedByStatus = Animes.GroupBy(a => a.Status)
            //                                    .ToDictionary(g => g.Key, g => g.ToList());
            //    AnimesGroupedBySubtype = Animes.GroupBy(a => a.Subtype)
            //          .ToDictionary(g => g.Key, g => g.ToList());
            //}

            var responseGenreName = await _httpClient.GetAsync("https://localhost:7269/Genre");
                 if (responseGenreName.IsSuccessStatusCode)
                 {
                    var content = await responseGenreName.Content.ReadAsStringAsync();
                    Genres = JsonSerializer.Deserialize<List<GenreDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    var genreNames = Genres.Select(genre => genre.Name).ToList();
                 }

            var responseCategoryName = await _httpClient.GetAsync("https://localhost:7269/Category");
            if (responseCategoryName.IsSuccessStatusCode)
            {
                var content = await responseCategoryName.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<CategoryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var categoryNames = Categories.Select(category => category.Name).ToList();
            }

            //if (responseGenre.IsSuccessStatusCode)
            //{
            //    var content = await responseGenre.Content.ReadAsStringAsync();
            //    Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            //}
        }

    }
}
