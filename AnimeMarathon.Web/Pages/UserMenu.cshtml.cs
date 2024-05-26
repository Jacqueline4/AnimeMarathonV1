

using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class UserMenuModel : PageModel
    {
        //public void OnGet()
        //{
        //}
        public const string SessionKeyName = "_Name";
        public const string SessionKeyId = "_Id";

        private readonly HttpClient _httpClient;
        private readonly ILogger<UserMenuModel> _logger;

        public UserMenuModel(HttpClient httpClient, ILogger<UserMenuModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public List<AnimeDTO> Animes { get; private set; } = new List<AnimeDTO>();
        public List<AnimeDTO> UserAnimes { get; private set; } = new List<AnimeDTO>();
        public Dictionary<string, List<AnimeDTO>> UserAnimesGroupedByStatus { get; private set; } = new Dictionary<string, List<AnimeDTO>>();

      
        public async Task OnGetAsync(UserDTO user)
        {
            var userIdString = HttpContext.Session.GetString(SessionKeyId);
            //if (user != null)
            //{
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out var userId))
            {

                var response = await _httpClient.GetAsync("https://localhost:7269/Anime");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var response1 = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimeByUserId/{userId}");
            if (response1.IsSuccessStatusCode)
            {
                var content = await response1.Content.ReadAsStringAsync();
                UserAnimes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    UserAnimesGroupedByStatus = UserAnimes.GroupBy(a => a.Subtype)
                                                      .ToDictionary(g => g.Key, g => g.ToList());
                }
        }
            else
            {
                _logger.LogWarning("Usuario no autenticado intentó acceder al menú de usuario.");
            }
        }
       
        
    }
}
