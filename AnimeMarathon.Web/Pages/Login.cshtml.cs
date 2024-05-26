
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AnimeMarathon.Web.Pages
{
    public class LoginModel : PageModel
    {
      
        public const string SessionKeyName = "_Name";
        public const string SessionKeyId = "_Id";

        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IHttpClientFactory clientFactory, ILogger<LoginModel> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync(string username, string password)
        {
            var client = _clientFactory.CreateClient();

            client.BaseAddress = new Uri("https://localhost:7269");

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri("https://localhost:7269/User/Login"))
            {
                
            };
            var jsonStr = JsonConvert.SerializeObject(new UserViewDTO { Name = username, Password = password });
            var reqcontent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await client.PostAsJsonAsync(new Uri("https://localhost:7269/User/Login"), new UserViewDTO { Name = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDTO>(content);

                HttpContext.Session.SetString(SessionKeyName, user.Name);
                HttpContext.Session.SetString(SessionKeyId, user.Id.ToString());
                return RedirectToPage("/UserMenu");

                //return RedirectToPage("/UserMenu", user);
              
            }
            else
            {
               
                ModelState.AddModelError(string.Empty,"Usuario o contraseña incorrectos");
                return Page();
            }
        }
    }
    public class UserViewDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
