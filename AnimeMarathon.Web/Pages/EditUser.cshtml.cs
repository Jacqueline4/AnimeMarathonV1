using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AnimeMarathon.Web.Pages
{
    public class EditUserModel : PageModel
    {
        public UserDTO User { get; set; }

        private readonly IHttpClientFactory _clientFactory;

        public EditUserModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //var userIdString = HttpContext.Session.GetString("_Id");
            var userName= HttpContext.Session.GetString("_Name");
            //if (int.TryParse(userIdString, out var userId))
            //{
            var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"https://localhost:7269/User/GetUserByName/{userName}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    User = JsonConvert.DeserializeObject<UserDTO>(content);
                    return Page();
                }
            //}

            return RedirectToPage("/Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdString = HttpContext.Session.GetString("_Id");
            if (int.TryParse(userIdString, out var userId))
            {
                //User.Id = userId;

                var client = _clientFactory.CreateClient();
                var content = new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7269/User", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/UserMenu");
                }
                ModelState.AddModelError(string.Empty, "Error al guardar los cambios");
        }

            return Page();
        }
    }
}
