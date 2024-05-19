using AnimeMarahon.Core.Entities;
using AnimeMarathon.Application.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace AnimeMarathon.Web.Pages
{
    public class LoginModel : PageModel
    {
        //public void OnGet()
        //{
        //}
         private readonly IHttpClientFactory _clientFactory;
        //private readonly ILogger<LoginModel> _logger;

        public LoginModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        //public LoginModel(IHttpClientFactory clientFactory, ILogger<LoginModel> logger) // Modificar el constructor para incluir ILogger
        //{
        //    _clientFactory = clientFactory;
        //    _logger = logger; // Asignar logger
        //}
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
                //_logger.LogInformation("User object: {User}", user); //

                return RedirectToPage("/UserMenu", user);
                //return RedirectToPage("/UserMenu", username);
            }
            else
            {
                //var content = await response.Content.ReadAsStringAsync();
                //var error = JsonConvert.DeserializeObject<dynamic>(content);
                //return ModelState.AddModelError(string.Empty, error.error.ToString());
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
