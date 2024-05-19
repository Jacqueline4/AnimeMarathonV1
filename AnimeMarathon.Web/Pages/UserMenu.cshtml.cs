
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

        private readonly HttpClient _httpClient;

        public UserMenuModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string Username { get; private set; }
        //public User User { get; private set; }
        public List<AnimeDTO> Animes { get; private set; } = new List<AnimeDTO>();
        public List<AnimeDTO> UserAnimes { get; private set; } = new List<AnimeDTO>();
        public async Task OnGetAsync(UserDTO user)
        {
            if (user != null)
            {
                Username = user.Name;
            var response = await _httpClient.GetAsync("https://localhost:7269/Anime");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Animes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var response1 = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimeByUserId/{user.Id}");
            if (response1.IsSuccessStatusCode)
            {
                var content = await response1.Content.ReadAsStringAsync();
                UserAnimes = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
    }
    }
}
