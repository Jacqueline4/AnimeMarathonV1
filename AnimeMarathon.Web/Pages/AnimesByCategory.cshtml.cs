using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class AnimesByCategoryModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AnimesByCategoryModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AnimeDTO> Animes { get; private set; }
        public List<CategoryDTO> Categories { get; private set; } = new List<CategoryDTO>();
        public string CategoryName { get; private set; }
        //public void OnGet()
        //{
        //}

        public async Task OnGetAsync(String categoryName)
        {
            CategoryName = categoryName;

            var responseCategoryName = await _httpClient.GetAsync("https://localhost:7269/Category");
            if (responseCategoryName.IsSuccessStatusCode)
            {
                var content = await responseCategoryName.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<CategoryDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var categoryNames = Categories.Select(category => category.Name).ToList();
            }

            var animesResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimesByCategory/{categoryName}");
            if (animesResponse.IsSuccessStatusCode)
            {
                var content = await animesResponse.Content.ReadAsStringAsync();
                Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

        }
    }
}
