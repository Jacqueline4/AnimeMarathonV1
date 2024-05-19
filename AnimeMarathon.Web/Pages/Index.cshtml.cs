using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnimeMarathon.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //public void OnGet()
        //{

        //}
        public IActionResult OnGet()
        {
            // Redirige a la p�gina de inicio de sesi�n
            return RedirectToPage("/Login");
        }
    }
}
