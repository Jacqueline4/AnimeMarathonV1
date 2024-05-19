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
            var commentsResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetCommentByAnime/{id}");
            if (commentsResponse.IsSuccessStatusCode)
            {
                var commentsContent = await commentsResponse.Content.ReadAsStringAsync();
                Comments = JsonSerializer.Deserialize<List<CommentDTO>>(commentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }

}
