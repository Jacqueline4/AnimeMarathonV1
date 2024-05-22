using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class AnimeDetailModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public AnimeDTO Anime { get; private set; }
        public List<GenreDTO> Genres { get; private set; }
        public List<CategoryDTO> Categories { get; private set; }
        public List<CommentDTO> Comments { get; private set; }

        public AnimeDetailModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync(int id)
        {

            var response = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimeById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Anime = JsonSerializer.Deserialize<AnimeDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            //https://localhost:7269/Genre/GetGenreByAnimeId/1
            var genresResponse = await _httpClient.GetAsync($"https://localhost:7269/Genre/GetGenreByAnimeId/{id}");
            if (genresResponse.IsSuccessStatusCode)
            {
                var commentsContent = await genresResponse.Content.ReadAsStringAsync();
                Genres = JsonSerializer.Deserialize<List<GenreDTO>>(commentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            //https://localhost:7269/Category/GetCategoryByAnimeId/
            var categoriesResponse = await _httpClient.GetAsync($"https://localhost:7269/Category/GetCategoryByAnimeId/{id}");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                var commentsContent = await categoriesResponse.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<CategoryDTO>>(commentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var commentsResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetCommentByAnime/{id}");
            if (commentsResponse.IsSuccessStatusCode)
            {
                var commentsContent = await commentsResponse.Content.ReadAsStringAsync();
                Comments = JsonSerializer.Deserialize<List<CommentDTO>>(commentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }

}
