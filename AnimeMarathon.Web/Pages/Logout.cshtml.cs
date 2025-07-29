using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnimeMarathon.Web.Pages
{
    public class LogoutModel : PageModel
    {
        private const string SessionKeyName = "_Name";
        private const string SessionKeyId = "_Id";

        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.Session.Remove(SessionKeyName);
            HttpContext.Session.Remove(SessionKeyId);
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
