using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.DTOs;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AnimeMarathon.Web.Pages
{
    public class AnimeDetailModel : PageModel
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyId = "_Id";

        private readonly HttpClient _httpClient;
        private readonly ILogger<AnimeDetailModel> _logger;
        public AnimeDTO Anime { get; private set; }
        public List<GenreDTO> Genres { get; private set; }
        public List<CategoryDTO> Categories { get; private set; }
        public List<CommentDTO> Comments { get; private set; }
        //public string UserIdString { get; private set; }

        public AnimeDetailModel(HttpClient httpClient, ILogger<AnimeDetailModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var UserIdString = HttpContext.Session.GetString(SessionKeyId);

            var animeResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/GetAnimeById/{id}");
            if (animeResponse.IsSuccessStatusCode)
            {
                var content = await animeResponse.Content.ReadAsStringAsync();
                Anime = JsonSerializer.Deserialize<AnimeDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            
            var genresResponse = await _httpClient.GetAsync($"https://localhost:7269/Genre/GetGenreByAnimeId/{id}");
            if (genresResponse.IsSuccessStatusCode)
            {
                var commentsContent = await genresResponse.Content.ReadAsStringAsync();
                Genres = JsonSerializer.Deserialize<List<GenreDTO>>(commentsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            
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
        public async Task<IActionResult> OnPostAsync(string CommentString, int AnimeId)
        {
            var UserIdString = HttpContext.Session.GetString(SessionKeyId);
            //UserIdString = UserId.ToString();

            if (!string.IsNullOrEmpty(UserIdString))
            {
                var userId = int.Parse(UserIdString);
                var newComment = new CommentDTO
                {
                    CommentString = CommentString,
                    AnimeId = AnimeId,
                    UsuarioId = userId

                };

                var content = new StringContent(JsonSerializer.Serialize(newComment), System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7269/Comments", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/AnimeDetail", new { id = AnimeId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al enviar el comentario");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado");
                return RedirectToPage("/Login");
            }

            return Page();
        }

        //public async Task<IActionResult> OnPostAddToMyListAsync(int AnimeId, string Status)
        //{
        //    var UserIdString = HttpContext.Session.GetString(SessionKeyId);

        //    if (!string.IsNullOrEmpty(UserIdString))
        //    {
        //        var userAnime = new
        //        {
        //            AnimeId = AnimeId,
        //            UsuarioId = int.Parse(UserIdString),
        //            Status = Status
        //        };
        //        var content = new StringContent(JsonSerializer.Serialize(userAnime), System.Text.Encoding.UTF8, "application/json");

        //        var response = await _httpClient.PostAsync("https://localhost:7269/Anime/UserAnime", content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            _logger.LogInformation("Anime añadido a la lista del usuario correctamente.");
        //            return RedirectToPage("/UserMenu");
        //        }
        //        else
        //        {
        //            _logger.LogError("Error al añadir el anime a la lista del usuario.");
        //            ModelState.AddModelError(string.Empty, "Error al añadir el anime a la lista.");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Usuario no autenticado");
        //        return RedirectToPage("/Login");
        //    }

        //    return Page();
        //}

        public async Task<IActionResult> OnPostAddToMyListAsync(int AnimeId, string Status)
        {
            var UserIdString = HttpContext.Session.GetString(SessionKeyId);

            if (!string.IsNullOrEmpty(UserIdString))
            {
                var userId = int.Parse(UserIdString);

                // Verificar si ya existe un registro para este usuario y anime
                var existingUserAnimeResponse = await _httpClient.GetAsync($"https://localhost:7269/Anime/{userId}/{AnimeId}");
                if (existingUserAnimeResponse.IsSuccessStatusCode)
                {
                    var existingUserAnimeContent = await existingUserAnimeResponse.Content.ReadAsStringAsync();
                    var existingUserAnime = JsonSerializer.Deserialize<UsersAnimeDTO>(existingUserAnimeContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (existingUserAnime != null)
                    {
                        // Ya existe un registro, realiza la operación de actualización
                        existingUserAnime.Status = Status;
                        var contentUpdate = new StringContent(JsonSerializer.Serialize(existingUserAnime), System.Text.Encoding.UTF8, "application/json");
                        var responseUpdate = await _httpClient.PutAsync("https://localhost:7269/Anime/UserAnime", contentUpdate);

                        if (responseUpdate.IsSuccessStatusCode)
                        {
                            _logger.LogInformation("Estado de anime actualizado correctamente.");
                            return RedirectToPage("/UserMenu");
                        }
                        else
                        {
                            _logger.LogError("Error al actualizar el estado del anime.");
                            ModelState.AddModelError(string.Empty, "Error al actualizar el estado del anime.");
                        }
                    }
                }


                // No existe un registro, realiza la operación de inserción
                var newUserAnime = new
                {
                    AnimeId = AnimeId,
                    UsuarioId = userId,
                    Status = Status
                };
                var content = new StringContent(JsonSerializer.Serialize(newUserAnime), System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7269/Anime/UserAnime", content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Anime añadido a la lista del usuario correctamente.");
                    return RedirectToPage("/UserMenu");
                }
                else
                {
                    _logger.LogError("Error al añadir el anime a la lista del usuario.");
                    ModelState.AddModelError(string.Empty, "Error al añadir el anime a la lista.");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario no autenticado");
                return RedirectToPage("/Login");
            }

            return Page();
        }


    }
}
