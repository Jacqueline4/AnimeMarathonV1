

using AnimeMarahon.Core.Entities;
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

        public int TotalPages { get; set; } = 1;
        public int AnimePageNumber { get; set; }
        public List<AnimeDTO> UserAnimesList { get; private set; } = new List<AnimeDTO>();
        public Dictionary<string, List<AnimeDTO>> UserAnimesGroupedByStatus { get; private set; } = new Dictionary<string, List<AnimeDTO>>();
        public IEnumerable<AnimeDTO> AnimesPaginados { get; set; } = new List<AnimeDTO>();

        public async Task OnGetAsync(UserDTO user, string sortBy, int pageNumber = 1, int pageSize = 8)
        {
            var userIdString = HttpContext.Session.GetString(SessionKeyId);
            AnimePageNumber = pageNumber;
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out var userId))
            {
                var response1 = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimeByUserId/{userId}");
                if (response1.IsSuccessStatusCode)
                {
                    var content = await response1.Content.ReadAsStringAsync();
                    UserAnimesList = JsonSerializer.Deserialize<List<AnimeDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    foreach (var anime in UserAnimesList)
                    {
                        foreach (var userAnime in anime.UserAnimes)
                        {
                            var status = userAnime.Status.Replace(" ", "-");
                                                     
                            if (!UserAnimesGroupedByStatus.ContainsKey(status))
                            {
                              UserAnimesGroupedByStatus[status] = new List<AnimeDTO>();
                            }
                            UserAnimesGroupedByStatus[status].Add(anime);
                        }
                    }

                }
                if (!string.IsNullOrEmpty(sortBy))
                {

                    if (sortBy.Equals("Rating"))
                    {
                        UserAnimesList = UserAnimesList.OrderByDescending(a => a.MiValoracion).ToList();
                    }
                    else if (sortBy.Equals("Title"))
                    {
                        UserAnimesList = UserAnimesList.OrderBy(a => a.Title).ToList();
                    }
                    else if (sortBy.Equals("Date"))
                    {
                        UserAnimesList = UserAnimesList.OrderByDescending(a => a.StartDate).ToList();
                    }
                }

              
                TotalPages = (int)Math.Ceiling(UserAnimesList.Count / (double)pageSize);
                AnimesPaginados = UserAnimesList.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            }
            else
            {
                _logger.LogWarning("Usuario no autenticado intentó acceder al menú de usuario.");
            }
        }
    }
}
